using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightPush : MonoBehaviour
{
    
    public Color lightUpColor,readyLightColor;
    private Light2D light2D;
    public float cooldownDuration,cooldown,targetIntensity=1;
    private bool isLightingUp = false;
    private Color baseLightColor,targetLightColor;
    private Collider2D hitbox;
    private PlayerController player;
    // Start is called before the first frame update`
    private void Awake() {
    }
    void Start()
    {
        player = PlayerController.instance;
        hitbox = GetComponent<Collider2D>();
        light2D = GetComponent<Light2D>();
        baseLightColor = light2D.color;
        targetLightColor = baseLightColor;
        cooldown = cooldownDuration;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        if(cooldown>=cooldownDuration){
            targetLightColor = readyLightColor;
            targetIntensity = 1;
            if(Input.GetButtonDown("Light")){
                if(player.IsGrounded){
                    StartCoroutine(LightUp());
                }
            }
        }
        if(isLightingUp){
            light2D.color = Color.Lerp(light2D.color,targetLightColor,1/.2f*Time.deltaTime);
            light2D.intensity = Mathf.Lerp(light2D.intensity,targetIntensity,1/.2f*Time.deltaTime);
        }else{
            light2D.color = Color.Lerp(light2D.color,targetLightColor,1/.5f*Time.deltaTime);
            light2D.intensity = Mathf.Lerp(light2D.intensity,targetIntensity,1/.5f*Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Ghost"){
            other.GetComponent<Ghost>().PushBack(transform.position);
        }else if(other.tag == "Projectile"){
            other.GetComponent<Projectile>().ResetProjectile();
        }
    }

    IEnumerator LightUp(){
        cooldown = 0;
        player.IsAbleToMove = false;
        targetIntensity = 3;
        player.VelocityX = 0;
        targetLightColor = lightUpColor;
        isLightingUp = true;
        hitbox.enabled = true;
        yield return new WaitForSeconds(1/3f);
        targetIntensity = .75f;
        targetLightColor = baseLightColor;
        hitbox.enabled = false;
        isLightingUp = false;
        player.IsAbleToMove = true;
    }
}
