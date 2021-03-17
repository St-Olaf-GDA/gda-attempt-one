//Initiated by: George

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Follow : MonoBehaviour
{
    private Transform playerSprite;

    public float camUpperBoundX;
    public float camLowerBoundX;
    public float camUpperBoundY;
    public float camLowerBoundY;

    private void Start() {

        transform.position = new Vector3(camLowerBoundX, camLowerBoundY, -10);
    }

    // Update is called once per frame
    void LateUpdate() {
        playerSprite = GameObject.Find("Player").transform;

        float playerX = playerSprite.transform.position.x;
        float playerY = playerSprite.transform.position.y;

        bool isXBounded = playerX < camUpperBoundX && playerX > camLowerBoundX;
        bool isYBounded = playerY < camUpperBoundY && playerY > camLowerBoundY;

        if (isXBounded) {
            transform.position = new Vector3(playerX, transform.position.y, -10);
        }

        if (isYBounded) {
            transform.position = new Vector3(transform.position.x, playerY, -10);
        }
    }
}
