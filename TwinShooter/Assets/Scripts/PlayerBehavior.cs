using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    //movement modifier applied to directional movement
    public float playerSpeed = 4.0f;
    //what the current speed of our player is
    private float currentSpeed = 0.0f;
    //the last movement that we've made
    private Vector3 lastMovement = new Vector3();

    //The laser we will be shooting 
    public Transform laser;
    //How far from the center of the ship should the laser be 
    public float laserDistance = .2f;
    //Time before the ship can fire again
    public float timeBetweenFires = 0.3f;
    //if value is less than or equal 0, the ship can fire
    private float timeTillNextFire = 0.0f;

    //The buttons that we can use to shoot lasers
    public List<KeyCode> shootButton;

    //what sound to play when we're shooting 
    public AudioClip shootSound;
    //Reference to audio source component
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehaviour.isPaused)
        {
            //Rotate the player to face the mouse
            Rotation();
            //Move the player's body
            Movement();

            //A foreach loop will go through each item inside of shootButton and do whatever we placed in {} using the element variable to hold the item
            foreach (KeyCode element in shootButton)
            {
                if (Input.GetKey(element) && timeTillNextFire < 0)
                {
                    timeTillNextFire = timeBetweenFires;
                    ShootLaser();
                    break;
                }
            }
            timeTillNextFire -= Time.deltaTime;
        }
    }

    //will rotate the ship to face the mouse
    void Rotation()
    {
        //Need to tell where the mouse is relative to the player
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        //Get the difference from each axis
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        //Get the angle between the two objects
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        //Z-axis is for rotation in 2D. Transform rotation uses a Quaternion. convert to a Vector
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        //Assign rotation
        this.transform.rotation = rot;

    }

    void Movement()
    {
        //The movement that needs to occur this frame
        Vector3 movement = new Vector3();

        //check for input 
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        //if user pressed multiple buttons, make sure we're only moving the same length
        movement.Normalize();

        //check if we pressed anything 
        if(movement.magnitude > 0)
        {
            //if we did move in that direction
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;
        }
        else
        {
            //Otherwise keep moving in the direction we were going
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            //slow down over time 
            currentSpeed *= .9f;
        }
    }

    //Creates a laser and gives it an intial position in front of the ship
    void ShootLaser()
    {
        audioSource.PlayOneShot(shootSound);

        //Position the laser in relation to our player's location
        Vector3 laserPos = this.transform.position;
        //the angle the laser will move away from the center
        float rotationAngle = transform.localEulerAngles.z - 90;
        //Calculate the position right in front of the ship's position laserDistance units away
        laserPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        laserPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);

        Instantiate(laser, laserPos, this.transform.rotation);
    }
}
