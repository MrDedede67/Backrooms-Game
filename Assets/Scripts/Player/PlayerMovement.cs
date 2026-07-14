using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;

    private float rotationX = 0;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] public Camera playerCamera;


    public bool isPaused = false;

    [SerializeField] private BoxCollider playerCollider;
    [SerializeField] Transform visualContainer;

    // Define these variables so the script knows what to use!
    private Vector3 defaultSize = new Vector3(1f, 2f, 1f);
    private float sneakYSize = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Alex cant comprehend this
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {

            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed = 2f;

            // 1. Update Box Collider size
            playerCollider.size = new Vector3(defaultSize.x, sneakYSize, defaultSize.z);

            // 2. Update Box Collider center so it stays on the ground
            // We shift the center down by half the amount we removed
            float offset = (defaultSize.y - sneakYSize) / 2f;
            playerCollider.center = new Vector3(0, -offset, 0);

            // 3. Update Visuals
            visualContainer.localScale = new Vector3(1f, sneakYSize, 1f);
        }
        else
        {
            // Reset to normal
            movementSpeed = Input.GetKey(KeyCode.LeftShift) ? 6f : 3f;

            playerCollider.size = defaultSize;
            playerCollider.center = Vector3.zero; // Assuming your default center is (0,0,0)
            visualContainer.localScale = Vector3.one;
        }

        // Calculate directions relative to Player's transform
        Vector3 moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);

        Vector3 targetVelocity = moveDirection.normalized * movementSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);


        if (Input.GetKeyDown("space") && isGrounded())
        {
            Jump();
        }

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    } 
}
