using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    // Time to wait before restarting the level
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Runs the Game Over and level reset sequence when the player dies.
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                // Claims to be deprecated but still works and issues with implementing the replacement
               Application.LoadLevel(Application.loadedLevel);      
            }
        }
    }
}