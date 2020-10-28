using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //amount of health of each enemy 
    public int health = 2;
    //when the enemy dies we play an explosion
    public Transform explosion;
    //what sound to play when hit
    public AudioClip hitSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check for collision (comment out if works)
        Debug.Log("Hit" + collision.gameObject.name);

        //looks for 'laser' in the names of anything collided
        if (collision.gameObject.name.Contains("Laser"))
        {
            LaserBehaviour laser = collision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(collision.gameObject);

            //plays a sound from this object's AudioSource
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            controller.KilledEnemy();
            controller.IncreaseScore(10);

            //check if the explosion was set
            if(explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }
        }
    }
}
