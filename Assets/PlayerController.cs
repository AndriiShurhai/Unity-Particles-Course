using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public CharacterController controller;
    public Animator animator;
    public Transform cameraTransform;
    private Vector3 velocity;
    private float currentAnimSpeed = 0f;
    private float animSpeedVelocity = 0f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 move = camForward * v + camRight * h;

        if (move.magnitude > 0.1f)
        {
            move.Normalize();
        }

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = (move * speed) + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);

        float targetAnimSpeed = move.magnitude;
        currentAnimSpeed = Mathf.SmoothDamp(currentAnimSpeed, targetAnimSpeed, ref animSpeedVelocity, 0.1f);
        animator.SetFloat("speed", currentAnimSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }
}