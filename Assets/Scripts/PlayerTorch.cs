using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTorch : MonoBehaviour
{
    private GameObject torch;
    private PlayerMovement playerMovement;
    private int currentLife;
    public int maxLife = 100;

    private SpriteRenderer spriteLight;

    private GameObject torchHealthText;
    void Start()
    {
        /* torch = transform.Find("Torch").gameObject; */
        playerMovement = GetComponent<PlayerMovement>();
        currentLife = maxLife;

        Transform lighting = transform.Find("Lighting");
        if (lighting) {
            spriteLight = lighting.gameObject.GetComponent<SpriteRenderer>();
        } else {
            Debug.LogError("Could not find torch light gameobject, unable to change torch intensity");
        }

        torchHealthText = GameObject.Find("TorchHealthText");
        UpdateUI();
        
    }

    public void Relight() {
        currentLife = maxLife;
    }

    public float GetLifeRatio() {
        float ratio = (float)currentLife / (float)maxLife;
        return ratio;
    }

    public bool HasLifeForAction(int amount) {
        return currentLife >= amount;
    }
    public void UseLife(int amount = 10) {
        currentLife -= amount;
        if (currentLife <= 0)
        {
            currentLife = 0;
        }
        UpdateUI();
    }

    public void UpdateUI() {
        if (torchHealthText) {
            torchHealthText.GetComponent<Text>().text = "Torch: " + currentLife + "/" + maxLife;
        }
    }

    public void Update() {
        if (spriteLight) {
            Color color = spriteLight.color;
            color.a = GetLifeRatio();
            spriteLight.color = color;
        }
    }
}
