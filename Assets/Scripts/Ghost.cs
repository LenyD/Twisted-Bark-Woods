using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,10)]
    public int nbProjectile=10;
    public float speed;
    public GameObject center;
    public Projectile[] projectiles;
    public SpriteRenderer eyeSprite;
    private PlayerController player;
    private float noiseX, noiseY,timeToSpit, movementRadius = 3,startTimeToSpit = 9f,stunDuration = 2f,targetSound =0.03f,randomX,randomY;
    private bool isAggro = false;
    private AudioSource audioSource;
    private SpriteRenderer sprite;
    
    public bool IsAggro{
        get{return isAggro;}
        set{isAggro = value;}
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = targetSound;
        player = PlayerController.instance;
        timeToSpit = startTimeToSpit*2;
        sprite = GetComponent<SpriteRenderer>();
        randomX = Random.Range(0,1000);
        randomY = Random.Range(0,1000);
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume -=  (audioSource.volume - targetSound)*Time.deltaTime;
        Follow();
    }

    private void Follow(){
        Wiggle();
        if(player.transform.position.x > center.transform.position.x){
            sprite.flipX = false;
        }else if(player.transform.position.x < center.transform.position.x){
            sprite.flipX = true;
        }

        eyeSprite.flipX = sprite.flipX;

        if(isAggro){
            if( Vector3.Distance(center.transform.position , player.transform.position)>10
            || (player.transform.position.x > center.transform.position.x && player.IsLookingRight) 
            || (player.transform.position.x < center.transform.position.x && !player.IsLookingRight)
            ){
                center.transform.position = Vector3.Lerp(center.transform.position,player.transform.position,speed/2 * Time.deltaTime );
                targetSound = 0.8f;
            }else{
                targetSound = 0.03f;
                
            }

            timeToSpit-=Time.deltaTime;
            if(timeToSpit<=0){
                Spit();
                timeToSpit= startTimeToSpit;
            }
        }
    }
    private void Wiggle(){
        
        noiseX = Mathf.PerlinNoise(Time.time,randomX)*Mathf.PI*2;
        noiseY = Mathf.PerlinNoise(randomY, Time.time)*Mathf.PI*2;
        
        if(isAggro && (player.transform.position.x > center.transform.position.x && player.IsLookingRight) || (player.transform.position.x < center.transform.position.x && !player.IsLookingRight)){
            transform.position = Vector3.Lerp(transform.position, center.transform.position + new Vector3(Mathf.Sin(noiseX), Mathf.Sin(noiseY),0)*movementRadius,speed*movementRadius*Time.deltaTime);
        }else{
            transform.position = Vector3.Lerp(transform.position, center.transform.position + new Vector3(Mathf.Sin(noiseX), Mathf.Sin(noiseY),0)*movementRadius/10,speed*movementRadius*Time.deltaTime);
        }
    }
    private void Spit(){
        sprite.flipX = false;
        for(int i = 0;i<nbProjectile;i++){
            projectiles[i].Launch(transform.position,player.transform.position);
        }
        StartCoroutine("Stunned");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            player.Damage();
        }
    }
    public void PushBack(Vector3 origin){
        Vector3 difference = (transform.position - origin);
        Debug.Log(difference);
        center.transform.position+= (center.transform.position - origin);
        StartCoroutine("Stunned");
    }
    
    IEnumerator Stunned(){
        isAggro = false;
        yield return new WaitForSeconds(stunDuration);
        isAggro = true;

    }
}
