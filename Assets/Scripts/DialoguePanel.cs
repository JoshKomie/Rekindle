using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    private Npc npc;
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNPC(Npc npc) {
        this.npc = npc;

    }

    public void Next() {
        string s = npc.ContinueDialogue();
        transform.Find("Text").GetComponent<Text>().text = s;
        if (s == null || npc.HasNothingToSay()) {
            gameObject.SetActive(false);
        }
    }
}
