using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private PlayerState state;
    public Animator animator;

    private void Start()
    {
        SetState(PlayerState.Idle);
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            //Seteamos a velocidad a -2 en lugar de a 0 porque é bastante posible que
            //detecte que está grounded antes de que realmente o esté
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        move = move.normalized;

        if(move != Vector3.zero)
        {
            SetState(PlayerState.Walk);
            Debug.Log("Ando");
        }
        else
        {
            SetState(PlayerState.Idle);
            Debug.Log("No ando");
        }

        controller.Move(move * speed * Time.deltaTime);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        //Multiplicamos velocity por Time.deltaTime outra vez porque así funciona a ecuación da caida libre
        controller.Move(velocity * Time.deltaTime);
    }

    private void SetState(PlayerState newState)
    {
        if (state != newState)
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Walk");
            state = newState;
            animator.SetTrigger($"{newState}");
        }
    }

    public enum PlayerState
    {
        Idle,
        Walk
    }
}
