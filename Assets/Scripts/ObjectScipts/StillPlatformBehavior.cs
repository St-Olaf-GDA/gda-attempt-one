using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initiated By: Soua

public class StillPlatformBehavior : MonoBehaviour
{

    public bool crumble;
    public bool destroyOnTouch;

    private GameObject player;

    private void Start() {
        // If we want the platform to crumble, then we'll also need destroyOnTouch to be true
        // This is just incase we forget to set destroyOnTouch to true
        if (crumble) destroyOnTouch = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // Detect player
        if (player == null) player = GameObject.Find("Player");

        if (collision.gameObject == player && destroyOnTouch) {
            if (crumble) {
                StartCoroutine("Crumble");
            } else {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Crumble() {
        // We need to have crumbling animation here
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
