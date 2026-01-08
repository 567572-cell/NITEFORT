using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 150f;
    public Transform cameraTransform; // LEAVE THIS EMPTY - code will find it
    
    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // AUTO-FIND CAMERA if not assigned
        if (cameraTransform == null)
        {
            // Try to find camera in children first
            cameraTransform = GetComponentInChildren<Camera>()?.transform;
            
            // If not found, use main camera
            if (cameraTransform == null && Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
                Debug.Log("Using Main Camera");
            }
            
            if (cameraTransform == null)
            {
                Debug.LogError("No camera found! Add a camera to the scene.");
            }
            else
            {
                Debug.Log($"Camera found: {cameraTransform.name}");
            }
        }
        
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        
        // Update animator
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        bool isRunning = moveX != 0 || moveZ != 0;
        
        if (animator != null)
            animator.SetBool("run", isRunning);
    }

    void HandleMovement()
    {
        // Check if controller exists and is enabled
        if (controller == null || !controller.enabled)
            return;
            
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        // Check if camera exists
        if (cameraTransform == null)
        {
            Debug.LogWarning("No camera assigned for mouse look!");
            return;
        }
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}