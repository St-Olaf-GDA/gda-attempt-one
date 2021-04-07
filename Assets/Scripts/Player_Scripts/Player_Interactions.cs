//Initiated by: George

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interactions : MonoBehaviour
{
    public GameObject currentInteractible = null;
    public GameObject interact_bubble;

    [SerializeField] bool is_currently_interacting = false;

    public void check_for_interactions(){
        if(Input.GetButtonDown("Interact") && currentInteractible){
            is_currently_interacting = currentInteractible.GetComponent<Interactible>().Interact(is_currently_interacting);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        switch (other.tag){
/*            case "Talking_NPC":
                currentInteractible = other.gameObject;
                interact_bubble = currentInteractible.transform.Find("Dialogue_Icon").gameObject;
                interact_bubble.SetActive(true);
                break;*/
            case "NPC_Interaction_Collider":
                Debug.Log("Interaction Collider");
                currentInteractible = other.transform.parent.gameObject;
                interact_bubble = currentInteractible.transform.Find("Dialogue_Icon").gameObject;
                interact_bubble.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        switch (other.tag){
/*            case "Talking_NPC":
                if (other.gameObject == currentInteractible){
                    interact_bubble.SetActive(false);
                    interact_bubble = null;
                    currentInteractible = null;
                }
                break;*/
            case "NPC_Interaction_Collider":
                if (other.transform.parent.gameObject == currentInteractible){
                    interact_bubble.SetActive(false);
                    interact_bubble = null;
                    currentInteractible = null;
                }
                break;
        }
    }
}
