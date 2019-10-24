using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool triggered = false;
    public Dialogue dialogue;
    private void Start()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&triggered==false) {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() {
        triggered = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
