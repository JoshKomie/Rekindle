using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private float distCovered = 0f;
    public GameObject dialogueWindow;
    public GameObject npcInteract;

    private Animator animator;
    private bool isMoving = false, previousIsMoving = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
       StartDay(); 
       animator = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void StartDay() {
        distCovered = 0f;

    }

    public float GetDistCovered() {
        return distCovered;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueWindow.activeSelf) return;
        /* if (!npcInteract.activeSelf) return; */
        float finalSpeed = speed;
        bool up = Input.GetKey("up") || Input.GetKey(KeyCode.W);
        bool left = Input.GetKey("left") || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey("right") || Input.GetKey(KeyCode.D);
        bool down = Input.GetKey("down") || Input.GetKey(KeyCode.S);
        if ((up && left) || (up && right) || (down && left) || (down && right)) {
            finalSpeed *= 0.707106781f;
        }

        if (up) {
            this.transform.Translate(Vector2.up * finalSpeed * Time.deltaTime);
        } else if (down) {
            this.transform.Translate(Vector2.down * finalSpeed * Time.deltaTime);
        }

        if (left) {
            this.transform.Translate(Vector2.left * finalSpeed * Time.deltaTime);
            spriteRenderer.flipX = true;
        } else if (right){
            this.transform.Translate(Vector2.right * finalSpeed * Time.deltaTime);
            spriteRenderer.flipX = false;
        }

        previousIsMoving = isMoving;
        if (up || down || left || right) {
            distCovered += (finalSpeed * Time.deltaTime);
            isMoving = true;
        } else {
            isMoving = false;
        }

        if (isMoving != previousIsMoving) {
            animator.SetBool("is_moving", isMoving);
        }
        
    }
}
