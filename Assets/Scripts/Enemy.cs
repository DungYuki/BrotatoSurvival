using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Player _player;
    private float _speed;
    private AudioSource _audioSource;
    private Animation _animation;
    private GameMode _gameMode;

    public int Damage;
    public float BaseSpeed;
    public int MaxHealth;
    public GameObject BloodPrefab;
    public AudioClip Groan;
    public AudioClip HitSound;
    public AudioClip GetHitSound;


    void Start()
    {
        BaseSpeed = GameState.EnemySpeed;
        MaxHealth = GameState.EnemyHealth;
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _animation = GetComponent<Animation>();
        _speed = BaseSpeed * (1 + Random.Range(-0.1f, 0.1f));
        _player = FindObjectOfType<Player>();
        _gameMode = FindObjectOfType<GameMode>();
    }
    
    void Update()
    {
        if (!FindObjectOfType<GameMode>().IsPause)
        {            
            if (_player != null)
            {
                transform.up = (_player.transform.position - transform.position).normalized;
                _rigidbody.velocity = transform.up * _speed;
            }
        }
        else
            _rigidbody.velocity = Vector2.zero;

    }

    public bool Hit(int _damage)
    {
        MaxHealth -= _damage;
        _animation.Play("EnemyHit");
        if (MaxHealth <= 0)
        {
            MaxHealth = 0;
            Die();
            return true;
        }
        else
        {
            _audioSource.PlayOneShot(GetHitSound);
            return false;
        }
    }

    public void Die()
    {
        Instantiate(BloodPrefab, transform.position, Quaternion.identity);
        GetComponent<DropSupply>().DropASupply();
        Destroy(gameObject);
        Spawners.EnemyDie();
        _gameMode.Score();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var Player = other.collider.GetComponent<Player>();
        if(Player != null)
        {
            _audioSource.PlayOneShot(HitSound);
        }
    }

}
