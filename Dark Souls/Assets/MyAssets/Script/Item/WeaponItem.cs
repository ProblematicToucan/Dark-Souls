using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Weapon Idle Animations")]
        public string weaponIdle;

        [Header("1H Sword Attack Animations")]
        public string oh_Light_Attack_1;
        public string oh_Light_Attack_2;
        public string oh_Light_Attack_3;
        public string oh_Heavy_Attack_1;
    }
}