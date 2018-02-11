using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private int Coins = 0;
    public int GetCoins { get { return Coins; } }
    public void AddCoins(int value) { Coins += value; SaveGame(); }

	void Start () {
        Instance = this;
        LoadGame();
    }
	
	void Update () {

	}

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public static void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void SaveGame()
    {
#if !UNITY_WEBGL
        StreamWriter stream = new StreamWriter(Application.persistentDataPath + "/GameSave.sav");
        BinaryFormatter bin = new BinaryFormatter();
        bin.Serialize(stream.BaseStream, new GameSave(this));
        stream.Close();
#endif
    }

    public void LoadGame()
    {
#if !UNITY_WEBGL
        if (File.Exists(Application.persistentDataPath + "/GameSave.sav"))
        {
            StreamReader stream = new StreamReader(Application.persistentDataPath + "/GameSave.sav");
            BinaryFormatter bin = new BinaryFormatter();
            GameSave gameSave = bin.Deserialize(stream.BaseStream) as GameSave;
            stream.Close();

            Coins = gameSave.Coins;
        }
#endif
    }
}

[System.Serializable]
public class GameSave
{
    public int Coins;

    public GameSave(GameManager manager)
    {
        Coins = manager.GetCoins;
    }
}
