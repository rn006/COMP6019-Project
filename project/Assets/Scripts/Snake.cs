using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Snake : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    public Animator animator;
    public CoinDropper coinDropper;

    int hp = 100;
    int coinsDropped = 5;

    bool canBeHit = true;
    bool aggravated = false;
    bool attacking = false;

    float runSpeed = 3f;
    float dashSpeed = 10f;
    float dashDuration = 0.5f;

    float attackRange = 1.5f;
    int damage = 15;
    int poison = 5;

    float death = 2f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nav.speed = runSpeed;
        nav.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(canBeHit && player)
        {
            if(aggravated && !attacking)
            {
                nav.SetDestination(player.position);
            }
        }
    }

    public void Aggravate()
    {
        if(!aggravated && canBeHit)
        {
            aggravated = true;
            animator.SetBool("Aggravated", true);
            StartCoroutine("AttackCycle");

        }
    }

    IEnumerator AttackCycle()
    {
        while (canBeHit && player)
        {
            // here
            yield return new WaitForSeconds(Random.Range(5f, 15f));
            attacking = true;
            nav.ResetPath();
            animator.SetTrigger("Fear");
            yield return new WaitForSeconds(3f);
            if (!player)
            {
                yield break;
            }

            animator.SetTrigger("Attack");
            nav.speed = dashSpeed;
            float dashTime = 0;

            while (dashTime < dashDuration && player)
            {
                nav.SetDestination(player.position);
                dashTime += Time.deltaTime;
                yield return null;
            }
            nav.ResetPath();

            nav.speed = runSpeed;
            if (!player)
            {
                yield break;
            }

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                // Deal initial damage
                player.GetComponent<PlayerMovement>().Hit(damage);

                // Poison the player
                player.GetComponent<PlayerMovement>().Poison(poison);
            }

            yield return new WaitForSeconds(0.5f);

            attacking = false;
        }
    }

    public void Hit(int _damage)
    {
        //If we can be hit
        if (canBeHit)
        {
            //Remove _damage value from hp
            hp -= _damage;

            GameObject[] snakes = GameObject.FindGameObjectsWithTag("Snake");

            foreach (GameObject snake in snakes)
            {
                snake.GetComponent<Snake>().Aggravate();
            }

            //If hp is less than 1
            if (hp < 1)
            {
                StopAllCoroutines();
                nav.enabled = false;

                animator.ResetTrigger("Fear");
                animator.ResetTrigger("Attack");

                animator.SetTrigger("Dead");

                //Drop coins
                coinDropper.DropCoins(coinsDropped);

                //Add to quest if needed

                //Destroy
                Destroy(gameObject);
            }
        }
    }
}
