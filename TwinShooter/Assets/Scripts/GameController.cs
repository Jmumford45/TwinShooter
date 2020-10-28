using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("User Interface")]
    //the value that will be printed
    private int score = 0;
    private int waveNumber = 0;
    //the actual GUI text objects
    public Text scoreText;
    public Text waveText;

    //our enemy to spawn
    public Transform enemy;

    [Header("Wave Properties")]
    //we want to delay our code at times
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = "Score: " + score;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    //Coroutine used to spawn enemies 
    IEnumerator SpawnEnemies()
    {
        //Give the player time before the wave starts
        yield return new WaitForSeconds(timeBeforeSpawning);

        //after timeBeforeSpawning has elapsed enter the wave loop
        while(true)
        {
            //Dont spawn anything new until all of the previous wave's enemies are dead
            if(currentNumberOfEnemies <= 0)
            {
                waveNumber++;
                waveText.text = "Wave: " + waveNumber;

                //spawn 10 enemies in random positions
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    //Enemies spawn off screen
                    //(Random.Range gives us a number between the first and second parameter
                    float randDistance = Random.Range(10, 25);

                    //enemies can come from any direction
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;

                    //use the distance and direction we set the position
                    enemyPos.x += randDirection.x * randDistance;
                    enemyPos.y += randDirection.y * randDistance;

                    //spawn the enemy and increment the number of enemies spawned
                    //(instantiate makes a clone of the parent object, places it with the second parameter and the rotation with the third)
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            //How much time to wait before checking if we need to spawn another wave
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    //allow classes outside of GameController to say when an enemy is killed
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }
}
