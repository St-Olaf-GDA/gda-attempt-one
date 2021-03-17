//Initiated by: George

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Scene1_Dialogue_Manager : MonoBehaviour {

    string whole_dialogue = null;
    string current_path = null;
    int starting_index = 0;
    int separation_index = -1;

    public string dialogue_handling(string interaction_object, GameObject continue_indicator) {
        //string interaction_object = "NPC_Olivia";
        string path = Application.dataPath + "/Scripts/Scene1_Dialogue/" + interaction_object + ".txt"; //Find the dialogue option
        if (path != current_path){
            reset_dialogue_variables(); //Just in case they retain other values.
            current_path = path;
            whole_dialogue = File.ReadAllText(path);
        }       
        
        separation_index = whole_dialogue.IndexOf('^', starting_index);
        string return_value;
        if (separation_index >= 0){
            return_value = whole_dialogue.Substring(starting_index, separation_index - starting_index - 1);
            starting_index = separation_index + 1;

            continue_indicator.GetComponent<TextMeshProUGUI>().text = ">>";
        } else {
            return_value = whole_dialogue.Substring(starting_index);
            reset_dialogue_variables();
            continue_indicator.GetComponent<TextMeshProUGUI>().text = "[Exit]";
        }

        Debug.Log(separation_index);
        return return_value;
    }

    public bool is_dialogue_over(){
        return separation_index == -1;
    }

    void reset_dialogue_variables(){
        whole_dialogue = null;
        current_path = null;
        starting_index = 0;
        separation_index = -1;
    }
}
