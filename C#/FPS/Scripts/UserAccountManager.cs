using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public static string LoggedIn_Username { get; protected set; }
    private static string LoggedIn_Password = "";

    public static string LoggedIn_Data { get; protected set; }

    public static bool IsLoggedIn { get; protected set; }

    public string loggedInSceneName = "Lobby";
    public string loggedOutSceneName = "LoginMenu";

    public delegate void OnDataReceivedCallback(string data);

    public void LogOut() {

        Debug.Log("User: "+LoggedIn_Username + " has Logged Out!");
        LoggedIn_Username = "";
        LoggedIn_Password = "";

        IsLoggedIn = false;
        SceneManager.LoadScene(loggedOutSceneName);
    }

    public void LogIn(string username, string password)
    {
        LoggedIn_Username = username;
        LoggedIn_Password = password;

        IsLoggedIn = true;
        Debug.Log("User: "+LoggedIn_Username + " has Logged In!");

        SceneManager.LoadScene(loggedInSceneName);
    }

    IEnumerator GetData(string playerUsername,string playerPassword,OnDataReceivedCallback onDataReceived)
    {
        IEnumerator e = DCF.GetUserData(playerUsername, playerPassword); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
        {
            //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.
            Debug.LogError("Error: Unknown Error. Please try again later.");
            yield return new WaitForSeconds(5f);
        }
        else
        {
            //The player's data was retrieved. Goes back to loggedIn UI and displays the retrieved data in the InputField
            
            LoggedIn_Data = response;
            Debug.Log(playerUsername+"'s Data got!: "+LoggedIn_Data);
            if (onDataReceived != null)
                onDataReceived.Invoke(LoggedIn_Data);
            StopCoroutine("GetData");
        }
    }

    IEnumerator SetData(string playerUsername,string playerPassword,string data)
    {
        IEnumerator e = DCF.SetUserData(playerUsername, playerPassword, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //The data string was set correctly. Goes back to LoggedIn UI
            Debug.Log("Player: "+playerUsername+"'s data: "+data+" has been sent!");
            StopCoroutine("SetData");
        }
        else
        {
            Debug.LogError("Player: "+playerUsername+"'s data send failed!");
        }
    }

    public void ReceiveData(OnDataReceivedCallback onDataReceived)
    {
        if(IsLoggedIn)
        //Called when the player hits 'Get Data' to retrieve the data string on their account. Switches UI to 'Loading...' and starts coroutine to get the players data string from the server
        StartCoroutine(GetData(LoggedIn_Username,LoggedIn_Password,onDataReceived));
    }

    public void SendData(string data) {
        if (IsLoggedIn) {
            StartCoroutine(SetData(LoggedIn_Username,LoggedIn_Password,data));
        }
    }
}
