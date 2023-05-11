using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //public int Damage;
    public int Magazine;
    public int Clip;
    public float ReloadTime;
    public GameObject AmmoHud;
    public GameObject ReloadingHud;
    public bool IsReloading;
    public bool ReadyToShoot;
    public AudioClip GunSound;
    public AudioClip ReloadSound;
    public AudioClip OutOfAmmo;
    public AudioClip DrawSound;
    public GameObject Crosshair;

    public GameObject BulletPrefab;

    private TMPro.TextMeshProUGUI _ammo_hud;
    private TMPro.TextMeshProUGUI _reloading_hud;
    private int _ammo;
    private int _bullet_shot;
    private AudioSource _audio;

    private float _audio_reload_rate;
    private float PauseTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _ammo_hud = AmmoHud.GetComponent<TMPro.TextMeshProUGUI>();
        _reloading_hud = ReloadingHud.GetComponent<TMPro.TextMeshProUGUI>();
        _ammo = Magazine;
        _bullet_shot = 0;
        ReadyToShoot = true;
        IsReloading = false;
        _audio = GetComponent<AudioSource>();
        _audio_reload_rate = ReloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Set UI
        _ammo_hud.SetText(_ammo + " /" + Clip);

        if (FindObjectOfType<GameMode>().IsPause)
        {
            PauseTime += Time.deltaTime;
            CancelInvoke("ReloadFinish");
        }
        else
        {
            if(IsReloading && PauseTime > 0)
                Invoke("ReloadFinish", ReloadTime - PauseTime);
            PauseTime = 0;
        }
    }

    public void Shoot()
    {
        if (ReadyToShoot)
        {
            Crosshair.GetComponent<Animator>().SetTrigger("Fire");
            if (!IsOutOfAmmo() && _ammo > 0 && !IsReloading)
            {
                Instantiate(BulletPrefab, transform.position, transform.rotation);
                _audio.PlayOneShot(GunSound);
                _ammo--;
                _bullet_shot++;
            }
            else if (IsOutOfAmmo())
            {
                _audio.PlayOneShot(OutOfAmmo);
            }
        }      
    }

    public bool IsOutOfAmmo()
    {
        if (_ammo <= 0 && Clip <= 0)
            return true;
        return false;
    }

    public bool IsOutOfMag()
    {
        if(_ammo <= 0 && Clip > 0)
            return true;
        return false;
    }

    public bool CanReload()
    {
        if (_ammo >= 0 && _ammo < Magazine && Clip > 0)
            return true;
        return false;
    }

    public void Reload()
    {
        _audio.PlayOneShot(ReloadSound);
        _audio.pitch = _audio_reload_rate/ReloadTime;
        IsReloading = true;
        ReadyToShoot = false;
        _reloading_hud.enabled = true;
        Invoke("ReloadFinish", ReloadTime);
    }

    public void AddAmmo(int _ammo)
    {
        Clip += _ammo;
    }

    private void ReloadFinish()
    {
        _audio.pitch = 1;
        AmmoCalculate();
        IsReloading = false;
        ReadyToShoot = true;
        _reloading_hud.enabled = false;
    }

    private void AmmoCalculate()
    {        
        if (Magazine > Clip)
        {
            if(_ammo + Clip < Magazine)
            {
                _ammo += Clip;
                Clip = 0;
            }
            else
            {
                _ammo += _bullet_shot;
                Clip -= _bullet_shot;
            }
        }
        else
        {
            _ammo = Magazine;
            Clip -= _bullet_shot;
        }
        _bullet_shot = 0;
    }
}
