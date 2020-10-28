using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    //how long the laser will live 
    public float lifetime = 2.0f;

    //how fast the laser will move
    public float speed = 5.0f;

    //how much damage the laser will do if it hits an enemy
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        //The game object that contains this component will be destroyed after lifetime seconds have passed;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
