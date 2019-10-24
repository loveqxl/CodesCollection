using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week6 {

    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;
        GeneralBodyPart DamagePart;
        BoxCollider boxCollider;
        public bool alive=true;
        public UIController uIController;
        public BotManager botManager;
        private AudioSource audioSource;
        public GameObject hitEffect;
        private string hitpartName;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
            boxCollider = control.GetComponent<BoxCollider>();
            audioSource = GetComponent<AudioSource>();
            alive = true;
        }

        private void Update()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0&&alive) {
                CheckAttack();
            }
        }

        private void CheckAttack() {
            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks) {
                if (info == null) {
                    continue;
                }

                if (!info.isRegistered) {
                    continue;
                }

                if (info.isFinishied) {
                    continue;
                }

                if (info.Attacker == control) {
                    continue;
                }

                if (info.MustCollide) {
                    if (IsCollided(info)&&info.MaxHits>0) {
                        info.MaxHits--;
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info) {

            foreach (TriggerDetector trigger in control.GetAllTriggers()) {
                foreach (Collider collider in trigger.CollidingParts)
                {
                    foreach (string name in info.ColliderNames)
                    {
                        if (name.Equals(collider.gameObject.name))
                        {
                            if (collider.transform.root.gameObject == info.Attacker.gameObject)
                            {
                                hitpartName = collider.name;
                                DamagePart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void TakeDamage(AttackInfo info) {

            CameraManager.Instance.ShakeCamera(0.25f);

            //Debug.Log(info.Attacker.gameObject.name+" hits "+this.gameObject.name);
            //Debug.Log(this.gameObject.name + " hit " + DamagePart.ToString());
            //control.SkinnedMeshAnimator.runtimeAnimatorController = info.attackAbility.GetDeathAnimator();
            CharacterControl control = GetComponent<CharacterControl>();
            control.currentHP -= info.damageAmount;
            info.CurrentHits++;
            if (info.hitSound)
            {
                audioSource.clip = info.hitSound;
                audioSource.Play();
            }
            GameObject effect = (GameObject)Instantiate<GameObject>(hitEffect);
            effect.transform.parent = info.Attacker.GetChildObj(hitpartName).transform;
            effect.transform.localPosition = Vector3.zero;
            effect.transform.localRotation = Quaternion.identity;
            StartCoroutine(DestroyEffect());

            if (control.currentHP <= 0)
            {
                alive = false;
                boxCollider.enabled = false;
                control.RIGID_BODY.useGravity = false;
                if (control.edgeChecker)
                {
                    control.edgeChecker.GetComponent<BoxCollider>().enabled = false;
                }

                control.SkinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetAnimator(DamagePart, info);
                uIController.StartCoroutine(uIController.HideEnemyPanel());
                if (tag == "Enemy")
                {
                    botManager.currentEnemyAmount -= 1;
                }
                }
            else {
                control.SkinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetHittedAnimator();
            }
            if (tag == "Enemy") {
                uIController.UpdateEnemyUI(control);
            }
             IEnumerator DestroyEffect() {
                yield return new WaitForSeconds(2f);
                Destroy(effect);
            }

        }

        public void TakeDamage(BulletInfo info)
        {

            CameraManager.Instance.ShakeCamera(0.25f);

            //Debug.Log(info.Attacker.gameObject.name + " hits " + this.gameObject.name);
            //Debug.Log(this.gameObject.name + " hit " + DamagePart.ToString());
            //control.SkinnedMeshAnimator.runtimeAnimatorController = info.attackAbility.GetDeathAnimator();

            info.CurrentHits++;
            CharacterControl control = GetComponent<CharacterControl>();
            control.currentHP -= info.damageAmount;
            if (info.hitSound)
            {
                audioSource.clip = info.hitSound;
                audioSource.Play();
            }
            if (control.currentHP <= 0)
            {
                control.SkinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetAnimator(DamagePart, info);
                alive = false;
                boxCollider.enabled = false;
                control.RIGID_BODY.useGravity = false;
                if (control.edgeChecker)
                {
                    control.edgeChecker.GetComponent<BoxCollider>().enabled = false;
                }
                uIController.StartCoroutine(uIController.HideEnemyPanel());
                if (tag == "Enemy")
                {
                    botManager.currentEnemyAmount -= 1;
                }
            }
            else {
                control.SkinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetHittedAnimator();
            }
            if (tag == "Enemy")
            {
                uIController.UpdateEnemyUI(control);
            }
        }
    }
}

