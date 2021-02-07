using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 playerPos;
    private Vector2 direction; // Direction the npc is going

    private int feetDist; // Distance from how far the npc position is from ground
    public int speed;
    private bool canMove;

    [Header("Conditions")]
    public int distanceEdgeCondition; // Max distance of npc to an edge
    public int distanceWallCondition; // Max distance of npc to a wall
    public int distanceOriginCondition; // Max distance that an npc can travel from its origin
    public int distancePlayerCondition; // Npc will stop moving once the npc is distancePlayerCondition away from player

    [Header("Randomness")]
    public int maxWaitTime;
    public int minWaitTime;
    private float randomEventTime;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        direction = Vector2.right;
        canMove = true;
        feetDist = 3;
        randomEventTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
    }

    // Update is called once per frame
    void Update()
    {
        int solid_objects = LayerMask.GetMask("Platforms");
        Vector2 point2shoot = new Vector2(transform.position.x + distanceEdgeCondition * direction.x, transform.position.y);
        RaycastHit2D hit_ground = Physics2D.Raycast(point2shoot, Vector2.down, feetDist, solid_objects);
        RaycastHit2D hit_ahead = Physics2D.Raycast(transform.position, direction, distanceWallCondition, solid_objects);

        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;

        bool isColliding = hit_ground.collider == null || hit_ahead.collider != null;
        bool isFarFromOrigin = Vector2.Distance(origin, transform.position) >= distanceOriginCondition;
        bool playerNearOrigin = Vector2.Distance(origin, playerPos) <= distanceOriginCondition;
        bool isNearPlayer = Vector2.Distance(playerPos, transform.position) <= distancePlayerCondition;

        //Debug.Log("Random Event Time: " + randomEventTime);

        if (Time.time >= randomEventTime) {

            // 0 - npc stops for a bit
            // 1 - npc changes direction
            int random = Random.Range(0, 2);

            if (random == 0) {
                changeDirection();
                canMove = false;
            } else {
                changeDirection();
            }

            randomEventTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
        }

        if (isColliding || isFarFromOrigin) { // Makes sure that npc will not collide with wall or go off an edge

            changeDirection();

        } else {
            if (playerNearOrigin) {

                if (isNearPlayer) { // Stops npc from moving if too close to player
                    canMove = false;
                } else {
                    // If player is near origin, npc will go to player
                    if (transform.position.x < playerPos.x && direction.x == -1) { 
                        changeDirection();
                    } else if (transform.position.x > playerPos.x && direction.x == 1) {
                        changeDirection();
                    }

                    canMove = true;
                }

            }
        }

        if (canMove) transform.Translate(direction * speed * Time.deltaTime);
    }

    // Toggles npc direction
    public void changeDirection() {
        direction *= -1;
        transform.localScale *= new Vector2(-1, 1);
        canMove = true;
    }
}
