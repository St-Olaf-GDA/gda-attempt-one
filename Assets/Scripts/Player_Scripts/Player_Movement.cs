using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified by: Soua Yang

public class Player_Movement : MonoBehaviour
{
    [Header("Component Addition")]
    public Rigidbody2D rigidbody2D; //Use for 2D physics manipulation
    public BoxCollider2D boxcollider2D; //Use of the players collision box

    [Header("Custome Movement Values")]
    public static bool facing_right; //Check the proper player direction (left or right)
    public bool on_ground; // check if the player is on the ground
    public bool on_wall;
    public float jumping_force; // the force with which the player can jump
    public float feetDist; //distance between player feet and floor when grounded, this will need to be adjusted
    public float movement_speed;
    public int maxWallJump;
    public int wallJumpsLeft;
    public int gravityOfPlayer;
    //private float falling_threshold; //the threshold at which the game detects you as falling, not sure if we'll need this or not yet

    
    [Header("Dash Movement Modifiers")]
    public bool dash_refreshed; //Checks if the player's dash has been refreshed by touching the ground
    public int dash_permissions; // Checks how many of the dash movements a player can do
    public bool is_dashing; // Checks if player is currently in a state of dashing
    private Vector2 dash_direction; // checks which direction the dash is in
    public float dash_timer; // Determines how long the dashes are
    private float dash_speed; // Current speed of the dash
    private float dash_distance; // current distance of the dash

    public float dash_distance_default; //the initial distance covered by the dash
    public int dash_speed_default; // default speed of dash

    public float pawn_dash_distance_default; //Controls the length of the pawn dash;
    public int pawn_dash_speed;

    [Header("Cooldown Times")]
    public float dash_cooldown;
    private float dash_aftertime; //the time after which one can perform a cooldown
    public float pawn_dash_cooldown; //pawn dash cooldown
    private float pawn_dash_aftertime; //pawn dash aftertime

    //-----------------------------------------
    //UNITY BUILT-IN FUNCTIONS - START
    void Start(){
        wallJumpsLeft = maxWallJump;
        facing_right = true;
        on_ground = true;
        on_wall = false;
        dash_refreshed = true;
        //falling_threshold = 0f;
        feetDist = 3f;
        dash_aftertime = Time.time;
        pawn_dash_aftertime = Time.time;
        dash_permissions = 2; // We will need to change this allowance is high only for playtesting purposes
        dash_direction = Vector2.up;
        dash_stop();
    }

