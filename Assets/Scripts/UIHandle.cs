using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandle : MonoBehaviour
{
    public Animator Background;
    public TextMeshProUGUI Mode;

    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("Mode"))
        {
            case 0: EasyMode(); break;
            case 1: MediumMode(); break;
            case 2: HardMode(); break;
            case 3: NightmareMode(); break;
            default: EasyMode(); break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        if(Background != null)
        {
            Background.SetTrigger("IsStart");
            var delayTime = Background.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ChangeScene", delayTime);
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void EasyMode()
    {
        float[] dropRate = new float[8] {8, 40, 30, 20,5,5,5,5};
        GameState.DropRate = dropRate;
        GameState.TNTRequired = 5;
        GameState.EnemySpeed = 1.5f;
        GameState.EnemySpeedRate = 0.5f;
        GameState.EnemyHealth = 15;
        GameState.EnemyHealthRate = 5;
        GameState.BulletDamage = 20;
        GameState.BulletSpeed = 17;
        if (Mode != null)
        {
            Mode.text = "Easy";
            Mode.color = new Color(0, 1, 0, 1);
        }
        PlayerPrefs.SetInt("Mode", 0);
        PlayerPrefs.Save();
    }
    public void MediumMode()
    {
        float[] dropRate = new float[8] { 8, 30, 20, 20, 3, 3, 3, 3 };
        GameState.DropRate = dropRate;
        GameState.TNTRequired = 5;
        GameState.EnemySpeed = 1.5f;
        GameState.EnemySpeedRate = 0.5f;
        GameState.EnemyHealth = 20;
        GameState.EnemyHealthRate = 5;
        GameState.BulletDamage = 20;
        GameState.BulletSpeed = 17;
        if (Mode != null)
        {
            Mode.text = "Medium";
            Mode.color = new Color(1, 1, 0, 1);
        }
        PlayerPrefs.SetInt("Mode", 1);
        PlayerPrefs.Save();
    }
    public void HardMode()
    {
        float[] dropRate = new float[8] { 8, 30, 20, 15, 2, 2, 2, 2 };
        GameState.DropRate = dropRate;
        GameState.TNTRequired = 8;
        GameState.EnemySpeed = 2f;
        GameState.EnemySpeedRate = 0.5f;
        GameState.EnemyHealth = 25;
        GameState.EnemyHealthRate = 10;
        GameState.BulletDamage = 20;
        GameState.BulletSpeed = 19;
        if (Mode != null)
        {
            Mode.text = "Hard";
            Mode.color = new Color(1, 0.5f, 0, 1);
        }
        PlayerPrefs.SetInt("Mode", 2);
        PlayerPrefs.Save();
    }

    public void NightmareMode()
    {
        float[] dropRate = new float[8] { 8, 30, 15, 10, 1, 1, 1, 1 };
        GameState.DropRate = dropRate;
        GameState.TNTRequired = 10;
        GameState.EnemySpeed = 2f;
        GameState.EnemySpeedRate = 1f;
        GameState.EnemyHealth = 30;
        GameState.EnemyHealthRate = 15;
        GameState.BulletDamage = 25;
        GameState.BulletSpeed = 19;
        if (Mode != null)
        {
            Mode.text = "Nightmare";
            Mode.color = new Color(1, 0, 0, 1);
        }
        PlayerPrefs.SetInt("Mode", 3);
        PlayerPrefs.Save();
    }
}
