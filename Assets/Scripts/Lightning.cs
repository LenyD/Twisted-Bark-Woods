using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Lightning : MonoBehaviour
{
    public float minWait= 15f,maxWait = 120f,flashDuration = 0.1f, intensity = 10f; 
    private float defaultIntensity;
    private SoundEffect SE_thunder;
    private Light2D light2D;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
        defaultIntensity = light2D.intensity;
        SE_thunder = GetComponent<SoundEffect>();
        StartCoroutine("LightningTimer");
    }

    IEnumerator LightningTimer(){
        while(this.isActiveAndEnabled){
            float time = Random.Range(minWait,maxWait);
            
            yield return new WaitForSeconds(time);
            light2D.intensity = intensity;
            SE_thunder.playSound();
            yield return new WaitForSeconds(flashDuration);
            light2D.intensity = defaultIntensity;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
