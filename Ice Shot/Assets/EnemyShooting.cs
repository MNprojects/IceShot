using UnityEngine;


/*Class works in a very similar way to Player shooting. Could probably be redesigned into a single class/script.
 */

public class EnemyShooting : MonoBehaviour
{
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    Gun currentGun;
    EnemyAI enemyAI;
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        enemyAI = GetComponentInParent<EnemyAI>();
        enemyHealth = GetComponentInParent<EnemyHealth>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {   
        currentGun = GetComponentInParent<Gun>();
        timer += Time.deltaTime;

        // TODO Don't shoot if the player is already dead. Adding here seems to disable entirely.
        if (Vector3.Magnitude(transform.position - enemyAI.player.position)  <= currentGun.range && timer >= currentGun.timeBetweenBullets && !enemyHealth.isDead)
        {
            Shoot();
        }

        if (timer >= currentGun.timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;
        
        gunAudio.Play();
        
        gunLight.enabled = true;
        
        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.up;


        if (Physics.Raycast(shootRay, out shootHit, currentGun.range, shootableMask))
        {
           playerHealth = shootHit.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(currentGun.damage);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * currentGun.range);
        }
    }
}