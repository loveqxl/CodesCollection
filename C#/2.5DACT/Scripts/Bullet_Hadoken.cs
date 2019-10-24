using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    public class Bullet_Hadoken : MonoBehaviour
    {
        public CharacterControl owner;
        public List<string> ColliderNames = new List<string>();
        public GameObject explosionParticle;

        public Collider bulletCollider;
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public float MaxExistTime;
        public int damageAmount;
        public AudioClip hitSound;

        private GameObject obj;
        private BulletInfo info;
        // Start is called before the first frame update
        void Awake()
        {
            obj = PoolManager.Instance.GetObject(PoolObjectType.BULLETINFO);
            info = obj.GetComponent<BulletInfo>();
            obj.SetActive(true);       
            bulletCollider = GetComponent<Collider>();
        }

        private void Start()
        {
            StartCoroutine(SelfDestroy());
        }
        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator SelfDestroy() {
            while (MaxExistTime > 0)
            {
                MaxExistTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
                Destroy(this.gameObject);

        }

        public void GetOwner(CharacterControl _owner) {
            owner=_owner;
        }

        public void ActivateCollider() {
            bulletCollider.enabled=true;
        }

        public void RegisterThisBullet() {
            info.ResetInfo(owner);
            info.Register(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterControl control = other.GetComponentInParent<CharacterControl>();
            if (control == owner) {
                return;
            }

            if (control != null && control != owner)
            {
                DamageDetector detector = control.GetComponent<DamageDetector>();
                if (detector.alive)
                {
                    info.Explo();
                    detector.TakeDamage(info);
                    Destroy(this.gameObject);
                }
            }
            else if (control == null) {
                info.Explo();
                Destroy(this.gameObject);
            }
            }
        

        }

    }
