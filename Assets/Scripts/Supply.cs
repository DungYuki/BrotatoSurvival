using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Supply : MonoBehaviour
{
    public float Amount;
    public float DropRate;
    public AudioClip PickUpSound;
    public GameObject PopUp;
    public Color PopUpColor;
    public bool IsIncrease;

    public void PickedUp()
    {
        //Pop Up
        var _prefab = Instantiate(PopUp, transform.position, Quaternion.identity);
        _prefab.GetComponentInChildren<TextMesh>().text = String.Format(" {0} {1} {2}", IsIncrease ? "+" : "-", Amount, tag);
        _prefab.GetComponentInChildren<TextMesh>().color = PopUpColor;
        _prefab.GetComponentInChildren<Animation>().Play();
    }
}
