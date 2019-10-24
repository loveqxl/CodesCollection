using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week6 {
    public class CharacterManager : Singleton<CharacterManager>
            {
        public List<CharacterControl> Characters = new List<CharacterControl>();

        public CharacterControl GetCharacter(PlayableCharacterType playerableType) {
            foreach (CharacterControl control in Characters) {
                if (control.playableCharacterType == playerableType) {
                    return control;
                }
            }

            return null;
        }

        public CharacterControl GetPlayer()
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.tag == "Player")
                {
                    return control;
                }
            }
            return null;
        }
        public CharacterControl GetCharacter(Animator animator) {

            foreach (CharacterControl control in Characters) {
                if (control.SkinnedMeshAnimator == animator) {
                    return control;
                }
            }
            return null;
        }
    }
}