using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6 {
    public enum SpellTypeParameters {
        Hadoken,
        Dash,
    }

    public class SpellCheck : MonoBehaviour
    {

        public List<Spell> spellInfos;
        public int[] inputStages;
        private CharacterControl control;

        private void Start()
        {
            control = GetComponent<CharacterControl>();
            inputStages = new int[spellInfos.Count];
            for (int i = 0; i < spellInfos.Count; i++)
            {
                inputStages[i] = 0;
            }

        }

        void Update()
        {        

        }


        public void GetInput(TransitionParameter transitionParameter) {
            foreach (Spell info in spellInfos)
            {
                int index = spellInfos.IndexOf(info);            
                float[] timer = new float[info.inputsList.Count-1];
                for (int i = 0; i < timer.Length - 1; i++)
                {
                    timer[i] = info.dirWaitTime;
                }
                timer[timer.Length-1] = info.atkWaitTime;

                if (info.inputsList[inputStages[index]] == transitionParameter) {

                    //Debug.Log(info.inputsList[inputStage]);
                    if(inputStages[index] < info.inputsList.Count-1) { 
                        if (inputStages[index] > 0)
                        {
                            StopCoroutine(Countdown(timer[inputStages[index] - 1]));
                        }
                        StartCoroutine(Countdown(timer[inputStages[index]]));
                        inputStages[index]++;
                    }
                    else if(inputStages[index] == info.inputsList.Count - 1)
                    {
                            StopCoroutine(Countdown(timer[inputStages[index] - 1]));
                        //Debug.Log("Hadoken");
                        if (!control.Spell && !control.Dash && !control.Attack_Normal && info.inputsList[inputStages[index]] == TransitionParameter.Attack)
                        {
                            control.Spell = true;
                            StartCoroutine(ResetSpell());
                            inputStages[index] = 0;
                        }
                        else if (!control.Spell && !control.Dash && !control.Attack_Normal && (info.inputsList[inputStages[index]] == TransitionParameter.MoveLeft|| info.inputsList[inputStages[index]] == TransitionParameter.MoveRight))
                        {
                            control.Dash = true;
                            StartCoroutine(ResetDash());
                            inputStages[index] = 0;
                        }
                    }
                }

                if (transitionParameter == TransitionParameter.Attack && !control.Spell && !control.Attack_Normal && inputStages[index] != info.inputsList.Count - 1)
                {
                    control.Attack_Normal = true;

                    StartCoroutine(ResetNormalAttack());
                }

                IEnumerator Countdown(float _timer)
                {
                    while (_timer > 0)
                    {
                        _timer -= 1;
                        yield return new WaitForFixedUpdate();
                    }
                    inputStages[index] = 0;
                }
            }
        }



        IEnumerator ResetSpell() {
            yield return new WaitForSeconds(0.5f);
            control.Spell = false;
        }

        IEnumerator ResetDash()
        {
            yield return new WaitForSeconds(0.3f);
            control.Dash = false;
        }

        IEnumerator ResetNormalAttack()
        {
            yield return new WaitForSeconds(0.3f);
            control.Attack_Normal = false;
        }
    }
}
