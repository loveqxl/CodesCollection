using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    public enum PlayableCharacterType
    {
        lola,
        dummy,
        NONE,
    }

    [CreateAssetMenu(fileName ="CharacterSelect", menuName = "Week6/CharacterSelect/CharacterSelect")]
    public class CharacterSelect : ScriptableObject
    {
        public PlayableCharacterType SelectedCharacterType;
    }
}
