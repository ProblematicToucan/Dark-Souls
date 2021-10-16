using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class PlayerAttacker : MonoBehaviour
    {
        PlayerManager playerManager;
        AnimationHandler animationHandler;
        InputHandler inputHandler;
        PlayerInventory playerInventory;

        [HideInInspector]
        // Attack & action interval
        public float nextAttackTime = 0;

        [SerializeField]
        string lastAttack;

        bool lightAttack;
        bool heavyAttack;

        [SerializeField]
        float lightAttackRate = 2.5f;
        [SerializeField]
        float heavyAttackRate = 1f;

        private void Awake() {
            playerManager = GetComponent<PlayerManager>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            inputHandler = GetComponent<InputHandler>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        void HandleLightAttack(WeaponItem weapon)
        {
            animationHandler.HandlePlayTargetedAnimations(weapon.oh_Light_Attack_1, true);
            lastAttack = weapon.oh_Light_Attack_1;
        }

        void HandleHeavyAttack(WeaponItem weapon)
        {
            animationHandler.HandlePlayTargetedAnimations(weapon.oh_Heavy_Attack_1, true);
            lastAttack = weapon.oh_Heavy_Attack_1;
        }

        void HandleWeaponCombo(WeaponItem weapon)
        {
            animationHandler.anim.SetBool("CanCombo", false);
            
            if (lastAttack == weapon.oh_Light_Attack_1)
            {
                animationHandler.HandlePlayTargetedAnimations(weapon.oh_Light_Attack_2, true);
                inputHandler.rb_Input = false;
                nextAttackTime = Time.time + 1f / lightAttackRate;
                lastAttack = weapon.oh_Light_Attack_2;
            }
            else if (lastAttack == weapon.oh_Light_Attack_2)
            {
                animationHandler.HandlePlayTargetedAnimations(weapon.oh_Light_Attack_3, true);
                inputHandler.rb_Input = false;
                nextAttackTime = Time.time + 1f / lightAttackRate;
            }
        }

        public void HandlePlayerAttack(float delta)
        {
            lightAttack = inputHandler.rb_Input;
            heavyAttack = inputHandler.rt_Input;

            if (Time.time >= nextAttackTime)
            {
                if (lightAttack)
                {
                    if (playerManager.canCombo)
                    {
                        HandleWeaponCombo(playerInventory.rightWeapon);
                    }
                    else
                    {
                        HandleLightAttack(playerInventory.rightWeapon);
                        inputHandler.rb_Input = false;
                        nextAttackTime = Time.time + 1f / lightAttackRate;
                    }
                }
                
                if (heavyAttack)
                {
                    HandleHeavyAttack(playerInventory.rightWeapon);
                    inputHandler.rt_Input = false;
                    nextAttackTime = Time.time + 1f / heavyAttackRate;
                }
            }
        }
    }
}