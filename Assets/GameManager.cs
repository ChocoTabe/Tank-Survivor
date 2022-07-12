using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class GameManager : MonoBehaviour
{
    public static bool IsGameStarted = false;
    internal static string playerDataPath = "";
    internal Player player;
    private void Awake()
    {
        playerDataPath = $@"{Application.persistentDataPath}\playerData.json";
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        var loadJson = File.ReadAllText(playerDataPath);
        var playerName = JsonUtility.FromJson<SavePlayerData>(loadJson).playerName;
        var maxScore = JsonUtility.FromJson<SavePlayerData>(loadJson).maxScore;
        if (SceneManager.GetActiveScene().buildIndex == 0 && File.Exists(playerDataPath))
        {
            GameObject.Find("Max Score").GetComponent<TextMeshProUGUI>().text = $"{playerName}'s Max Score: {maxScore}";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && File.Exists(playerDataPath))
        {
            PlayerData playerData = GameObject.Find("Player Data").GetComponent<PlayerData>();
            playerData.playerName = playerName;
        }
#if UNITY_EDITOR
        if (EditorSceneManager.loadedSceneCount == 1)
        {
            IsGameStarted = true;
        }
#endif
    }

    void Update()
    {
        if (IsGameStarted && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (player.PlayerHP <= 0)
            {
                GameObject.Find("Game Over").GetComponent<TextMeshProUGUI>().enabled = true;
                GameObject.Find("Restart").GetComponent<TextMeshProUGUI>().enabled = true;
                Restart();
            } 
        }
    }
    void Restart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}


[System.Serializable]
public class SavePlayerData
{
    public string playerName;
    public int maxScore;
}
