using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    public class SpellManager : Singleton<SpellManager>
    {
        public List<SpellInfo> CurrentSpells = new List<SpellInfo>();
    }
}

