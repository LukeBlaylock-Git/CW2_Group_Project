using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [Header("Camera Settings")]
    float XRotation;
    float YRotation;
    public float SensX;
    public float SensY; //The sensitivity of the mouse
    float Height = 1.5f; //This sets the height of the camera forcefully, I highly recommend NOT changing this.
    [Header("Object References")]
    public Transform Orientation;
    public Transform Player;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks the mouse
        Cursor.visible = false;
    }

    void Update()
    {
        float MouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float MouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        XRotation -= MouseY; //X Axis Camera
        XRotation = Mathf.Clamp(XRotation, -90f, 90f); //Prevents the Character from looking too far up or down respectively.

        YRotation += MouseX; //Y Axis Camera

        //Rotate camera and orientation
        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);
    }
    void LateUpdate()
    {
        transform.position = Player.position + Vector3.up * Height; //This is going to force our camera to be centered on our player, no matter what.
    }
    //Reference https://youtu.be/f473C43s8nE?si=nciixbfiIJW71S5R&t
}

// Helped by Copilot to provide the concept as to how to make this code