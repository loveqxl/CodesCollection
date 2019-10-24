using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HorrorGame
{
    public class MessageManager : Singleton<MessageManager>
    {
        private GameObject messageBox;
        private Text messageText;
        private float showTime=1f;
        
        void Start() { 
  
            messageBox = GameObject.Find("Canvas").transform.Find("MessageUI").gameObject;
            messageText = messageBox.GetComponentInChildren<Text>();
            messageText.text = "";
        }

        public void ShowMessage(string _message,float _showTime=1f) {
            StopCoroutine("MessageCR");
            messageText.text = _message;
            showTime = _showTime;
            StartCoroutine("MessageCR");
        }

        IEnumerator MessageCR() {
            messageBox.SetActive(true);
            yield return new WaitForSeconds(showTime);
            messageBox.SetActive(false);
            messageText.text = "";
        }
    }
}
