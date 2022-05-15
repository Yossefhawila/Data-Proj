using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    public string playerName;
    public float Score;

    public PlayerData NowplayerData;

    [SerializeField]
    private TextMeshProUGUI HighScoreLabel;

    [SerializeField]
    private TextMeshProUGUI NameField;

    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button ExitButton;





    private void Awake()
    {
        StartButton = GameObject.Find("Start").GetComponent<Button>();
        ExitButton = GameObject.Find("Exit").GetComponent<Button>();
        NameField = GameObject.Find("TextField").GetComponent<TextMeshProUGUI>();
        HighScoreLabel = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        StartButton.onClick.AddListener(StartGame);
        ExitButton.onClick.AddListener(ExitGame);
        //NameField = GameObject.Find("TextField").GetComponent<TextMeshProUGUI>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (File.Exists(Application.persistentDataPath + "/jsonData.json"))
        {
            loadData();
        }
        else
        {
            PlayerData playerData = new PlayerData();
        }





    }
    public void StartGame()
    {
        playerName = NameField.text;
        SceneManager.LoadScene(1);
        
        
    }

    public class PlayerData
    {
       public string Name;
       public float Score;
    }
    public void saveData(string _PlayerName,float _Score)
    {
        PlayerData playerData = new PlayerData();
        playerData.Name = _PlayerName;
        playerData.Score = _Score;
        if (File.Exists(Application.persistentDataPath + "/jsonData.json"))
        {
            PlayerData LastPlayerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/jsonData.json"));
            if (playerData.Score > LastPlayerData.Score)
            {
                File.WriteAllText(Application.persistentDataPath + "/jsonData.json", JsonUtility.ToJson(playerData));
            }
            
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/jsonData.json", JsonUtility.ToJson(playerData));
        }
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/jsonData.json"))
        {
            NowplayerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/jsonData.json"));
            HighScoreLabel.text = "HighScore : "+ NowplayerData.Name+":"+ NowplayerData.Score;
        }
    }
    public string GetHighScore(PlayerData playerData)
    {
        return "HighScore : "+playerData.Name+":"+playerData.Score;
    }

    public void ExitGame()
    {

      #if UNITY_EDITOR

            UnityEditor.EditorApplication.ExitPlaymode();
      #else
            Application.Quit();
      #endif
       
    }
}
