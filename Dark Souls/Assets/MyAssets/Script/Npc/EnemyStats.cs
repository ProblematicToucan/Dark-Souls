using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    HealthBar healthBar;

    Animator animator;
    
    public bool isDead;

    private void Awake() {
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start() {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth = currentHealth - damage;

        healthBar.SetCurrentHealth(currentHealth);

        animator.Play("Attacked");

        if (currentHealth <= 0.0f)
        {
            isDead = true;
            currentHealth = 0;
            animator.Play("Death_01");
        }
    }
}
}