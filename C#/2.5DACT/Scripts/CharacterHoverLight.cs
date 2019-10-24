using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public class CharacterHoverLight : MonoBehaviour
    {
        public Vector3 Offset = new Vector3();

        CharacterControl HoverSelectedCharacter;
        MouseControl mouseHoverSelect;
        Vector3 TargetPos = new Vector3();
        Light light;
        // Start is called before the first frame update
        void Start()
        {
            mouseHoverSelect = GameObject.FindObjectOfType<MouseControl>();
            light = GetComponent<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            if (mouseHoverSelect.selectedCharacterType == PlayableCharacterType.NONE)
            {
                HoverSelectedCharacter = null;
                light.enabled = false;
            }
            else {
                light.enabled = true;
                LightUpSelectedCharacter();
            }
        }

        private void LightUpSelectedCharacter() {
            if (HoverSelectedCharacter == null) {
                HoverSelectedCharacter = CharacterManager.Instance.GetCharacter(mouseHoverSelect.selectedCharacterType);
                this.transform.position = HoverSelectedCharacter.SkinnedMeshAnimator.transform.position+HoverSelectedCharacter.SkinnedMeshAnimator.transform.TransformDirection(Offset);
                this.transform.parent = HoverSelectedCharacter.SkinnedMeshAnimator.transform;
            }
        }
    }

}
