using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    public Ghost[] ghosts;
    private int nbTrigger = 1;
    private SoundEffect SE_start;
    void Start()
    {
        player = PlayerController.instance;
        SE_start = GetComponent<SoundEffect>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject == player.gameObject){
            if(nbTrigger>0){
                foreach (Ghost ghost in ghosts)
                {
                    ghost.IsAggro = true;
                }
                nbTrigger--;
                SE_start.playSound();
            }
        }
    }
}
