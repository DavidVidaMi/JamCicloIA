using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float speedMultiplier = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool interacting = false;

    Vector3 velocity;
    bool isGrounded;
    bool animationFinished = true;

    private PlayerState state;
    public Animator animator;

    public static PlayerMovementScript instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetState(PlayerState.Idle);
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            //Seteamos a velocidad a -2 en lugar de a 0 porque � bastante posible que
            //detecte que est� grounded antes de que realmente o est�
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        move = move.normalized;

        if(move != Vector3.zero)
        {
            if (isGrounded)
            {
                SetState(PlayerState.Walk);
            }
        }
        else
        {
            if (isGrounded)
            {
                SetState(PlayerState.Idle);
            }
        }

        if (interacting)
        {
            interacting = false;
            SetState(PlayerState.Interact);
        }

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            if (state == PlayerState.Walk)
            {
                SetState(PlayerState.Jump);
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            else if (state == PlayerState.Idle && animationFinished)
            {
                SetState(PlayerState.StandJump);
                StartCoroutine("JumpAction");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * speedMultiplier;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / speedMultiplier;
        }


        velocity.y += gravity * Time.deltaTime;

        //Multiplicamos velocity por Time.deltaTime outra vez porque as� funciona a ecuaci�n da caida libre
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator JumpAction()
    {
        animationFinished = false;
        yield return new WaitForSeconds(0.75f);

        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        animationFinished = true;
    }

    private void SetState(PlayerState newState)
    {
        if (state != newState)
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Walk");
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("StandJump");
            animator.ResetTrigger("Interact");
            state = newState;
            animator.SetTrigger($"{newState}");
        }
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        StandJump,
        Interact
    }
}
