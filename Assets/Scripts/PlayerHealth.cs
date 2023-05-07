using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public static bool isDead = false;

    public Image healthBar;
    bool once = true;

    private void Start()
    {
        isDead = false;
        healthBar.fillAmount = health / 100;
    }

    private void Update()
    {
        if (health <= 0f)
        {
            if (once)
            {
                isDead = true;
                GameManager.LOST = true;
                Destroy(gameObject, 2f);
                once = false;
            }
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = health / 100f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();
    }
}