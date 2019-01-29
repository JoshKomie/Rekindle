using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

  public AudioSource theAudioSource = null;

  public AudioClip currentClip;
  public int nextTrack;

  public float endTimer = 0;

  public static SoundManager instance = null;

  private List<AudioClip> theClipList = new List<AudioClip>(); 

    // Start is called before the first frame update
    void Awake()
    {
      if (theAudioSource == null)
      {
        theAudioSource = gameObject.GetComponent<AudioSource>();
      }
      if (instance != null)
      {
        GameObject.Destroy(gameObject);
      }

      instance = this;
    }

    public void PlayAudioList(List<AudioClip> theClips)
    {
      foreach (var item in theClips)
      {
        theClipList.Add(item);
      }

    }

    // Update is called once per frame
    void Update()
    {
      endTimer -= Time.deltaTime;

      if (endTimer <= 0)
      {
        startNextClip();
        endTimer = 0;
      }
    }

    public void startNextClip()
    {
      if ((theAudioSource != null) && (nextTrack < theClipList.Count))
      {
        theAudioSource.clip = theClipList[0];
        theClipList.RemoveAt(0);
        endTimer = theAudioSource.clip.length;
      }
    }
}
