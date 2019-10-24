using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/SpawnObjects")]
    public class SpawnObjects : StateData
    {
        public PoolObjectType ObjectType;
        [Range(0f, 1f)]
        public float SpawnTiming;
        public string ParentObjectName = string.Empty;
        public bool StickToParent;

        //private bool IsSpawn;
        private CharacterControl control;


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0f) {
                control = characterState.GetCharacterControl(animator);
                SpawnObject(control);
            }
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!control.animationProgress.PoolObjectList.Contains(ObjectType)) {
                if (stateInfo.normalizedTime >= SpawnTiming) {
                    SpawnObject(control);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.animationProgress.PoolObjectList.Contains(ObjectType)) {
                control.animationProgress.PoolObjectList.Remove(ObjectType);
            }
        }

        public void SpawnObject(CharacterControl control) {

            if (control.animationProgress.PoolObjectList.Contains(ObjectType)) {
                return;
            }

            GameObject obj = PoolManager.Instance.GetObject(ObjectType);
            if (!string.IsNullOrEmpty(ParentObjectName)) {
                GameObject p = control.GetChildObj(ParentObjectName);
                obj.transform.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }
            if (!StickToParent) {
                obj.transform.parent = null;
                }

            obj.SetActive(true);
            control.animationProgress.PoolObjectList.Add(ObjectType);
        }
    }
}

