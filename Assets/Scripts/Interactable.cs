using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent call;

    private GameObject player;

    public float interactDistance = 3f;

    private bool isInteractable = false;
    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        isInteractable = dist < interactDistance;
        if (isInteractable && Input.GetKeyUp(KeyCode.E)) {
            /* Debug.Log("HI"); */
            call.Invoke();
        }
    }
}
