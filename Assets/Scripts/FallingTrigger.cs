using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    public FallingTile fallingTile;
    private void OnTriggerEnter2D(Collider2D other) {
        fallingTile.IsFadingOut = false;
        fallingTile.enabled = true;
        
    }
}
