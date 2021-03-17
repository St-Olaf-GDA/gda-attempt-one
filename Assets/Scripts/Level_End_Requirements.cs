//Initiated by: George

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_End_Requirements : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int passing_requirement;
    [SerializeField] bool initiates_cutscene = false;
    [SerializeField] Scene scene;
    public int met_requirements = 0;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (met_requirements >= passing_requirement){
            //Pass state
            Debug.Log("Win");
            if (initiates_cutscene){
                //Code for cutscene
            } else {
                //SceneManager.LoadScene(scene);
            }
        } else {
            //Don't Pass state
            Debug.Log("");
        }
    }

}
