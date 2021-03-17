//Initiated by: George

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Main_Script : MonoBehaviour
{
    public GameObject DPP; //Defined via the editor
    public static GameObject Dialogue_Panel_Parent;

    void Awake() {
        Dialogue_Panel_Parent = DPP;
    }

    void Start(){
       //GameObject.Find("Dialogue_Panel").SetActive(false);
    }

    void Update(){
        gameObject.GetComponent<Game_Pause>().check_for_pause();
    }
}
