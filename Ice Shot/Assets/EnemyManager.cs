using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       
    public GameObject enemy;                
    public float spawnTime = 3f;    
    //An array for multiple spawn points, futureproofing
    public Transform[] spawnPoints;         


    void Start()
    {
        // Repeatedly runs Spawn after a delay
        InvokeRepeating("Spawn", 3f, spawnTime);
    }


    void Spawn()
    {
        // Don't spawn if the player is dead
        if (playerHealth.currentHealth <= 0f)
        {
            
            return;
        }

        // Picks a random spawn point within the array
        int currentSpawnPoint = Random.Range(0, spawnPoints.Length);

        // Creates a new enemy at the position found in currentSpawnPoint
        Instantiate(enemy, spawnPoints[currentSpawnPoint].position, spawnPoints[currentSpawnPoint].rotation);
    }
}