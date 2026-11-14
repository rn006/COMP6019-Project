using UnityEngine;

public class bomber : MonoBehaviour
{
    public GameObject explosion;
    float timer = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // Create explosion effect
            Instantiate(explosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }

    }
}
