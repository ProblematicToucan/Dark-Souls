using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public WeaponItem leftWeapon;
        public WeaponItem rightWeapon;

        private void Awake() {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start() {
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        }
    }
}