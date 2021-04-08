using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    [SerializeField] bool should_stop_moving = false;

    [SerializeField] float speed = 5f;
    [SerializeField] int direction_rightward = 1; //1 Means right, -1 means left
    [SerializeField] Vector2 edge_checker;
    [SerializeField] bool should_turn = false;

    [SerializeField] Vector2 origin;
    [SerializeField] float max_distance=50;

    [SerializeField] bool is_following_player=false;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed*direction_rightward, rb.velocity.y);
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Values_check();
        Change_direction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (gameObject.tag) {
            case "Talking_NPC":
                if (collision.tag == "Player") {
                    should_stop_moving = !should_stop_moving;
                    Debug.Log("Collision happened with " + collision.name + ".should_stop_moving status: " + should_stop_moving);
                    Movement_stopper();
                }
                break;
            case "Following_Enemy":
                is_following_player = !is_following_player;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (gameObject.tag) {
            case "Talking_NPC":
                if (collision.tag == "Player") {
                    should_stop_moving = !should_stop_moving;
                    Debug.Log("Collision happened with " + collision.name + ".should_stop_moving status: " + should_stop_moving);
                    Movement_stopper();
                }
                break;
            case "Following_Enemy":
                is_following_player = !is_following_player;
                break;
        }
    }

    void Values_check()
    {
        float centerX_offset = (transform.localScale.x + 0.5f) * direction_rightward;
        edge_checker = new Vector2(transform.position.x + centerX_offset, transform.position.y);
    }

    void Change_direction()
    {
        int solid_objects = LayerMask.GetMask("Platforms");
        RaycastHit2D hit_ground = Physics2D.Raycast(edge_checker, Vector2.down, 3f, solid_objects);
        RaycastHit2D hit_ahead = Physics2D.Raycast(transform.position, Vector2.right * direction_rightward, 3f, solid_objects);
        float current_distance = Vector2.Distance(origin, transform.position);
        int turn_randomizer = Random.Range(0, 1000);

        switch (gameObject.tag) {
            case "Talking_NPC":
                should_turn = hit_ahead.collider != null || hit_ground.collider == null || current_distance > max_distance || turn_randomizer == 10;
                if (should_turn && !should_stop_moving) {
                    direction_rightward *= -1;
                    rb.velocity = new Vector2(speed * direction_rightward, rb.velocity.y);
                }
                break;
            case "Following_Enemy":
                if (is_following_player){
                    
                } else {
                    should_turn = hit_ahead.collider != null || hit_ground.collider == null || current_distance > max_distance || turn_randomizer == 10;
                    if (should_turn && !should_stop_moving) {
                        direction_rightward *= -1;
                        rb.velocity = new Vector2(speed * direction_rightward, rb.velocity.y);
                    }
                }
                break;
        }
    }

    void Movement_stopper()
    {
        Debug.Log("STOP");
        if (should_stop_moving)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed * direction_rightward, rb.velocity.y);
        }
    }


}
