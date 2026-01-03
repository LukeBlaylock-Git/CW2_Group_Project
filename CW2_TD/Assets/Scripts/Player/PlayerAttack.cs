using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float Damage = 25f;
    public float Range = 50f;
    public float FireRate = 0.2f;

    [Header("Layer Mask")]
    public LayerMask EnemyLayer; //For the raycast we will be "firing"

    private float NextFireTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentPhase != GameManager.GamePhase.Combat)
            return;
        if (Input.GetMouseButton(0) && Time.time >= NextFireTime)
        {
            NextFireTime = Time.time + FireRate;
            Fire();
        }
    }

    void Fire()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range, EnemyLayer))
        {
            Enemy Enemy = hit.collider.GetComponent<Enemy>();
            if (Enemy != null)
            {
                Enemy.TakeDamage(Damage);
            }
        }
    }
}
