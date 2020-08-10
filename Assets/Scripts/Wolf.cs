using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1, roarDelayMin = 15,roarDelayMax = 30;
    public SoundEffect SE_roar;
    private Rigidbody2D rb;
    private PlayerController player;
    private Vector3 lastPosition;
    void Start()
    {
        player = PlayerController.instance;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("RoarTimer");
    }

    // Update is called once per frame
    void Update()
    {
        Move(transform.localScale.x);
    }
    public void SwitchSide(){
        
        lastPosition = transform.position;
        transform.localScale =  new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }
    private void Move(float direction){
        Vector3 targetVelocity = new Vector2(direction *speed* 5f, rb.velocity.y);
		rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity,1000f *Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            player.Damage();
        }
    }

    IEnumerator RoarTimer(){
        while(this.enabled){
            float waitTime = Random.Range(roarDelayMin,roarDelayMax);
            yield return new WaitForSeconds(waitTime);
            SE_roar.playSound();
        }
    }
}
