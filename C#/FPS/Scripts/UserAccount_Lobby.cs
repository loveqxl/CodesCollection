using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAccount_Lobby : MonoBehaviour
{
    public Text userAccountInfoText;
    // Start is called before the first frame update
    void Start()
    {
        if (UserAccountManager.IsLoggedIn) {
            userAccountInfoText.text = "Logged In As: " + UserAccountManager.LoggedIn_Username;
        }
    }

    public void LogOut() {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.LogOut();
        }

    }
}
