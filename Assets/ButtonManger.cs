using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class ButtonManger : MonoBehaviour
{
    [SerializeField] internal List<GameObject> buttonObjectList;
    void Start()
    {

    }

    void Update()
    {

    }

    public void GameStart()
    {
        foreach (var b in buttonObjectList)
        {
            if (b.CompareTag("Player Name"))
            {
                PlayerData playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();
                playerData.playerName = b.GetComponent<TextMeshProUGUI>().text;
                var playerNameJson = JsonUtility.ToJson(playerData);
                File.WriteAllText(GameManager.playerDataPath, playerNameJson);
            }
        }
        SceneManager.LoadScene(1);
        GameManager.IsGameStarted = true;
    }
    public void Quit() => Application.Quit();
    public void BackToMenu() => SceneManager.LoadScene(0);
}
