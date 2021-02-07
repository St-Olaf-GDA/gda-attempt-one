using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene_Manager : MonoBehaviour
{
    public List<CutScene> cutScenes;
    public int currentCutScene;
    public static bool inCutScene;

    // Start is called before the first frame update
    void Start()
    {
        // Starts the first cutscene at the beginning of the level
        currentCutScene = 0;
        startCutScene();
    }

    // Update is called once per frame
    void Update()
    {
        // Changing the panels of the cutscene
        if (Input.GetButtonDown("Interact") && inCutScene == true) {
            cutScenes[currentCutScene].changePanel();
            if (!cutScenes[currentCutScene].isActive()) {
                currentCutScene++;
                Time.timeScale = 1;
            }
        }
    }

    // Should be called from other scripts for when to start a cutscene
    public void startCutScene() {
        if (cutScenes[currentCutScene].screenToAppear == General.current_screen) {
            inCutScene = true;
            gameObject.GetComponent<Image>().enabled = true;
            cutScenes[currentCutScene].showCurrentPanel();
            Time.timeScale = 0;
        }
    }

    // Cutscene stores the panels of that cutscene
    [System.Serializable]
    public class CutScene {

        public List<Sprite> panels;
        private int currentPanel = 0;
        public Image imageComponent;
        public int screenToAppear;
        private bool active = true;

        public void changePanel() {
            currentPanel++;
            if (currentPanel < panels.Count) {
                
                showCurrentPanel();
            } else {
                // Ends cutscene once all panels are seen
                active = false;
                imageComponent.enabled = false;
                inCutScene = false;
                Debug.Log("No longer in cut scene");
            }
        }

        public void showCurrentPanel() {
            imageComponent.sprite = panels[currentPanel];
        }

        public int panelCount() {
            return panels.Count;
        }

        public bool isActive() {
            return active;
        }
    }
}
