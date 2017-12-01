using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    float timer;    // A timer to determine when to fire. Reset in Shoot(), used in Update()
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    Gun currentGun;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        currentGun = GetComponentInParent<Gun>();
        timer += Time.deltaTime;

        // checks for fire button and if the guns fire rate is ready to fire
        if (Input.GetButton("Fire1") && timer >= currentGun.timeBetweenBullets)
        {
            Shoot();
          
            Cursor.lockState = CursorLockMode.Locked; ;
        }

        if (timer >= currentGun.timeBetweenBullets * effectsDisplayTime)
        {
            // disables the effects if they have been running long enough
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;

        // Makes sure particles have stopped before playing them again
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer at the end of the player's gun
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Ray starts at the end of the gun and points forward from the barrel
        shootRay.origin = transform.position;
        shootRay.direction = transform.up;
        
        // Checks if the raycast hits something in the set shootable layer
        if (Physics.Raycast(shootRay, out shootHit, currentGun.range, shootableMask))
        {
            // If the ray hits something gets the health component
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // Make sure that it exists to avoid crashes when hitting things without health
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(currentGun.damage, shootHit.point);
            }

            // Used to start the game
            PlayButton playButton = shootHit.collider.GetComponent<PlayButton>();

            if (playButton != null)
            {
                playButton.StartGame();
            }
            

            // Set the second position of the line renderer to the point the raycast hit
            gunLine.SetPosition(1, shootHit.point);
        }
        // If it didn't hit anything on the right layer
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * currentGun.range);
        }
    }
}