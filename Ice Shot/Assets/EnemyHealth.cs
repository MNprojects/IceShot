using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;           
    public int currentHealth;
    // The speed the enemy sinks through the floor after dying
    public float sinkSpeed = 2.5f;              
    
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    public bool isDead;
    bool isSinking = false;


    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {   
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        // Turn the collider into a trigger so shots pass through it. Rotate the body and 
        // start sinking to make it obvious the enemy is dead
        capsuleCollider.isTrigger = true;
        transform.rotation = new Quaternion(-90, 180, 0, 0);
        StartSinking();
    }

    
    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        // Make the Rigidbody kinematic so we can use translate to sink
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}