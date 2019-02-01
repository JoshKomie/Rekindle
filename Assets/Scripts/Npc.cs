using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.UI;

[System.Serializable]
public class Npc : MonoBehaviour
{ 
    public float satisfaction = 0;
    public float[] contributionValue = new float[3];

    public string[] successStories = new string[3];
    public string[] failureStories = new string[3];
    public AudioClip[] successClips = new AudioClip[3];
    public AudioClip[] failureClips = new AudioClip[3];

    /* private GameObject player; */
    /* public float interactDistance = 0.4f; */
    private bool playerInRange = false;
    private GameObject campfire;

    private int level = 0;
    private int currentConversation = 0;
    public string requiredItem;

    public string name;

    public Item[] relevantItems;
    public string nothingToSay;
    public string requiredItemAbsent;
    public Conversation[] conversations;

    public GameObject dialoguePanel;
    public GameObject npcInteract;
    public GameObject initiateText;

    public Vector3 interactionOffset;

    public float distanceFromPlayer = 0;
    public bool isWithPlayer = false;

    private Inventory inventory;

    public void ReceivedTrade()
    {
        satisfaction += 1f;

    }

    public float getContributionValue(int disasterIndex)
    {
        if (disasterIndex < 0 || disasterIndex > this.contributionValue.Length)
        {
            return 0;
        }
        else
        {
            return (this.contributionValue[disasterIndex] * this.satisfaction);
        }
    }

    private string SuccessStory(int disasterIndex)
    {
        if (disasterIndex < 0 || disasterIndex > this.successStories.Length)
        {
            return string.Empty;
        }
        else
        {
            if (this.getContributionValue(disasterIndex) < 1)
            {
                return string.Empty;
            }
            else
            {
                return this.successStories[disasterIndex];
            }
        }
    }


    private string FailureStory(int disasterIndex)
    {
        if (disasterIndex < 0 || disasterIndex > this.successStories.Length)
        {
            return string.Empty;
        }
        else
        {
            if (this.getContributionValue(disasterIndex) < 1)
            {
                return string.Empty;
            }
            else
            {
                return this.failureStories[disasterIndex];
            }
        }
    }

    private AudioClip SuccessClip(int disasterIndex)
    {
        if (disasterIndex < 0 || disasterIndex > this.successStories.Length)
        {
            return null;
        }
        else
        {
            if (this.getContributionValue(disasterIndex) < 1)
            {
                return null;
            }
            else
            {
                return this.successClips[disasterIndex];
            }
        }
    }


    private AudioClip FailureClip(int disasterIndex)
    {
        if (disasterIndex < 0 || disasterIndex > this.successStories.Length)
        {
            return null;
        }
        else
        {
            if (this.getContributionValue(disasterIndex) < 1)
            {
                return null;
            }
            else
            {
                return this.failureClips[disasterIndex];
            }
        }
    }


    public string GetDisasterStory(int disasterIndex)
    {
        if (this.satisfaction > 0)
        {
            return this.SuccessStory(disasterIndex);
        }
        else
        {
            return this.FailureStory(disasterIndex);
        }
    }

    public AudioClip GetDisasterAudioClip(int disasterIndex)
    {
        if (this.satisfaction > 0)
        {
            return this.SuccessClip(disasterIndex);
        }
        else
        {
            return this.FailureClip(disasterIndex);
        }
    }

    private GameObject player;

    void Start()
    {
        var playerMove = GameObject.FindObjectOfType<PlayerMovement>();
        player = playerMove.gameObject;
        inventory = this.player.GetComponent<Inventory>();
        /* campfire = transform.Find("Campfire").gameObject; */
        /* campfire.SetActive(false); */
    }

    void Update() {
        if (playerInRange && Input.GetKeyUp(KeyCode.E)) {
            if (level == 0)
                Unlock();
            npcInteract.SetActive(true);
            initiateText.SetActive(false);
            /* npcInteract.transform.position = transform.position + interactionOffset; */
        }
    }
    public void ResetAllConv() {
        foreach (var conv in conversations) {
            conv.Reset();
        }
    }
    public void Talk() {
        ResetAllConv();

        npcInteract.SetActive(false);
        string t = GetDialogue();
        UnityEngine.Debug.Log("t="+ t);
        /* bool isLastConversation = conversations[currentConversation].IsLastConversation(); */
        /* if (isLastConversation) { */
        /*     if (currentConversation < conversations.Length - 1) */
        /*         currentConversation++; */
        /*     else */
        /*         conversations[currentConversation].Reset(); */
        /* } */
        if (t != null) {
            /* Debug.Log(t); */
            dialoguePanel.SetActive(true);
            dialoguePanel.transform.Find("Text").GetComponent<Text>().text = t;
            dialoguePanel.GetComponent<DialoguePanel>().SetNPC(this);
        }

    }

    public void Leave() {
        npcInteract.SetActive(false);
    }

    public void Unlock() {
        /* campfire.SetActive(true); */
        level = 1;

    }

    public string MissingItemMessage
    {
        get
        {
            if (this.requiredItemAbsent.Contains("<X>"))
            {
                var parts = this.requiredItemAbsent.Split(new string[] { "<", ">" }, StringSplitOptions.RemoveEmptyEntries);
                string result = "";
                foreach (var s in parts)
                {
                    if (s == "X")
                    {
                        result += this.conversations[0].requiredItem;
                    }

                    else
                    {

                        result += s;
                    }
                }

                return result;
            }
            else
            {
                return this.requiredItemAbsent;
            }

        }
    }


    public string ContinueDialogue() {
        Conversation conv = conversations[currentConversation];
        string dialogue = conv.GetConversation();
        return dialogue;

    }
    public string GetDialogue() {
        if (requiredItem != "NONE" && !this.inventory.HasItem(requiredItem)) {
            currentConversation = 0;

        } else {
            currentConversation = 1;
        }
        Conversation conv = conversations[currentConversation];
        string dialogue = conv.GetConversation();
        if (currentConversation == 1) {
            this.ReceivedTrade();
            UnityEngine.Debug.Log("Score!");

            this.inventory.RemoveItem(conv.requiredItem);
            if (conv.giveItem != null)
            {
                this.inventory.AddItem(conv.giveItem);
            }

        }

        return dialogue;
        /* if (conv.requiredItem != "NONE" && !this.inventory.HasItem(conv.requiredItem)) { */
        /*     return MissingItemMessage; */
        /* } */
        /* else */
        /* { */
        /*   string dialogue = conv.GetConversation(); */
        /*   if (conv.requiredItem != "NONE") { */
        /*       this.ReceivedTrade(); */

        /*       this.inventory.RemoveItem(conv.requiredItem); */
        /*       if (conv.giveItem == null) */
        /*       { */
        /*           this.inventory.AddItem(conv.giveItem); */
        /*       } */

        /*   } */

        /*   return dialogue; */
        /* } */
    }

    public bool HasNothingToSay() {
        Conversation conv = conversations[currentConversation];
        /* return conv.requiredItem != null && (conv.requiredItem != "NONE" && !inventory.HasItem(conv.requiredItem)); */
        return false;
    }


    public void OnTriggerEnter2D(Collider2D other) {
        UnityEngine.Debug.Log("ENTER");
        initiateText.SetActive(true);
        playerInRange = true;
    }

    public void OnTriggerExit2D(Collider2D other) {
        initiateText.SetActive(false);
        UnityEngine.Debug.Log("EXIT");
        playerInRange = false;
    }
}
