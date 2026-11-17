
using UnityEngine;

public class explode : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.currentHealth--;

    
                if (player.currentHealth <= 0)
                {
                    Destroy(player.gameObject);
                }
            }
        }

        if (other.CompareTag("Enemy"))
        {
            enemyAI enemy = other.GetComponent<enemyAI>();

            if (enemy != null)
            {
                enemy.currentEnemyHealth--;


                if (enemy.currentEnemyHealth <= 0)
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }
}
