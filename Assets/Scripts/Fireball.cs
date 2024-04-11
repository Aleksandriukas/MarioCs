using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] float speed = 20;
    private bool hit;
    private float dir;

    private float lifeTime = 0f;

    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (hit) {
            return;
           }
            float movementSpeed = speed * Time.deltaTime * dir;
            transform.Translate(movementSpeed, 0, 0);
        
        lifeTime += Time.deltaTime;
        if(lifeTime > 4f)
        {
            Deactivate();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit= true;
        boxCollider2D.enabled = false;
        animator.SetTrigger("explode");


        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetTrigger("hurt");
            boxCollider2D.enabled = false;
        }
    }

    public void SetDirection(float direction)
    {
        lifeTime = 0f;
        dir = direction;
        this.gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;

        if(Mathf.Sign(localScaleX) != Mathf.Sign(dir))
        {
            localScaleX = -1;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

}
