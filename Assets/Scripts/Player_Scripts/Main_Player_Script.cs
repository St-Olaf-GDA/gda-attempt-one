//Initiated by: George

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Player_Script : MonoBehaviour
{
    void Update(){
        gameObject.GetComponent<Player_Interactions>().check_for_interactions();
        //gameObject.GetComponent<Player_Movement>().check_movement_presses();
    }

    void FixedUpdate(){
        gameObject.GetComponent<Player_Movement>().check_movement_presses();
    }
}
