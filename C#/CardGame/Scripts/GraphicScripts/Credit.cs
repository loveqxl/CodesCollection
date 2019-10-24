using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Credit : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        Sequence s = DOTween.Sequence();

        s.Append(img.rectTransform.DOMoveY(2000,15f));
        s.OnComplete(()=>SceneManager.LoadScene(0));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }
}
