using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    NavMeshAgent agent;
    Gun currentGun;
    EnemyHealth enemyHealth;
 




    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentGun = GetComponentInChildren <Gun>();
        enemyHealth = GetComponent<EnemyHealth>();


    }
    void Update()
    {
        // Makes the enemy track the current position of the player and face them. Used an offset to adjust for the gun position.
        // TODO Adjustment currently off if player has a greater x co-ord than enemy.
        Vector3 relativePos = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos + new Vector3(0.3f, -0.4f, 0));
    }
    
    void FixedUpdate()
    {
        // Checks that the player is alive and gets the player into range of their currently equipped gun
        // using the Navmesh. Tries to back away if too close to the player.
        // TODO create way of tracking if player is in line of sight. Probably using raycast.
        if (!enemyHealth.isDead)
        {
            if (Vector3.Magnitude(transform.position - player.position) < 5f)
            {
                agent.SetDestination(transform.position - player.position);
                
            }
            else if (Vector3.Magnitude(transform.position - player.position) > currentGun.range - 10)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
    }

}
