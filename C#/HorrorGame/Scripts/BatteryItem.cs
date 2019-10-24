using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorrorGame
{
    [CreateAssetMenu(fileName = "New BatteryItem", menuName = "HorrorGame/BatteryItem")]
    public class BatteryItem : Item
    {
        private void Awake()
        {
            canBeRemoved = false;
            Count = 0;
        }
    }
}
