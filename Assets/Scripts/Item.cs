using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public string name;
    private bool playerInRange = false;
    private GameObject player;
    public void Start() {
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("ENTER");
        playerInRange = true;
    }

    public void OnTriggerExit2D(Collider2D other) {
        Debug.Log("EXIT");
        playerInRange = false;
    }
    void Update() {
        if (playerInRange && Input.GetKeyUp(KeyCode.E)) {
            player.GetComponent<Inventory>().AddItem(gameObject);
            gameObject.SetActive(false);
        }
    }
}
