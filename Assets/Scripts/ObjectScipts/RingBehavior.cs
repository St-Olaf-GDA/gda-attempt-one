using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehavior : MonoBehaviour
{

    [SerializeField] Vector2 direction;
    [SerializeField] float distance;
    [SerializeField] int speed;
    private Vector2 origin;

    private void Start() {
        origin = transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromOrigin = Vector2.Distance(origin, transform.position);
        if (distanceFromOrigin < distance) {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
