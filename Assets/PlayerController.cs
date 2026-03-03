using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Increased default speed
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    public CharacterController controller;
    public Animator animator;
    public Transform cameraTransform;

    private Vector3 velocity;

    // Variables for smoothing the animation transition
    private float currentAnimSpeed = 0f;
    private float animSpeedVelocity = 0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = cameraTransform.forward * v + cameraTransform.right * h;
        move.y = 0;

        // Only normalize if there is input, otherwise it stays 0
        if (move.magnitude > 0.1f)
        {
            move.Normalize();
        }

        controller.Move(move * speed * Time.deltaTime);

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // --- SMOOTHED ANIMATION CODE ---
        // Smoothly transition the float from current to target over 0.1 seconds
        float targetAnimSpeed = move.magnitude;
        currentAnimSpeed = Mathf.SmoothDamp(currentAnimSpeed, targetAnimSpeed, ref animSpeedVelocity, 0.1f);

        animator.SetFloat("speed", currentAnimSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}