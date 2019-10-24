using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneUI : MonoBehaviour
{
    public Animator blackscreenAnimator;
     public void FadeInScene()
     {
         blackscreenAnimator.SetTrigger("FadeOut");
     }
    public void QuitGame() {
        Application.Quit();
    } 
}
