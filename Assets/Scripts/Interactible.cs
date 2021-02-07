using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactible : MonoBehaviour{

    public GameObject dialogue_panel;
    public GameObject dialogue_text;
    public bool interacting = false;

    void Start() {
        dialogue_panel = Canvas_Main_Script.Dialogue_Panel_Parent;
        dialogue_text = dialogue_panel.transform.Find("Dialogue_Text(TMP)").gameObject;
    }

    public void Interact() {
        switch (gameObject.tag) {
            case "Talking_NPC":
                bool keep_interacting_status = false;
                if (!interacting){
                    keep_interacting_status = start_dialogue();
                } else {
                    end_dialogue();
                    keep_interacting_status = !keep_interacting_status;
                }
                if (keep_interacting_status) { interacting = !interacting; }
                break;
        }
    }

    bool start_dialogue() {
        Time.timeScale = 0;
        dialogue_panel.SetActive(true);
        string responce = gameObject.GetComponent<Scene1_Dialogue_Manager>().dialogue_handling(gameObject.name, dialogue_panel.transform.Find("Continue_Indicator").gameObject);
        dialogue_text.GetComponent<TextMeshProUGUI>().text = responce;
        return gameObject.GetComponent<Scene1_Dialogue_Manager>().is_dialogue_over();
    }

    void end_dialogue() {
        Time.timeScale = 1;
        dialogue_text.GetComponent<TextMeshProUGUI>().text = "";
        dialogue_panel.SetActive(false);
    }
}
