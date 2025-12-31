using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public Vector3 Jump;
    public float Jumpforce = 2.0f; //Since im using force this determines how high the player jumps
    public bool IsGrounded;

    private Rigidbody rb; // Reference to player's Rigidbody.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        Jump = new Vector3(0, Jumpforce, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        IsGrounded = true; //Will ensure is grounded is on when on the floor
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(Jump * Jumpforce, ForceMode.Impulse);
            IsGrounded = false;
        }
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}

//Reference
/*
 N/A (no date) 
 Add a movement script, 
 Unity Learn. 
 Available at: https://learn.unity.com/pathway/unity-essentials/unit/programming-essentials/tutorial/add-a-movement-script?version=6.0#6699f6f6edbc2a0fecfe9903 
 (Accessed: 04 December 2025). */

