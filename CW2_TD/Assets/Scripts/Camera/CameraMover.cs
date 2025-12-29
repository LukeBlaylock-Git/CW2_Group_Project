using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform CameraPos;


    // Update is called once per frame
    void Update()
    {
        transform.position = CameraPos.position;
    }
    //Reference: https://youtu.be/f473C43s8nE?si=nciixbfiIJW71S5R&t=173
}
