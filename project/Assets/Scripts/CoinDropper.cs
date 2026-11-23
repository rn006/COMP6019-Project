using UnityEngine;

public class CoinDropper : MonoBehaviour
{
    public GameObject coinPrefab;
    public float radius = 2f;

    public void DropCoins(int coinsDropped)
    {
        for (int i = 0; i < coinsDropped; i++)
        {
            Vector3 position = transform.position;
            position.x += Random.Range(-radius, radius);
            position.z += Random.Range(-radius, radius);

            Instantiate(coinPrefab, position, Quaternion.identity);
        }
    }
}
