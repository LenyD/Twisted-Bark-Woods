using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FallingTile : MonoBehaviour
{
    public float timeToFade = 5, respawnTimer = 10f;
    public bool IsFadingOut{
        get{return isFadingOut;}
        set{isFadingOut = false;}
    }
    private TilemapCollider2D tileCollider;
    private Tilemap tilemap;
    private bool isFadingOut = false, isCrumbling = false;
    private ParticleSystem[] particles;
    // Start is called before the first frame update
    void Start()
    {
        tileCollider = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        particles = (ParticleSystem[]) GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadingOut){
            if(!isCrumbling){
                isCrumbling = true;
                StartParticles();
            }
            Color c = tilemap.color;
            c.a -= 1/timeToFade*Time.deltaTime;
            tilemap.color = c;
            if(c.a <=0){
                tileCollider.enabled = false;
                isFadingOut = false;
                StartCoroutine("Respawn");
            }
            
        }else{
            StopParticles();
            isCrumbling = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            isFadingOut = true;
        }
    }
    private void StartParticles(){
        foreach (ParticleSystem part in particles)
        {
            part.Play();
        }
    }
    private void StopParticles(){
        foreach (ParticleSystem part in particles)
        {
            part.Stop();
        }
    }

    IEnumerator Respawn(){
        yield return new WaitForSeconds(respawnTimer);
        Color c = tilemap.color;
        c.a = 1;
        tilemap.color = c;
        tileCollider.enabled = true;
    }
}
