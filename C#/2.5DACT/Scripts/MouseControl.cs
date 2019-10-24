using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Week6 {
    public class MouseControl : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;
        CharacterSelectLight characterSelectLight;
        CharacterHoverLight characterHoverLight;
        GameObject selectCircle;
        Animator characterSelectCameraAnimator;

        private void Awake()
        {
            characterSelect.SelectedCharacterType = PlayableCharacterType.NONE;
            characterSelectLight = GameObject.FindObjectOfType<CharacterSelectLight>();
            characterHoverLight = GameObject.FindObjectOfType<CharacterHoverLight>();
            characterSelectCameraAnimator = GameObject.Find("SelectCameraController").GetComponent<Animator>();
            selectCircle = GameObject.Find("SelectCircle");
            selectCircle.SetActive(false);
        }
        void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                CharacterControl control = hit.collider.gameObject.GetComponent<CharacterControl>();
                if (control != null)
                {
                    selectedCharacterType = control.playableCharacterType;
                }
                else {

                        selectedCharacterType = PlayableCharacterType.NONE;

                }
            }

            if (Input.GetMouseButtonDown(0)) {

                if (hit.collider==null)
                {
                    return;
                }
                if (selectedCharacterType != PlayableCharacterType.NONE)
                {
                    characterSelect.SelectedCharacterType = selectedCharacterType;
                    CharacterControl control = CharacterManager.Instance.GetCharacter(selectedCharacterType);
                    characterSelectLight.transform.position = characterHoverLight.transform.position;                    
                    characterSelectLight.transform.parent = control.SkinnedMeshAnimator.transform;
                    characterSelectLight.light.enabled = true;
                    selectCircle.SetActive(true);
                    selectCircle.transform.parent = control.SkinnedMeshAnimator.transform;
                    selectCircle.transform.localPosition = new Vector3(0f, 0f, 0f);

                }
                else {
                    characterSelect.SelectedCharacterType = PlayableCharacterType.NONE;
                    characterSelectLight.light.enabled = false;
                    selectCircle.SetActive(false);
                }

                foreach (CharacterControl c in CharacterManager.Instance.Characters) {
                    if (c.playableCharacterType == selectedCharacterType)
                    {
                        c.SkinnedMeshAnimator.SetBool(TransitionParameter.Select.ToString(), true);
                    }
                    else {
                        c.SkinnedMeshAnimator.SetBool(TransitionParameter.Select.ToString(), false);
                    }
                }
                characterSelectCameraAnimator.SetBool(selectedCharacterType.ToString(),true);


            }

        }
    }
}

