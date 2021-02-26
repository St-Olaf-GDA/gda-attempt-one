using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixMenuProps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Main Menu Graphic Stretching
        GameObject MM = GameObject.Find ("Canvas/Image");
        var MMT = MM.transform as RectTransform;
        MMT.sizeDelta = new Vector2 (Screen.width, Screen.height);
        //Start Button Graphic Stretching
        GameObject SB = GameObject.Find ("Canvas/Button");
        var SBT = SB.transform as RectTransform;
        SBT.sizeDelta = new Vector2 (Screen.width/3, Screen.height/8);
        SB.transform.position = new Vector2 (Screen.width/2, Screen.height/8);
        //Fixtry for text scale, doesn't seem to change
        GameObject Text = GameObject.Find ("Canvas/Button/Text");
        var TextT = Text.transform as RectTransform;
        TextT.sizeDelta = new Vector2 (Screen.width/3, Screen.height/8);
        TextT.transform.position = new Vector2 (Screen.width/2, Screen.height/8);
        
        //MM.width = Screen.width;
        //MM.sizeDelta = new Vector3(Screen.width/500, Screen.height/500);
        //transform.localScale = new Vector3(Screen.width/500, Screen.height/500);
    }
}
