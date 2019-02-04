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

    public int interactCost = 0;

    private PlayerTorch playerTorch;
    void Start()
    {
        player = GameObject.Find("Player");
        playerTorch = player.GetComponent<PlayerTorch>();
    }


    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        isInteractable = dist < interactDistance;

        if (playerTorch != null && playerTorch.HasLifeForAction(interactCost) && isInteractable && Input.GetKeyUp(KeyCode.E)) {
            call.Invoke();
            playerTorch.UseLife(interactCost);
        }
    }
}
