using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] internal GameObject enemy;
    [SerializeField] internal static float MinSpawnDelay { get; set; } = 2.0f;
    [SerializeField] internal static float MaxSpawnDelay { get; set; } = 4.0f;
    //internal static bool IsSpawned { get; set; } = false;

    void Start()
    {
        StartCoroutine(SpawnEnemy()); 
        MinSpawnDelay = 2.0f; 
        MaxSpawnDelay = 4.0f;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().PlayerHP <= 0)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemy, transform.position, enemy.transform.rotation);
            yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
            MinSpawnDelay -= 0.05f;
            MaxSpawnDelay -= 0.05f;
        }
    }
}
