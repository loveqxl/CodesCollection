using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public UIScript UI;
    public List<Pickup> pickups=new List<Pickup>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        Setup();
    }


    // Update is called once per frame
    void Setup()
    {
        
        Pickup[] allPickups = FindObjectsOfType<Pickup>();
        foreach (Pickup pku in allPickups) {
            pickups.Add(pku);
        }

        Time.timeScale = 1;
    }

    public void Win() {
        UI.winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose() {
        UI.gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
