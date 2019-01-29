using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    private GameObject torch;
    private PlayerMovement playerMovement;

    void Start()
    {
        torch = transform.Find("Torch").gameObject;
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /* float dist = playerMovement.GetDistCovered(); */
        /* float scale = 1 - dist / 20.0f; */
        /* torch.transform.localScale = new Vector2(scale, scale); */
        
    }
}
