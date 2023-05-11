using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static List<ScoreElement> ScoreList = new List<ScoreElement>();
    public int MaxCount = 20;
    private string _filename = "/HighScore.txt";

    // Start is called before the first frame update
    void Start()
    {
        _filename = Application.persistentDataPath + _filename;
        Debug.Log(_filename);
        if(!File.Exists(_filename))
            File.Create(_filename);
        //Handle Score
        LoadScore();/*
        foreach (var item in ScoreHandler.ScoreList)
        {
            Debug.Log(item.PlayerName);
            Debug.Log(item.Score);
        }*/
    }

    public void LoadScore()
    {
        string line = string.Empty;
        ScoreList = new List<ScoreElement>();
        using (StreamReader streamReader = new StreamReader(_filename, true))
        {
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] parts = line.Split(";");
                ScoreElement p = new ScoreElement(parts[0], int.Parse(parts[1]));
                ScoreList.Add(p);
            }
        }
        while (ScoreList.Count > MaxCount)
        {
            ScoreList.RemoveAt(MaxCount);
        }
    }

    public void SaveScore()
    {
        ScoreList.Sort();
        Debug.Log("Saved");
        using (StreamWriter streamWriter = new StreamWriter(_filename))
        {
            foreach (var item in ScoreList)
            {
                streamWriter.WriteLine(item.PlayerName + ";" + item.Score);
            }
        }
    }

}
