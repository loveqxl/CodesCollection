using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    public float startHealth = 100;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount) {
        health -= amount;

        healthBar.fillAmount = health/startHealth;

        if (health <= 0&& !isDead) {
         Die();
        }
    }

    public void Slow(float pct) {
        speed = startSpeed*(1f - pct);
    }

    void Die() {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStats.Money += worth;
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
        return;
    }


}
