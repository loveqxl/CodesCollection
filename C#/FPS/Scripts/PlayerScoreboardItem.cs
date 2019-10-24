using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardItem : MonoBehaviour
{
    [SerializeField]
    Text usernameText;
    [SerializeField]
    Text killsText;
    [SerializeField]
    Text deathsText;

    public void Setup(string username, int kills, int deaths) {
        usernameText.text = username;
        if (username == UserAccountManager.LoggedIn_Username) {
            usernameText.color = new Color32(255,0,0,100);
        }
        killsText.text = "Kills: "+kills;
        deathsText.text = "Deaths: "+deaths;

    }
}
