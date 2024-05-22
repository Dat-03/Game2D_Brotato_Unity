using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    public Slider HealthSlider;

    public HealthBar healthBar;

    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;

    public bool camShake = false;
    public string name;
    public GameObject gold;
    public bool isboss = false;
    private void Start()
    {
        currentHealth = maxHealth;
        HealthSlider.value = currentHealth;
        HealthSlider.maxValue = maxHealth;
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.tag == "Enemy")
                {
                    FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);
                    FindObjectOfType<Killed>().UpdateKilled();
                    FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4), name);
                    Destroy(this.gameObject, 0.125f);
                }
                isDead = true;
                if (isboss)
                {
                    Vector3 currentPosition = transform.position;
                    Instantiate(gold, currentPosition, Quaternion.identity);
                    Instantiate(gold, currentPosition, Quaternion.identity);
                    Instantiate(gold, currentPosition, Quaternion.identity);
                    Instantiate(gold, currentPosition, Quaternion.identity);
                    Instantiate(gold, currentPosition, Quaternion.identity);
                }
            }

            // Nếu là player thì cập nhật thanh máu
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }

    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
        HealthSlider.value = currentHealth;
        HealthSlider.maxValue = maxHealth;
    }
}
