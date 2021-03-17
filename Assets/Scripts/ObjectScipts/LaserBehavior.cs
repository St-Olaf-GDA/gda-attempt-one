using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    [SerializeField] Vector2 laserDirection;

    // Update is called once per frame
    void Update()
    {
        Vector2 point2shot = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D ray = Physics2D.Raycast(point2shot, laserDirection);

        if (ray.collider.gameObject.name == "Player") {
            ray.collider.gameObject.GetComponent<Kill_Player>().player_death();
        }
    }
}
