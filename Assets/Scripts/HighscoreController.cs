using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class HighscoreController : MonoBehaviour
{
    public string highscoreFilePath = "/highscore.bin";
    public HighscoreList myHighscoreListObj = new HighscoreList();
    public PlayerStats myPlayerStats;
    public Text inputField, highscoreBoardText;

    void Start()
    {
        if (HighscoreFileExists())
            LoadHighscoreFile();
        else
            SaveMyHighScoreList();
    }

    bool HighscoreFileExists()
    {
        bool fileExists = false;
        if (File.Exists(Application.persistentDataPath + highscoreFilePath))
            fileExists = true;
        return fileExists;
    }

    void LoadHighscoreFile()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + highscoreFilePath, FileMode.Open);
            HighscoreList load = (HighscoreList)bf.Deserialize(file);
            myHighscoreListObj = load;
            file.Close();
        }
        catch (System.Runtime.Serialization.SerializationException)
        {
            Debug.Log("Highscorefilen är tom!");
        }
    }

    public void SaveMyHighScoreList()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + highscoreFilePath);
        bf.Serialize(file, myHighscoreListObj);
        file.Close();
    }

    public void SaveHighscoreToHighscoreList()
    {
        string myName = inputField.text.ToString();
        MyHighscore myHighscore = new MyHighscore();
        myHighscore.myName = myName;
        myHighscore.totalKills = myPlayerStats.totalKills;
        myHighscore.coins = myPlayerStats.playerTreasure;
        myHighscoreListObj.myHighscoreList.Add(myHighscore);
    }

    public void SortHighscoreList()
    {
        myHighscoreListObj.myHighscoreList = myHighscoreListObj.myHighscoreList.OrderByDescending(x=>x.coins).ToList();
    }

    public string GetHighscoreTextFromList()
    {
        
        string highscoreText = "";
        foreach (MyHighscore myHighscore in myHighscoreListObj.myHighscoreList)
        {
            highscoreText += myHighscore.myName.ToString();
            highscoreText += "\t\t\t";
            highscoreText += myHighscore.coins.ToString();
            highscoreText += "\n";
        }
        return highscoreText;
    }

    public void ClearHighscore()
    {
        myHighscoreListObj.myHighscoreList.Clear();
        SaveMyHighScoreList();
        highscoreBoardText.text = "";
    }
}
