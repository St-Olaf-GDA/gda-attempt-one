using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Script : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter music_emitter;
    public bool audio_mute_request;

    public int player_progression; // Keeps where the player is in the level

    // Start is called before the first frame update
    void Start(){
        audio_mute_request = false;
        music_emitter.Play();
        update_progression(0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.M) && !Game_Pause.game_is_paused){
            audio_mute_request = !audio_mute_request;
        }

        if (Game_Pause.game_is_paused || audio_mute_request) {
            if (music_emitter.IsPlaying()) music_emitter.Stop();
        } else {
            if (!music_emitter.IsPlaying()) music_emitter.Play();
        }

        update_progression(player_progression);
    }

    public void update_progression(int amount) {
        player_progression = amount;
        music_emitter.SetParameter("Progression", player_progression);
    }
}
