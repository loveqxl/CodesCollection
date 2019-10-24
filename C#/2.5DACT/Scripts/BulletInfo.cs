using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
    public class BulletInfo : MonoBehaviour
    {
        public CharacterControl Attacker = null;
        public GameObject bulletPrefab;
        public Bullet_Hadoken Bullet;

        public List<string> ColliderNames = new List<string>();
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegistered;
        public bool isFinishied;
        public float MaxExistTime;
        public int damageAmount;
        public AudioClip hitSound;
        public void ResetInfo(CharacterControl attacker)
        {
            isRegistered = false;
            isFinishied = false;
            
            Attacker = attacker;
        }

        public void Register(Bullet_Hadoken bullet)
        {
            isRegistered = true;


            Bullet = bullet;
            ColliderNames = bullet.ColliderNames;
            LaunchIntoAir = bullet.LaunchIntoAir;
            MustCollide = bullet.MustCollide;
            MustFaceAttacker = bullet.MustFaceAttacker;
            LethalRange = bullet.LethalRange;
            MaxHits = bullet.MaxHits;
            MaxExistTime = bullet.MaxHits;
            CurrentHits = 0;
            damageAmount = bullet.damageAmount;
            hitSound = bullet.hitSound;
        }

        public void Explo() {
            StartCoroutine(Explosion());
        }

        public IEnumerator Explosion()
        {
            GameObject effect = (GameObject)Instantiate<GameObject>(Bullet.explosionParticle);
            effect.transform.position = Bullet.transform.position;
            yield return new WaitForSeconds(1f);
            Destroy(effect.gameObject);
            Destroy(this.gameObject);
        }
        private void OnDisable()
        {
            isFinishied = true;
        }
    }

}