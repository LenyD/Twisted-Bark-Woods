using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{

    public Wolf wolf;
    private bool isGrounded;
    

    // Update is called once per frame
    void Update()
    {
        if(!isGrounded){
            wolf.SwitchSide();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
         if(other.gameObject.tag == "Floor")
        {
            
            isGrounded=false;
            
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
         if(other.gameObject.tag == "Floor")
        {
            isGrounded=true;
        }
    }
}
