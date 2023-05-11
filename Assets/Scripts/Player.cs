using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _input;
    private Vector2 _hit_velocity;
    private TMPro.TextMeshProUGUI _health_hud;
    private bool _get_hit;
    private int _damage_taken;
    private Gun _gun;
    private FlashLight _flash_light;
    private Animation _animation;
    private AudioSource _audio;
    private int _health;

    public int MaxHealth;
    public float Speed;
    public int TNT;
    public bool IsPlanted;
    public GameMode GameMode;
    public GameObject Gun;
    public GameObject Halo;
    public GameObject FlashLight;
    public PackOfTNT PackOfTNT;
    public GameObject BloodPrefab;
    public GameObject HealthHud;
    public AudioClip PlayerHitSound;
    public GameObject PopUpText;

    void Start()
    {
        GameMode = FindObjectOfType<GameMode>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health_hud = HealthHud.GetComponent<TMPro.TextMeshProUGUI>();
        _get_hit = false;
        _gun = Gun.GetComponent<Gun>();
        _flash_light = FlashLight.GetComponent<FlashLight>();
        _animation = GetComponent<Animation>();
        _audio = GetComponent<AudioSource>();
        _health = MaxHealth;
        TNT = 0;
    }
    
    void Update()
    {
        if (!GameMode.IsPause)
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.up = (MouseUtils.GetMousePosition2d() - (Vector2)transform.position).normalized;
            if (!_get_hit)
            {
                _rigidbody.velocity = _input.normalized * Speed;/*
                if (Math.Abs(Input.GetAxis("Horizontal")) < 0.1f && Math.Abs(Input.GetAxis("Vertical")) < 0.1f)
                    StayStill();*/
            }
            else
            {
                _rigidbody.velocity = _hit_velocity;
            }

            //Shoot
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _gun.Shoot();
            }
            //Reload
            if ((Input.GetKeyDown(KeyCode.R) || _gun.IsOutOfMag()) && !_gun.IsReloading && _gun.CanReload())
            {
                _gun.Reload();
            }
            _health_hud.SetText("+ " + _health.ToString());
        }
        else
        {
            StayStill();
        }

    }

    public void Die()
    {
        GameMode.PlayerDie();
        Instantiate(BloodPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<TMP_Text>().enabled = true;
        _health_hud.SetText("+ " + _health.ToString());
        Instantiate(Halo, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        _health = _health - damage;
        _animation.Play("PlayerHit");
        if(_health <= 0)
        {
            _health = 0;
            Die();
        }
        else
        {
            _audio.PlayOneShot(PlayerHitSound);
        }
    }

    private void StayStill()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
        _get_hit = false;
    }

    private void Heal(int _amount)
    {
        if(_health < MaxHealth)
        {
            _health += _amount;
            if(_health >= MaxHealth)
                _health = MaxHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.collider.GetComponent<Enemy>();

        //take damage from enemy
        if (enemy != null)
        {
            _damage_taken = enemy.Damage;
            Hit(_damage_taken);
            _get_hit = true;
            _hit_velocity = ((Vector2)transform.position - (Vector2)enemy.transform.position).normalized * 10; //fallback
            Invoke("StayStill", 0.3f); //stay still
        }
        else 
            StayStill();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var blocker = collision.GetComponent<Blocker>();

        if (blocker != null)
        {
            if(GameState.TNTRequired <= TNT && !IsPlanted)
            {
                GameMode.BlockerInLevel.GetComponentInChildren<MeshRenderer>().enabled = true;
                //Plant TNT
                if (Input.GetKey(KeyCode.E))
                {
                    var packTNT = Instantiate(PackOfTNT, collision.transform.position, Quaternion.identity);
                    packTNT.transform.parent = collision.gameObject.transform;
                    TNT = 0;
                    IsPlanted = true;
                    GameMode.BlockerInLevel.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var blocker = other.GetComponent<Blocker>();

        if (blocker != null)
        {
            GameMode.Arrow.GetComponentInChildren<SpriteRenderer>().enabled = true;
            GameMode.BlockerInLevel.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var Supply = other.GetComponent<Supply>();

        //pick up item
        if (Supply != null)
        {
            if (Supply.tag == "Ammo")
                _gun.AddAmmo((int)Supply.Amount);
            if (Supply.tag == "Health")
                Heal((int)Supply.Amount);
            if (Supply.tag == "Battery")
                _flash_light.AddBattery((int)Supply.Amount);
            if (Supply.tag == "TNT")
                TNT++;
            if (Supply.tag == "Mag Size")
                _gun.Magazine += (int)Supply.Amount;
            if (Supply.tag == "Damage")
                GameState.BulletDamage += (int)Supply.Amount;
            if (Supply.tag == "Fire Rate")
                GameState.BulletSpeed += Supply.Amount;
            if (Supply.tag == "Reload Time")
                _gun.ReloadTime -= Supply.Amount;
            Supply.PickedUp();
            Destroy(other.gameObject);
            _audio.PlayOneShot(Supply.PickUpSound);
        }

        var blocker = other.GetComponent<Blocker>();

        if (blocker != null)
        {
            GameMode.Arrow.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

}
