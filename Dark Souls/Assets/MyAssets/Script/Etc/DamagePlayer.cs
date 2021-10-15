using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player")
            {
                PlayerStats playerStats = other.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage);
                }
            }
        }
    }
}