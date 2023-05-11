using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private int _damage;
    public float Speed;

    public int Damage;
    void Start()
    {
        _damage = GameState.BulletDamage;
        Speed = GameState.BulletSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = Damage;
        Destroy(gameObject, 4f);
    }
    
    void Update()
    {
        if (!FindObjectOfType<GameMode>().IsPause)
        {
            _rigidbody.velocity = transform.up * Speed;
            if (_damage <= 0)
                Destroy(gameObject);
        }
        else
            _rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            var _dmg = enemy.MaxHealth;
            if (!enemy.Hit(_damage))
                Destroy(gameObject);
            _damage -= _dmg;
        }
    }
}
