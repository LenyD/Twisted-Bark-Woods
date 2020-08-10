using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject == player.gameObject){
           player.SetWinner();
        }
    }
}
