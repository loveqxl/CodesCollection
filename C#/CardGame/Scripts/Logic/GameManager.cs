using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        MenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
           MenuCanvas.SetActive(!MenuCanvas.activeInHierarchy);
            if (MenuCanvas.activeInHierarchy == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }
    }

    public void ResumeGame()
    {

        MenuCanvas.SetActive(!MenuCanvas.activeInHierarchy);
            if (MenuCanvas.activeInHierarchy == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }
}
