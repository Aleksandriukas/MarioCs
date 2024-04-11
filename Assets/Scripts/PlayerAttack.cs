using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackTimeout;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator animator;
    private PlayerMovement playerMovement;
    private float attackTimer = 100000;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0) && attackTimer > attackTimeout && playerMovement.canAttack() ) {
            Attack();
        }

        attackTimer += Time.deltaTime;


    }

    // Update is called once per frame
    private void Attack()
    {
        animator.SetTrigger("attack");
        attackTimer = 0;

        fireballs[FindFireball()].transform.position = attackPoint.position;
        fireballs[FindFireball()].GetComponent<Fireball>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return -1;
    }

}