    // For only play first lands on Platform
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer== 8) {  //Mathf.Abs(rigidbody2D.velocity.y) <= falling_threshold){
            int solid_objects = LayerMask.GetMask("Platforms");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, feetDist, solid_objects);
            if (hit.collider != null)
            {
                // Play landing sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player SFX/Player Landing");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {  //Mathf.Abs(rigidbody2D.velocity.y) <= falling_threshold){

            int solid_objects = LayerMask.GetMask("Platforms");
            RaycastHit2D hit_down = Physics2D.Raycast(transform.position, Vector2.down, feetDist, solid_objects);
            RaycastHit2D hit_right = Physics2D.Raycast(transform.position, Vector2.right, feetDist, solid_objects);
            RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, feetDist, solid_objects);

            if (hit_down.collider != null) {
                on_ground = true;
                dash_refreshed = true;
                wallJumpsLeft = maxWallJump;
                on_wall = false;
            } else if (hit_right.collider != null || hit_left.collider != null && wallJumpsLeft > 0) {
                on_ground = true;
                dash_refreshed = true;
                on_wall = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        on_ground = false;
    }
    //UNITY BUILT-IN FUNCTIONS - END
    //-----------------------------------------



    //-----------------------------------------
    //CUSTOM-MADE FUNCTIONS - check_movement_presses() related - START

    /*check_movement_presses() - Checks the movement keys that have been pressed
    Please try to restrict this function to LOCATING the movement key presses, for ease of locating functions.
    Other functions will take care of movement
    Called by:
        - FixedUpdate()
    Current features:
        - Check if the Shift key has been pressed (triggers a speed increase)
        - Check if the Space key has been pressed (triggers a jump)
        - Check Left/Right movement:
            > Check if A or the Left Key have been pressed (movement to right)
            > Check if D or the Right Key have been pressed (movement to left)*/
    public void check_movement_presses() {
        //Set the player to sprint if they press the Shift Key
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) {
            movement_speed += 6f;
        }

        if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            movement_speed -= 6f;
        }

        //Check if the player is attempting to jump
        if (Input.GetButtonDown("Jump")) {
            simple_movement(0, movement_speed, true);
            dash_stop();
        }

        // Check if the player is going left or right
        float horizontal_movement = Input.GetAxisRaw("Horizontal");
        float vertical_movement = Input.GetAxisRaw("Vertical");
        simple_movement((int)horizontal_movement, movement_speed); //Calls simple_movement(). Determines player movement based on the direction they want to move (in this case left)

        if (Input.GetButton("Big_Dash") && Time.time > dash_aftertime && dash_permissions > 0) {
            if (vertical_movement == 0 && horizontal_movement == 0) {
                if (facing_right) {
                    dash_start(dash_distance_default, dash_speed_default, new Vector2(1, 0));
                } else {
                    dash_start(dash_distance_default, dash_speed_default, new Vector2(-1, 0));
                }
            } else  {
                dash_start(dash_distance_default, dash_speed_default, new Vector2(Mathf.Round(horizontal_movement), Mathf.Round(vertical_movement)));
            }
            dash_aftertime = Time.time + dash_cooldown;
        }

        if (Input.GetButton("Small_Dash") && dash_refreshed && Time.time > pawn_dash_aftertime) {
            //pawn_dash_movement(Mathf.Round(horizontal_movement), Mathf.Round(vertical_movement));
            pawn_dash_aftertime = Time.time + pawn_dash_cooldown;

            if (vertical_movement == 0 && horizontal_movement == 0) {
                if (facing_right)  {
                    dash_start(pawn_dash_distance_default, pawn_dash_speed, new Vector2(1, 0));
                } else {
                    dash_start(pawn_dash_distance_default, pawn_dash_speed, new Vector2(-1, 0));
                }
            } else {
                dash_start(pawn_dash_distance_default, pawn_dash_speed, new Vector2(Mathf.Round(horizontal_movement), Mathf.Round(vertical_movement)));
            }

            if (!on_ground)  {
                dash_refreshed = false; //player can not dash again until they collide with the floor
            }
        }

        // The player has a set amount of dash_time that it is dashing
        // dash_time = dash_distance / dash_speed aka physic formula
        if (is_dashing) {
            dash_timer = dash_timer - Time.deltaTime;

            if (dash_timer < 0f) {
                dash_stop();
            } else {
                dash_movement(dash_direction, dash_speed, dash_distance);
            }
        }
    }

    /* simple_movement() - moves and faces the player based on key bindings
    Called by:
        -simple_movement_presses()
    Included features:
        - Faces the player towards their previous movement
        - Walking/Running along the x-axis
        - Jumping with consideration of whether the collision box is touching the */
    void simple_movement(int movement_dir, float speed, bool jumping=false){      
        //Used to flip the player in the appropriate direction
            // Sees if the player avatar should be facing right or left
        bool position = facing_right;
        if (movement_dir == 1){
            facing_right = true;
        } else if (movement_dir == -1){
            facing_right = false;
        }

            // Sees if the direction of the player avatar is already set correctly
        if (facing_right != position){
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        // Used to move the player left or right
        rigidbody2D.velocity = new Vector2(speed * movement_dir, rigidbody2D.velocity.y);

        // Used to make the player jump
        if (jumping && on_ground){ 
            rigidbody2D.velocity = Vector2.up * jumping_force; //Makes the player avatar jump
            on_ground = false; // Notifies the game that the player is no longer touching the ground
            if (on_wall) wallJumpsLeft--;
        }
    }

    /* dash_movement() - makes the dash for the player
    Called by:
        -simple_movement_presses()
    Included features:
        - Can differentiate between movements, based on the player progress:
            > Rook (Simple dash with only Horizontal Movements)
            > Tower (Increased dash with added Vertical Movements)
            > Bishop (allows for vertical with added diagonal movements)*/
    void dash_movement(Vector2 direction, float dashspd, float dash_distance) {
        if (dash_permissions > 1) { dash_distance *= 2; } // Allow the players double their dash only if they have progressed
        if (dash_permissions < 3 && (direction.y !=0 && direction.x !=0)) { direction.x = 0; } //Don't allow vertical dash if the player is in early levels

        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = new Vector2(direction.x, direction.y / 2) * dashspd;
        //Debug.Log(rigidbody2D.velocity.ToString());

        

    }

    // dash_stop() - stops the player's dash
    void dash_stop() {
        // dash_timer is determined using physic's velocity formula
        rigidbody2D.gravityScale = gravityOfPlayer;
        is_dashing = false;
    }

    void dash_start(float distance, float speed, Vector2 direction) {
        is_dashing = true;
        dash_timer = distance / speed;
        dash_speed = speed;
        dash_distance = distance;
        dash_direction = direction;

        // Play pawn dash SFX
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player SFX/Pawn Dash");
    }
    //CUSTOM-MADE FUNCTIONS - check_movement_presses() related - END
    //-----------------------------------------

}