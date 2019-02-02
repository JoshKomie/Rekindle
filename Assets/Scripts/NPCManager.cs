using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
  public static NPCManager instance = null;
  public List<Npc> theNPCs = new List<Npc>();

  public float talkDistance = 3;

  public Npc activeNPC = null;

  public PlayerMovement Player = null;
  

  public List<string> MakeDisasterStory(int disasterIndex)
  {
    List<string> theStory = new List<string>();
    foreach (var npc in theNPCs)
    {
      string part = npc.GetDisasterStory(disasterIndex).Trim();
      if (part.Length > 0)
      {
        theStory.Add(part);
      }
    }

    return theStory;
  }

  public List<AudioClip> MakeAudioStory(int disasterIndex)
  {
    List<AudioClip> theStory = new List<AudioClip>();
    foreach (var npc in theNPCs)
    {
      AudioClip part = npc.GetDisasterAudioClip(disasterIndex);
      if (part)
      {
        theStory.Add(part);
      }
    }

    return theStory;
  }

  void Awake()
  {
    if (instance != null)
      GameObject.Destroy(gameObject);
    else
    {
      instance = this;
    }
  }

  public void clearCurrentInventoryPoints()
  {
    foreach (var npc in theNPCs)
    {
      npc.satisfaction = 0;
    }
  }

  public string DisasterName(int id) {
      if (id == 0) 
          return "Heavy Rain and Flooding";
      else if (id == 1)
          return "Blizzard";
      else
          return "Famine";
  }
  public float getCurrentInventoryPoints(int disasterIndex=0)
  {
    float totalPoints = 0;
    foreach (var npc in theNPCs)
    {
      float value = npc.getContributionValue(disasterIndex);
      UnityEngine.Debug.Log(npc.name + " = " + value);
      totalPoints += value;
    }

    UnityEngine.Debug.Log("Total points : " + totalPoints);

    return totalPoints;
  }

    // Start is called before the first frame update
    void Start()
    {
      this.Player = GameObject.FindObjectOfType<PlayerMovement>();
      var objects = FindObjectsOfType<Npc>();
      foreach (var obj in objects)
      {
        this.theNPCs.Add(obj);
      }
    }

    
}
