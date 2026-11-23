using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NewMonoBehaviourScript : MonoBehaviour
{
    int hp = 100;
    int coinsDropped = 5;
    bool canBeHit = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int _damage)
    {
        //If we can be hit
        if (canBeHit)
        {
            //Start Hit2
            StartCoroutine("Hit2");
            //Remove _damage value from hp
            hp -= _damage;
            //If hp is less than 1
            if (hp < 1)
            {

                //Drop coins

                //Add to quest if needed

                //Destroy
                Destroy(gameObject);
            }
        }
    }
}
