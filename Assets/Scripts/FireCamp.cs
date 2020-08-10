using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamp : MonoBehaviour
{
    public int uses = 99999;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(uses>0){
                other.GetComponent<PlayerController>().Heal();
                uses--;
            }
        }else if(other.tag == "Projectile"){
            other.GetComponent<Projectile>().ResetProjectile();
        }
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Ghost"){
            other.GetComponent<Ghost>().PushBack(transform.position);
        }
    }
}
