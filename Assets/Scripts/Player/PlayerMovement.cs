using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Control Settings")]
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;

    [Header("References")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] public Camera playerCamera;
    [SerializeField] private BoxCollider playerCollider;
    [SerializeField] Transform visualContainer;

    Rigidbody rb;
    private float rotationX = 0;
    public bool isPaused = false;
    private Vector3 defaultSize = new Vector3(1f, 2f, 1f);
    private float sneakYSize = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Alex cant comprehend this
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isPaused)
        {

            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed = 2f;

            playerCollider.size = new Vector3(defaultSize.x, sneakYSize, defaultSize.z);

            float offset = (defaultSize.y - sneakYSize) / 2f;
            playerCollider.center = new Vector3(0, -offset, 0);

            visualContainer.localScale = new Vector3(1f, sneakYSize, 1f);
        }
        else
        {
            movementSpeed = Input.GetKey(KeyCode.LeftShift) ? 6f : 3f;

            playerCollider.size = defaultSize;
            playerCollider.center = Vector3.zero; 
            visualContainer.localScale = Vector3.one;
        }

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
