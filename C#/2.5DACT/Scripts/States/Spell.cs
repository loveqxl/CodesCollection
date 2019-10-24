using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    [CreateAssetMenu(fileName = "New State", menuName = "Week6/AbilityData/Spell")]
    public class Spell : StateData
    {
        public bool debug;

        public SpellTypeParameters spellType;
        public float dirWaitTime;
        public float atkWaitTime;
        public List<TransitionParameter> inputsList;

        public GameObject bullet;
        public GameObject handpoint;
        public GameObject firepoint;
        public float firetime;
        public float flySpeed;
        public bool shot;

        public float StartSpellTime;
        public float EndSpellTime;
        public List<string> ColliderNames = new List<string>();
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        private GameObject bulletInstance;

        private List<SpellInfo> FinishedSpells = new List<SpellInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(SpellTypeParameters.Hadoken.ToString(), false);
            handpoint = characterState.GetCharacterControl(animator).handPoint;
            firepoint = characterState.GetCharacterControl(animator).firePoint;
            shot = false;
            bulletInstance = null;
            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.SPELLINFO);
            SpellInfo info = obj.GetComponent<SpellInfo>();
            characterState.GetCharacterControl(animator).Spell = false;
            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator));

            if (!SpellManager.Instance.CurrentSpells.Contains(info))
            {
                SpellManager.Instance.CurrentSpells.Add(info);
            }
        }

        override public void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterSpell(characterState, animator, stateInfo);
            DeregisterSpell(characterState, animator, stateInfo);
            if (bullet != null && !shot && stateInfo.normalizedTime>=firetime)
            {
                StartShoot(characterState, animator, stateInfo);
            }
            //CheckCombo(characterState, animator, stateInfo);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            characterState.GetCharacterControl(animator).Spell = false;
            ClearSpell();
        }

        public void RegisterSpell(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartSpellTime <= stateInfo.normalizedTime && EndSpellTime > stateInfo.normalizedTime)
            {
                foreach (SpellInfo info in SpellManager.Instance.CurrentSpells)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    if (!info.isRegistered && info.spellAbility == this)
                    {
                        info.Register(this);
                    }
                }
            }
        }

        public void DeregisterSpell(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndSpellTime || animator.GetBool(SpellTypeParameters.Hadoken.ToString()))
            {
                if(bulletInstance){
                    if (bulletInstance.transform.parent != null)
                    {
                        Shoot(characterState, animator, stateInfo);
                    }
                }
                foreach (SpellInfo info in SpellManager.Instance.CurrentSpells)
                    {
                        if (info == null)
                        {
                            continue;
                        }
                        if (info.spellAbility == this && !info.isFinishied)
                        {
                            info.isFinishied = true;
                            info.GetComponent<PoolObject>().TurnOff();
                        }
                    }
               
            }
        }

        public void ClearSpell()
        {

            FinishedSpells.Clear();
            foreach (SpellInfo info in SpellManager.Instance.CurrentSpells)
            {
                if (info == null || info.spellAbility == this)
                {
                    FinishedSpells.Add(info);
                }
            }

            foreach (SpellInfo info in FinishedSpells)
            {
                if (SpellManager.Instance.CurrentSpells.Contains(info))
                {
                    SpellManager.Instance.CurrentSpells.Remove(info);
                }
            }

        }

        public void StartShoot(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {
            shot = true;
            characterState.GetCharacterControl(animator).GetComponent<AudioSource>().Play();
            bulletInstance = (GameObject)Instantiate<GameObject>(bullet, handpoint.transform.position ,Quaternion.identity);
            bulletInstance.GetComponent<Bullet_Hadoken>().GetOwner(characterState.GetCharacterControl(animator));
            bulletInstance.GetComponent<Bullet_Hadoken>().RegisterThisBullet();
            bulletInstance.transform.SetParent(handpoint.transform);
            bulletInstance.transform.localPosition = new Vector3(0f,0f,0f);
        }

        public void Shoot(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) {
         bulletInstance.transform.SetParent(null);
            bulletInstance.transform.position = firepoint.transform.position;
            bulletInstance.transform.rotation = Quaternion.Euler(Vector3.zero);
            bulletInstance.gameObject.GetComponent<Rigidbody>().velocity = characterState.GetCharacterControl(animator).gameObject.transform.forward * flySpeed;
            bulletInstance.GetComponent<Bullet_Hadoken>().ActivateCollider();
        }

    }
}