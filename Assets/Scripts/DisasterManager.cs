using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class DisasterManager : MonoBehaviour
{
    private float disasterProgress;
    private int disasterType;
    public float disasterDuration = 120.0f;

    public UnityEngine.UI.Text disasterResultText;
    public UnityEngine.UI.Text disasterHeaderText;
    public UnityEngine.UI.Text scoreText;
    public GameObject disasterResultPanel;

    private float totalPoints = 0.0f;

    public string GetCurrentDisasterType() {
        return DisasterName(disasterType);
    }

    //returns a float between 0 and 1 indicating intensity of the current disaster
    public float GetCurrentDisasterIntensity() {
        return disasterProgress / disasterDuration;
    }

    public void Update() 
    {
        disasterProgress += Time.deltaTime;
        if (disasterProgress >= disasterDuration) {
            TriggerNewDisaster();
        }
    }

    public string DisasterName(int id) 
    {
        if (id == 0) 
            return "Heavy Rain and Flooding";
        else if (id == 1)
            return "Blizzard";
        else
            return "Famine";
    }

    public void TriggerNewDisaster() 
    {
        NPCManager npcManager = NPCManager.instance;

        List<string> story = npcManager.MakeDisasterStory(disasterType);
        List<AudioClip> audioStory = npcManager.MakeAudioStory(this.disasterType);
        float points = npcManager.getCurrentInventoryPoints(disasterType);
        totalPoints += points;

        disasterResultPanel.SetActive(true);
        disasterHeaderText.text = "A " +  DisasterName(disasterType) + " Occurred!";
        string finalStory = "";
        foreach (var s in story)
        {
            finalStory += s + " ";
        }

        if ((audioStory.Count > 0) && SoundManager.instance!=null)
        {
            SoundManager.instance.PlayAudioList(audioStory);
        }



        if (points <= 0) 
        {
            disasterResultText.text = "None of your villagers helped mitigate the disaster. Try talking to them with their desired items.";
        }
        else 
        {
            if (story.Count < 1  && audioStory.Count < 1) 
            {
                this.disasterResultText.text = "Thanks for your help, but unfortunately your deliveries couldn't help.";
            }
            else 
            {
                disasterResultText.text = finalStory;
            }
        }

        if (totalPoints != null) 
        {
            scoreText.text = totalPoints.ToString();
        }
        npcManager.clearCurrentInventoryPoints();



        disasterProgress = 0.0f;
        disasterType = ++disasterType % 3;
    }


}
