using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variaveis de andar
    public float velocity;
    private bool facingRight = true;
    private Vector3 scale;

    //variaveis de pulo
    public float jumpForce;
    private Rigidbody2D rigidBody;

    //resolvendo bug do pulo
    private bool isGrounded = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float circleRadius;

    //variáveis de animação
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            //andar pra direita
            if(!facingRight)
            {
                Turn();
            }

            Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += (movementDirection * Time.deltaTime * velocity);

            animator.SetBool("isRunning", true);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            //andar pra esquerda
            if (facingRight)
            {
                Turn();
            }

            Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += (movementDirection * Time.deltaTime * velocity);

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    void Turn()
    {
        facingRight = !facingRight;

        scale = transform.localScale;
        // scale.x = scale.x * (-1);
        scale.x *= (-1);
        transform.localScale = scale;
    }


    void checkIfIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsGround);
    }

    void Jump()
    {
        checkIfIsGrounded();

        //se o jogador apertar espaço, dá um impulso pra cima
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        } else
        {
            animator.SetBool("isJumping", true);
        }
    } 
}