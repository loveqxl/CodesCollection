using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HorrorGame
{
    public class Credit : MonoBehaviour
    {
        private RectTransform rectTransform;
        public int speed = 200;
        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.position = new Vector3(rectTransform.position.x, -0.5f * rectTransform.sizeDelta.y, rectTransform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            if (rectTransform.position.y < rectTransform.sizeDelta.y + Screen.height)
            {
                rectTransform.position += Vector3.up * Time.deltaTime*speed;
            }
            else {
                SceneManager.LoadScene("MainMenu");
            }
                
        }
    }
}
