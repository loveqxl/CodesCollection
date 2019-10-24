using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Es.InkPainter;

public class UIScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;
    // Start is called before the first frame update


    public void Restart() {
        

        //InkCanvas[] inkCanv = FindObjectsOfType<InkCanvas>();
        //foreach (InkCanvas cav in inkCanv) {
        //    cav.ResetPaint();
        //}

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
