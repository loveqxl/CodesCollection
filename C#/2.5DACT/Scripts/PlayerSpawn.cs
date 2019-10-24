using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {

    public class PlayerSpawn : MonoBehaviour
    {

        public CharacterSelect characterSelect;
        private string objName;
        private void Awake()
        {
            switch (characterSelect.SelectedCharacterType) {
                case PlayableCharacterType.lola: {
                        objName = "Lola";
                    }
                    break;
                case PlayableCharacterType.dummy: { 
                        objName = "Dummy";
                    }
                    break;
            }

            GameObject obj = Instantiate(Resources.Load(objName, typeof(GameObject)) as GameObject);
            obj.transform.position = this.transform.position;
            GetComponent<MeshRenderer>().enabled = false;

            Cinemachine.CinemachineVirtualCamera[] arr = GameObject.FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            foreach (Cinemachine.CinemachineVirtualCamera v in arr) {
                CharacterControl control = CharacterManager.Instance.GetCharacter(characterSelect.SelectedCharacterType);
                Collider target = control.GetBodyPart("Spine1");
                v.LookAt = target.transform;
                v.Follow = target.transform;
            }
        }

    }

}
