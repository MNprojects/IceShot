using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Data store for the gun. Used for future proofing if more guns were added.
    public GameObject barrel;
    public GameObject bulletHole;

    public float range = 10f;
    public int damage = 10;
    public float timeBetweenBullets = 0.2f;
}
