using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interactions : MonoBehaviour
{
    public GameObject currentInteractible = null;
    public GameObject interact_bubble;

    public void check_for_interactions(){
        if(Input.GetButtonDown("Interact") && currentInteractible){
            currentInteractible.SendMessage("Interact");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Talking_NPC")) {
            currentInteractible = other.gameObject;
            interact_bubble = currentInteractible.transform.Find("Dialogue_Icon").gameObject;
            interact_bubble.SetActive(true);
            Debug.Log(currentInteractible.name);
        }        
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Talking_NPC")) {
            if (other.gameObject == currentInteractible){
                interact_bubble.SetActive(false);
                interact_bubble = null;
                currentInteractible = null;
            }
        }
    }
}
