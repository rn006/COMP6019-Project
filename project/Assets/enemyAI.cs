using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    public int currentEnemyHealth = 3;
    Transform player;
    NavMeshAgent nav;


        public enum States
    {
        idle,
        chase,
        escape
    }

    States _state = States.idle;

    


    void Awake ()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent <NavMeshAgent> ();
    }

     void Update()
    {
        if (currentEnemyHealth > 0 && player)
        {
            // Calculate distance between Ghost and Player
            float distance = Vector3.Distance(transform.position, player.position);

            // Decide which state the Ghost should be in
            if (currentEnemyHealth <= 1)
            {
                _state = States.escape;
            }
            else if (distance < 10)
            {
                _state = States.chase;
            }
            else
            {
                _state = States.idle;
            }

        
            switch (_state)
            {
                case States.idle:
                    nav.ResetPath();
                    break;

                case States.chase:
                    nav.SetDestination(player.position);
                    break;

                case States.escape:

                    // Turn away from the player
                    transform.rotation = Quaternion.LookRotation(transform.position - player.position);

                    Vector3 runTo = transform.position + transform.forward * 10;

                    nav.SetDestination(runTo);
                    break;
            }
        }
        else
        {
            // Disable the NavMeshAgent
            nav.enabled = false;
        }
    }
     
    


    
}
