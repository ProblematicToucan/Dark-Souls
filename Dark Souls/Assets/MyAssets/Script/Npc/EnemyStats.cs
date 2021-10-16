using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    Animator animator;
    
    public bool isDead;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
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

        animator.Play("Attacked");

        if (currentHealth <= 0.0f)
        {
            isDead = true;
            currentHealth = 0;
            animator.Play("Death_01");
        }
    }
}
