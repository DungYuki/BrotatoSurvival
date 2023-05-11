using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public Player Player;
    public Enemy EnemyState;
    public Blocker BlockerInLevel;
    public GameObject Arrow;
    public GameObject HUD;
    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject PauseUI;
    public GameObject MessageUI;
    public TMPro.TextMeshProUGUI[] ScoreHud;
    public TMPro.TextMeshProUGUI DamageHud;
    public TMPro.TextMeshProUGUI FireRateHud;
    public TMPro.TextMeshProUGUI ReloadTimeHud;
    public TMPro.TextMeshProUGUI MagSizeHud;
    public TMPro.TextMeshProUGUI TNTHud;

    public float GunReloadTimeMin;
    public int ScorePerKill;
    public int KPI;
    public static int EnemyKill;
    public float EnemyMaxSpeed;

    public bool IsPause;
    public bool IsOver;
    public bool IsStart;

    private Player _player;
    private Enemy _enemy;
    private Gun _gun;
    private int _score;
    private string _playerName;
    private ScoreHandler _score_handle;

    // Start is called before the first frame update
    void Start()
    {
        Arrow.SetActive(false);
        _player = Player.GetComponent<Player>();
        _gun = Player.GetComponentInChildren<Gun>();
        EnemyKill = 0;
        _score = 0;
        _score_handle = GetComponent<ScoreHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in ScoreHud)
        {
            item.SetText("Score: " + _score);
        }
        DamageHud.SetText("Damage: " + GameState.BulletDamage);
        FireRateHud.SetText("Fire Rate: " + GameState.BulletSpeed);
        ReloadTimeHud.SetText("Reload Speed: " + _gun.ReloadTime);
        MagSizeHud.SetText("Mag Size: " + _gun.Magazine);
        TNTHud.SetText("TNT: " + _player.TNT + "/" + GameState.TNTRequired);

        if (_gun.ReloadTime <= GunReloadTimeMin)
        {
            FindSupply("Reload Time").DropRate = 0;
        }
        if(_player.TNT >= GameState.TNTRequired)
        {
            Arrow.SetActive(true);
        }
        if(GameState.EnemySpeed >= EnemyMaxSpeed)
            GameState.EnemySpeed = EnemyMaxSpeed;

        //Pause Game
        if (Input.GetKeyDown(KeyCode.Escape) && !IsOver)
        {
            if(IsPause)
                Resume();
            else
                Pause();
        }

        if (!IsStart)
        {
            var BlackScreenTime = GameObject.Find("BlackScreen").GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
            _gun.GetComponent<AudioSource>().PlayOneShot(_gun.DrawSound);
            Invoke("StartGame", BlackScreenTime);
            IsStart = true;
        }        
    }

    private void StartGame()
    {
        IsPause = false;
        IsOver = false;
        PauseUI.SetActive(false);
        HUD.SetActive(true);
        GameOverUI.SetActive(false);
        WinUI.SetActive(false);
        MessageUI.SetActive(false);

    }

    private Supply FindSupply(string _tag_name)
    {
        foreach (var item in EnemyState.GetComponent<DropSupply>().SupplyPrefab)
        {
            if(item.tag == _tag_name)
                return item;
        }
        return null;
    }

    public void Score()
    {
        EnemyKill++;
        _score += ScorePerKill;

        if (EnemyKill % KPI == 0)
        {
            KPI += KPI;
            ScorePerKill += ScorePerKill;
            GameState.EnemyHealth += GameState.EnemyHealthRate;
            GameState.EnemySpeed += GameState.EnemySpeedRate;
        }
    }

    public void PlayerDie()
    {
        IsOver = true;
        IsPause = true;
        PauseUI.SetActive(false);
        HUD.SetActive(false);
        GameOverUI.SetActive(true);
        WinUI.SetActive(false);
        MessageUI.SetActive(false);
    }

    public void PlayerEscape()
    {
        IsOver = true;
        IsPause = true;
        PauseUI.SetActive(false);
        HUD.SetActive(false);
        WinUI.SetActive(true);
        MessageUI.SetActive(false);
    }

    public void RePlay()
    {
        AddScore();
        _score_handle.SaveScore();
        SceneManager.LoadScene("MainLevel");
    }

    public void Pause()
    {
        foreach (var item in GameObject.FindObjectsOfType<AudioSource>())
            item.Pause();
        IsPause = true;
        PauseUI.SetActive(true);
        HUD.SetActive(false);
        GameOverUI.SetActive(false);
        WinUI.SetActive(false);
        MessageUI.SetActive(false);
    }

    public void Resume()
    {
        foreach (var item in GameObject.FindObjectsOfType<AudioSource>())
            item.UnPause();
        IsPause = false;
        PauseUI.SetActive(false);
        HUD.SetActive(true);
        GameOverUI.SetActive(false);
        WinUI.SetActive(false);
        MessageUI.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AddScore()
    {
        if (!string.IsNullOrEmpty(_playerName))
        {
            Debug.Log("Added");
            ScoreElement scoreElement = new ScoreElement(_playerName, _score);
            ScoreHandler.ScoreList.Add(scoreElement);
        }
    }
    public void GetPlayerName(string PlayerName)
    {
        _playerName = PlayerName;
    }
}
