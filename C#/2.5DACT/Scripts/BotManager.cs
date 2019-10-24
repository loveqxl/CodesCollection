using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week6 { 
public class BotManager : MonoBehaviour
    {
        public int currentEnemyAmount;
        public Animator blackscreenAnimator;
        // Start is called before the first frame update
        void Start()
    {
            currentEnemyAmount=0;
            DamageDetector[] damageDetectors = FindObjectsOfType<DamageDetector>();
            foreach (DamageDetector d in damageDetectors) {
                if (d.tag == "Enemy") {
                    currentEnemyAmount += 1;
                }
            } 
    }

    // Update is called once per frame
    void Update()
    {
            if (currentEnemyAmount == 0) {
                FadeInScene();

            }
    }
       private void  FadeInScene()
        {
            blackscreenAnimator.GetComponent<BlackScreenMain>().sceneName = "WinScene";
            blackscreenAnimator.SetTrigger("FadeOut");
        }
    }
}