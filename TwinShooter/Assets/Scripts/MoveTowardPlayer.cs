using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour
{
    private Transform player;
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        //finds the player ship and gets the transform component 
        player = GameObject.Find("PlayerShip").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Every frame moves the enemy from where it currently is to the direction where the player is at.
        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
    }
}
