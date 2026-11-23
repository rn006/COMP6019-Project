using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NewMonoBehaviourScript : MonoBehaviour
{
    int hp = 100;
    int coinsDropped = 5;
    bool canBeHit = true;
    public CoinDropper coinDropper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("TestDeath", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TestDeath()
    {
        Hit(100);
    }

    public void Hit(int _damage)
    {
        //If we can be hit
        if (canBeHit)
        {
            //Remove _damage value from hp
            hp -= _damage;
            //If hp is less than 1
            if (hp < 1)
            {

                //Drop coins
                coinDropper.DropCoins(coinsDropped);

                //Add to quest if needed

                //Destroy
                Destroy(gameObject);
            }
        }
    }
}
