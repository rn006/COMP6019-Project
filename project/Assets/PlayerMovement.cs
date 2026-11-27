using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;    // The speed that the player will move at. 
       Vector3 movement;      // The vector to store the direction of the player's movement 
       Rigidbody playerRigidbody;      // Reference to the player's rigidbody. 
    
    Animator anim;

    //for bomb
    public int noBomb = 3;
    public GameObject bombPrefab;

    public int currentHealth = 3;

    // Use this for initialization 
    void Start()
    {
        // Set up references. 
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }



    private void Update()
    {   // Store the input axes. 
             float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene. 
        Move(h, v);
        Turning();
        Animating(h, v);
        dropBomb();
        Attack();
    }



    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input. 
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second 
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement. 
        playerRigidbody.MovePosition(transform.position + movement);
    }



    void Turning()
    {
        if (Input.GetKey(KeyCode.W))
        { //Up  
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        { //Down
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        { //Left
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        { //Right
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero. 
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking. 
        anim.SetBool("isWalking", walking);
    }

    void dropBomb()
    {
        if (noBomb > 0 && Input.GetKeyUp(KeyCode.Space))
        { //Drop bomb 


            Instantiate(bombPrefab, new Vector3(GetComponent<Transform>().position.x, bombPrefab.transform.position.y, GetComponent<Transform>().position.z), bombPrefab.transform.rotation);


        }
    }

    // for testing attack
    public int attackDamage = 25;
    public float attackRange = 2f;
    bool poisoned = false;
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Find all snakes in the scene
            GameObject[] snakes = GameObject.FindGameObjectsWithTag("Snake");

            foreach (GameObject snake in snakes)
            {
                // Calculate distance between player and snake
                float distance = Vector3.Distance(
                    transform.position, snake.transform.position);

                // If the snake is within attack range
                if (distance <= attackRange)
                {
                    snake.GetComponent<Snake>().Hit(attackDamage);
                }
            }
        }
    }
    public void Hit(int _damage)
    {
        // Remove damage from player health
        currentHealth -= _damage;

        Debug.Log("Player HP: " + currentHealth);

        // Check if player has died
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Debug.Log("Player has died");

            // Add player death functionality here
        }
    }
    public void Poison(int _damage)
    {
        // Check if player is not already poisoned
        if (!poisoned && currentHealth > 0)
        {
            StartCoroutine(PoisonDamage(_damage));
        }
    }

    IEnumerator PoisonDamage(int _damage)
    {
        poisoned = true;

        // Wait 1.5 seconds before dealing poison damage
        yield return new WaitForSeconds(1.5f);

        if (currentHealth > 0)
        {
            Hit(_damage);
        }

        // Wait remaining duration of poison
        yield return new WaitForSeconds(1f);

        poisoned = false;
    }
}

