using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initiated By: Soua
//Modified by: William

public class DynamicObjectBehavior : MonoBehaviour
{
    [SerializeField] float spinRadius;
    [SerializeField] float spinSpeed;
    [SerializeField] bool isSpinning;

    [SerializeField] bool isFalling;

    [SerializeField] Rigidbody2D rb;

    private Vector2 origin;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;

        // Disables physics if we don't need them
        if (isSpinning) {
            rb.isKinematic = true;
        } else if (isFalling) {
            gameObject.tag = "Untagged";
            rb.isKinematic = true;
        } else {
            rb.isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning) {
            Spin();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Detect player
        if (player == null) player = GameObject.Find("Player");

        if (collision.gameObject == player) {
            rb.isKinematic = false;
        }

        //this line I added below may work poorly with future functionality for our dynamic hazard objects. keep an eye on it!
        if (collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }

    private void Spin() {
        transform.position = new Vector2(origin.x + Mathf.Cos(spinSpeed * Time.time), origin.y + Mathf.Sin(spinSpeed * Time.time)) * spinRadius;
    }
}
