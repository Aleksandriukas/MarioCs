using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;


    [SerializeField] private Behaviour[] components;

    public float currentHealth { get; private set; }
    private Animator animator;
    private bool isDead;

    private void Awake()
    {

        animator = GetComponent<Animator>();
        currentHealth = startingHealth;

    }
    public void TakeDamage(float _damage)
    {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {

            animator.SetTrigger("hurt");

        }
        else
        {
            if (!isDead)
            {
                animator.SetTrigger("die");
                isDead = true;

                if (GetComponent<PlayerMovement>() != null)
                {

                    GetComponent<PlayerMovement>().enabled = false;

                }

                if(GetComponentInParent<EnemyPatrol>() != null)
                {

                    GetComponentInParent<EnemyPatrol>().enabled = false;

                }
                if (GetComponent<KnightEnemy>() != null)
                { 

                    GetComponent<KnightEnemy>().enabled = false;

                }

                foreach (Behaviour comp in components)
                { 

                    comp.enabled = false;
                
                }

            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Deactivate()
    {

        gameObject.SetActive(false);

    }

    public void Respawn()
    {

        isDead = false;

        currentHealth = startingHealth;
        isDead = false;
        animator.ResetTrigger("die");
        animator.Play("Idle");

        foreach (Behaviour comp in components)
        {

            comp.enabled = true;

        }

    }

}