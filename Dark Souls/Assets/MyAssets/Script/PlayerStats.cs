using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class PlayerStats : MonoBehaviour
    {
        PlayerManager playerManager;
        AnimationHandler animationHandler;

        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthBar;

        void Start() {
            playerManager = GetComponent<PlayerManager>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
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
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animationHandler.HandlePlayTargetedAnimations("Attacked", true);

            if (currentHealth <= 0.0f)
            {
                playerManager.isDeath = true;
                currentHealth = 0;
                animationHandler.HandlePlayTargetedAnimations("Death_01", true);
                
            }
        }
    }
}