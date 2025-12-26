using UnityEngine;
using UnityEngine.Splines;

public class EnemySplineMovement : MonoBehaviour
{
    public EnemyData Type; //Imported stats
    public SplineContainer Path; //Spline holder, tldr just put the spline in here

    private float Pos = 0f; //Current position along said Spline

    void Start()
    {
        InitVisual();
    }

    void Update()
    {
        if (Path == null || Path.Splines.Count == 0) return; //If there is no path asigned or the spline has no curves, come to a stop

        var spline = Path.Splines[0]; //Grab the first spline available (most levels SHOULD only have 1, if you have added anymore, ill be concerned.)

       
        float Length = SplineUtility.CalculateLength(spline, Path.transform.localToWorldMatrix); //Calculates the length of the spline.
        if (Length < 0.01f) return;   // No length, no movement.

        float dt = (Type.MoveSpeed / Length) * Time.deltaTime;
        Pos += dt;
        if (Pos >= 1f) //"if we had reached the end of the path"
        {
            Destroy(gameObject);
            return;
        }

        Vector3 localPos = SplineUtility.EvaluatePosition(spline, Pos);
        Vector3 worldPos = Path.transform.TransformPoint(localPos);
        Vector3 localTan = SplineUtility.EvaluateTangent(spline, Pos);
        Vector3 worldTan = Path.transform.TransformDirection(localTan);

        transform.position = worldPos;
        if (worldTan.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(worldTan);
    }

    void InitVisual()
    {
        if (Type != null && Type.EnemyPrefab != null)
        {
            var obj = Instantiate(Type.EnemyPrefab, transform);
            obj.transform.localPosition = Vector3.zero;
        }
    }
}