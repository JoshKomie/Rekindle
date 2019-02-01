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

  private int currentDisaster = 0;
  private float timeUntilNextDisaster = 0;
  
  public UnityEngine.UI.Text disasterResultText;
  public UnityEngine.UI.Text disasterHeaderText;
  public UnityEngine.UI.Text scoreText;
  public GameObject disasterResultPanel;

  public float totalPoints = 0.0f;

  public float DISASTER_DURATION = 80.0f;

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
    this.timeUntilNextDisaster = this.DISASTER_DURATION;
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

    // Update is called once per frame
    void Update()
    {
        timeUntilNextDisaster -= Time.deltaTime;
        if (timeUntilNextDisaster < 1) {
            List<string> story = MakeDisasterStory(currentDisaster);
            List<AudioClip> audioStory = MakeAudioStory(this.currentDisaster);
            UnityEngine.Debug.Log(story);
            float points = getCurrentInventoryPoints(currentDisaster);
            totalPoints += points;
            disasterResultPanel.SetActive(true);
            disasterHeaderText.text = "A " +  DisasterName(currentDisaster) + " Occurred!";
            string finalStory = "";
            foreach (var s in story)
            {
              finalStory += s + " ";
            }


            UnityEngine.Debug.Log(audioStory.Count +" : "+ audioStory);
            if ((audioStory.Count > 0) && SoundManager.instance!=null)
                {
                  SoundManager.instance.PlayAudioList(audioStory);
                }



            if (points <= 0)
            {
              this.disasterResultText.text = "Thanks for your help, but unfortunately your deliveries couldn't help.";
            }
            else
            {
              if (story.Count < 1  && audioStory.Count < 1)
              {
                disasterResultText.text =
                  "None of your villagers helped mitigate the disaster. Try talking to them with their desired items.";
              }
              else
              {
                disasterResultText.text = finalStory;
              }
            }

            /* if (totalPoints != null) { */
            /*     scoreText.text = totalPoints.ToString(); */
            /* } */
            clearCurrentInventoryPoints();

            currentDisaster++;
            timeUntilNextDisaster = DISASTER_DURATION;
        }
      /*
      float minDist = 1000000;
      this.activeNPC = null;
      if (Player)
      {
        foreach (var npc in this.theNPCs)
        {
          npc.isWithPlayer = false;
          Debug.Log(npc.gameObject.name + " : " + npc.distanceFromPlayer);
          npc.distanceFromPlayer = Vector3.Distance(Player.transform.position, npc.transform.position);
          if (minDist < npc.distanceFromPlayer)
          {
            activeNPC = npc;
            minDist = npc.distanceFromPlayer;
          }
        }

        if (this.activeNPC)
        {
          this.activeNPC.isWithPlayer = true;
        }
      }*/
    }
    
}
