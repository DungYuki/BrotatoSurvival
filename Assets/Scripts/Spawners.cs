using UnityEngine;

public class Spawners : MonoBehaviour
{
    public float SpawningRate = 2f;
    public GameObject EnemyPrefab;
    public AudioClip Groan;
    public Transform[] SpawnPoints;
    public Player Player;
    public int MaxEnemy;

    private float LastSpawnTime;
    private static int EnemyCount = 0;
    private AudioSource _audioSource;

    void Start()
    {
        EnemyCount = 0;
        _audioSource = GetComponentInChildren<AudioSource>();
        StartGroanding();
    }
    void Update()
    {
        if (Player == null) return;

        if(EnemyCount < MaxEnemy)
        {
            if (LastSpawnTime + SpawningRate < Time.time)
            {
                var randomSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
                Instantiate(EnemyPrefab, randomSpawnPoint.position, Quaternion.identity);
                LastSpawnTime = Time.time;
                SpawningRate = 0.98f;
                EnemyCount++;
            }
        }
    }

    private void StartGroanding()
    {
        float time = Random.Range(1f, 10f);
        Invoke("Groanding", time);
    }
    private void Groanding()
    {
        float time = Random.Range(Groan.length, Groan.length + 5f);
        _audioSource.PlayOneShot(Groan);
        Invoke("Groanding", time);
    }

    public static void EnemyDie()
    {
        EnemyCount --;
    }
}
