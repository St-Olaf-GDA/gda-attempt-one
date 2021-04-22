using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//initiated by William

public class Hazard_Spawner : MonoBehaviour
{
    public GameObject hazard;
    float timer = 0.0f;
    List<GameObject> hazards;
    // Start is called before the first frame update
    void Start()
    {
        hazards = new List<GameObject>();
        GameObject tmpObj = Instantiate(hazard);
        tmpObj.transform.position = this.transform.position;
        hazards.Add(tmpObj);
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.9f) // the rate at which the hazards spawn
        {
            timer = 0.0f;
            //GameObject tmpObj = Instantiate(hazard);
            //tmpObj.transform.position = this.transform.position;
            //hazards.Add(tmpObj);
            

        }
    }

    //on any collision of the hazards, the oldest object to be spawned is deleted
    //this does not work currently, my code checks for a collision with the spawner itself rather than with the hazards, so i'll need to fix this
    private void onCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            hazards.RemoveAt(0);
        }

    }
}
