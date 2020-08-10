using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public GameObject camTarget;
    public float camDistance;
    public bool canJump = true;
    private  float horizontalAxis;
    private bool isLookingRight = true;
    public bool IsLookingRight{
        get{return isLookingRight;}
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        if(horizontalAxis!=0){
            isLookingRight = (horizontalAxis>0);
        }
        Move(horizontalAxis);
    }
    private void Update() {
        if(Input.GetButtonDown("Jump")){
            Jump();
        }
    }
    private void Move(float direction){
        transform.Translate(transform.right*direction*speed*Time.deltaTime);
        camTarget.transform.position = transform.position + transform.right*direction*camDistance;
    }
    private void Jump(){
        if(canJump){
            canJump = false;
            rb.AddForce(Vector2.up * jumpHeight - new Vector2(0,Mathf.Min(rb.velocity.y,0)*100));
        }
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Floor"){
            canJump = true;
        }
        
    }
}
    
    
