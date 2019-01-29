using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conversation {
    public int requiredLevel;
    public string requiredItem;
    public GameObject giveItem;
    public string[] conversationStrings;

    private int currentConversationString = 0;

    public string  First() {
        return conversationStrings[0];
    }
    public string  Second() {
        return conversationStrings[1];
    }

    public string GetConversation() {
        if (currentConversationString > conversationStrings.Length - 1) {
            return null;
        }
        return conversationStrings[currentConversationString++];
    }

    public bool IsLastConversation() {
        return currentConversationString == conversationStrings.Length;
    }

    public void Reset() {
        currentConversationString = 0;
    }

}
