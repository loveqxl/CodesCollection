using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Wave[] waves;

    public static int EnemiesAlive = 0;

    public Transform spawnPoint;

    private int waveIndex = 0;

    public float timeBetweenWaves = 2f;

    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {

        if (EnemiesAlive > 0) {
            return;
        }

        if (waveIndex == waves.Length && PlayerStats.Lives!=0)
        {
            gameManager.winLevel();
            this.enabled = false;
        }

        if (countdown <= 0) {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // Clamp is used to limited a number to make sure it will not go out of the range

        waveCountdownText.text = string.Format("{0:00.00}",countdown);
    }

    IEnumerator spawnWave() {
        
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            spawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        waveIndex++;

    }

    void spawnEnemy(GameObject enemy) {

        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);

    }

}
