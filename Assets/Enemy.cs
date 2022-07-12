using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    internal GameObject player;
    [SerializeField] internal List<GameObject> itemList;
    [SerializeField] internal float TraceSpeed { get; set; } = 10f;
    internal static bool IsDestroyed { get; set; } = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (GameManager.IsGameStarted)
        {
            if (Random.Range(1, 11) == 1)
            {
                TraceSpeed = Random.Range(1, 46);
            }
            transform.Translate(Time.deltaTime * TraceSpeed * (player.transform.position - transform.position).normalized);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            TextManager.ScoreText++;
            if (Random.Range(1, 3) == 1)
            {
                Instantiate(itemList[0], transform.position, itemList[0].transform.rotation);
            }
            else if (Random.Range(1, 31) == 1)
            {
                Instantiate(itemList[1], transform.position, itemList[1].transform.rotation);
            }
            PlayerData playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();
            playerData.maxScore = TextManager.ScoreText;
            var playerNameJson = JsonUtility.ToJson(playerData);
            File.WriteAllText(GameManager.playerDataPath, playerNameJson);

        }
    }
}
