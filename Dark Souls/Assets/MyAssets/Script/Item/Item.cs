using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class Item : ScriptableObject
    {
        [Header("Item Information")]
        public Sprite itemIcon;
        public string itemName;
    }
}