using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public List<GameObject> textObjectList;
    internal static int ScoreText { get; set; }
    void Start()
    {
        ScoreText = 0;
    }

    void Update()
    {
        if (GameManager.IsGameStarted)
        {
            foreach (var t in textObjectList)
            {
                if (t.name == "Score")
                {
                    t.GetComponent<TextMeshProUGUI>().text = $"Score: {ScoreText}";
                }
                if (t.name == "Player HP")
                {
                    var player = GameObject.Find("Player").GetComponent<Player>();
                    t.GetComponent<TextMeshProUGUI>().text = $"HP: {Mathf.Round(player.PlayerHP)}";
                }
            }
        }
    }
}
