using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public void StartGame ()
    { 
        // runs when the player shoots the big PLAY button. Starts enemies spawning script.
        Destroy(transform.parent.gameObject, 0.5f);
        var enemyManager = GameObject.Find("EnemyManager");
        enemyManager.GetComponent<EnemyManager>().enabled = true;
    }

}
