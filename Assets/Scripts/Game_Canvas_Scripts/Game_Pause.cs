using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Pause : MonoBehaviour
{
    public static bool game_is_paused = false;
    public GameObject pauseMenu; //Set by UI

    private void Start() {
        game_is_paused = false;
    }

    // Update is called once per frame
    public void check_for_pause(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            game_is_paused = !game_is_paused;

            if (game_is_paused) {
                pause_game();
            } else {
                resume_game();
            }
        }
    }

    void pause_game(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void debug_player(){
        resume_game();
        GameObject.Find("Player").transform.position = new Vector2(-7, 3);
    }

    public void main_menu(){
        resume_game();
        GameObject.Find("Player").transform.position = new Vector2(-7, 3);
        SceneManager.LoadScene (sceneName:"Main Menu");
    }

    public void resume_game(bool button_activated = false) {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        if (button_activated) { 
            game_is_paused = !game_is_paused;
        }
    }
}
