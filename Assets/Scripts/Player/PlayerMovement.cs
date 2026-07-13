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

        if(Input.GetKey(KeyCode.LeftShift)) {
            movementSpeed = 6f;
        } else
        {
            movementSpeed = 3f;
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
