using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pocket;
    public ParticleSystem splash;
    private float lifeTime = 3f;
    private Rigidbody2D rb;
    private PlayerController player;
    private SoundEffect SE_vomit;
    private SpriteRenderer sprite;
    private Collider2D circleCollider2D;
    public Rigidbody2D Rb{
        get{return rb;}
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        SE_vomit = GetComponent<SoundEffect>();
        player = PlayerController.instance;
        rb=GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }
    public void Launch( Vector3 startPosition,Vector3 target){
        transform.position = startPosition + Vector3.back;
        gameObject.SetActive(true);
        float differenceX = startPosition.x - target.x;
        float differenceY = Mathf.Abs(startPosition.y - target.y);
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(-Random.Range(differenceX*40-300,differenceX*40+300),Random.Range(differenceY*40-300,differenceY*40+300),0));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        SE_vomit.playSound();
        
        if(other.gameObject.tag == "Player"){
            splash.Play();
            player.Damage();
        }
        if(other.gameObject.tag == "Floor"){
            splash.Play();
            //StartCoroutine("WaitBeforeReset");
        }
    }
    public void ResetProjectile(){
        transform.position = pocket.transform.position;
        circleCollider2D.enabled = true;
        SetAlpha(1);
        gameObject.SetActive(false);
        
    }
    IEnumerator LifeTime(){
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine("WaitBeforeReset");
    }
    IEnumerator WaitBeforeReset(){
        SetAlpha(0);
        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        ResetProjectile();
    }

    void SetAlpha(float a){
        Color c = sprite.color;
        c.a = a;
        sprite.color = c;
    }
}
