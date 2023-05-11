using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerName : MonoBehaviour
{
    public void GetName()
    {
        var gameMode = FindObjectOfType<GameMode>();
        if (gameMode != null && !string.IsNullOrEmpty(GetComponent<TMP_InputField>().text))
        {
            gameMode.GetPlayerName(GetComponent<TMP_InputField>().text);
        }
    }
}
