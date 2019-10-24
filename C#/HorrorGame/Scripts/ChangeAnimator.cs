using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimator : StateMachineBehaviour
{



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.runtimeAnimatorController = null;
    }

}
