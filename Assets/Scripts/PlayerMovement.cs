using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpTimeout;
    private float horizontal;

    private CameraController cameraController;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();


       
    }
    private void Update()
    {

        horizontal = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontal * speed, body.velocity.y);

        if (horizontal > 0.01f) { 
            transform.localScale = Vector3.one;
        }
        else if (horizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

       

        
        animator.SetBool("run", horizontal != 0);
        animator.SetBool("grounded", isGrounded());
        
        if(wallJumpTimeout > 0.15f)
        {

            body.velocity = new Vector2(horizontal * speed, body.velocity.y);
            if (onWall() && !isGrounded()) { 
            
                body.gravityScale = 0;

                body.velocity = Vector2.zero;

            }
            else
            {
                body.gravityScale = 2;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else { 
        
            wallJumpTimeout += Time.deltaTime;
        }

    }

    private void Jump()
    {

        if (isGrounded()) { 
            body.velocity = new Vector2(body.velocity.x, speed*2);
            animator.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if(horizontal == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 12, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, 6);
            }
            wallJumpTimeout = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    private bool isGrounded() { 

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

        return raycastHit2D.collider != null;
    
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);

        return raycastHit2D.collider != null;
    }


    public bool canAttack()
    {
        return horizontal == 0 && isGrounded() && !onWall();
    }

}

