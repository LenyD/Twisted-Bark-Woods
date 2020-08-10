using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public Wolf wolf;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Floor" || other.tag == "Wolf" || other.tag == "Player"){
            wolf.SwitchSide();
        }
    }
}
