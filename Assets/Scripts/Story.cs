using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public GameObject[] page;
    public Button Continue;
    public Button Skip;
    private GameObject curent_page;
    private GameObject next_page;
    public bool IsEndPage;

    private void Start()
    {
        UpdatePage();
    }
    void UpdatePage()
    {
        for (int i = 0; i < page.Length; i++)
        {
            if (page[i].activeInHierarchy)
            {
                curent_page = page[i];
                if (i == page.Length - 1)
                {
                    Continue.gameObject.SetActive(false);
                    Skip.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                    break;
                }
                next_page = page[i + 1];
            }
        }
    }

    public void NextPage()
    {        
        curent_page.SetActive(false);
        next_page.SetActive(true);
        UpdatePage();
    }
}
