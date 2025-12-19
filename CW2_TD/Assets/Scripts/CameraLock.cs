using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLock : MonoBehaviour
{
    public float Sens = 100f; //The sensitivity of the mouse
    public Transform Player; //Used to move the players rotation

    float XRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks the mouse
    }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * Sens * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * Sens * Time.deltaTime;

        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f); //Prevent them from looking fully to prevent flipping

        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);

        Player.Rotate(Vector3.up * MouseX);
    }
}
