using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceCrossFader : MonoBehaviour
{
    public GameObject happy;
    public GameObject sad;

    private AudioSource happyAudio, sadAudio;

    public int happyValue = 0;
    public int maxHappyValue = 100;
    public float ratio = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        happyAudio = happy.GetComponent<AudioSource>();
        sadAudio = sad.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        happyAudio.volume = ratio;
        sadAudio.volume = 1 - ratio;
        
    }
}
