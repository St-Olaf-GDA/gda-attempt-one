using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified by: Soua Yang, William Gilbert

public class General : MonoBehaviour
{
    public GameObject player_initializer;
    public GameObject cut_scene_manager;
    public int gravity_scale = 15;

    public static int current_screen;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player_object = Instantiate(player_initializer, new Vector2(-7, 3), Quaternion.identity);
        //player_object.GetComponent<Rigidbody2D>().gravityScale = gravity_scale;
        player_object.name = "Player";
        
        current_screen = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Currently used as a way to test the effects of changing screens
        if (Input.GetKeyDown("p")) {
            current_screen++;
            cut_scene_manager.GetComponent<CutScene_Manager>().startCutScene();
            Debug.Log(current_screen);
        }
    }
}
