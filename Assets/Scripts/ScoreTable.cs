using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts;
using TMPro;
using Unity.VisualScripting;

public class ScoreTable : MonoBehaviour
{
    public GameObject Content;
    public GameObject ScoreElementPrefab;
    private ScrollRect scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();        
    }

    private void OnEnable()
    {
        foreach (var item in ScoreHandler.ScoreList)
        {
            var scoreElement = Instantiate(ScoreElementPrefab, Vector2.zero, Quaternion.identity);
            scoreElement.gameObject.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = item.PlayerName;
            scoreElement.gameObject.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = item.Score.ToString();
            scoreElement.transform.SetParent(Content.transform);
            scoreElement.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollRect != null && scrollRect.IsActive() && Input.GetAxis("Mouse ScrollWheel") != 0)
            scrollRect.verticalScrollbar.value += Input.GetAxis("Mouse ScrollWheel") * 100f * Time.smoothDeltaTime;
    }
}
