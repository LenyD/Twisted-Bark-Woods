using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;


public class Switch : MonoBehaviour
{
    public Transform[] doors;
    public Image lever;
    public Light2D light2D;
    public float stageMultiplier1 = 0.5f,stageMultiplier2 = 1f,stageMultiplier3 = 6f,startTimer = 10;
    private int stage = 0,lastStage;
    private Vector3[] startPosition;
    private SoundEffect SE_door;
    
    float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        SE_door = GetComponent<SoundEffect>();
        timeLeft = startTimer;
        startPosition = new Vector3[doors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            startPosition[i] = doors[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timeLeft<startTimer){
            timeLeft+= Time.deltaTime;
        }
        for (int i = 0; i < doors.Length; i++)
        {
            Vector3 targetPosition = startPosition[i];
            if(timeLeft/startTimer<= 0.25){
                targetPosition += Vector3.down*stageMultiplier3;
                stage = 3;
            }else if(timeLeft/startTimer<= 0.5){
                targetPosition += Vector3.down*stageMultiplier2;
                stage = 2;
            }else if(timeLeft/startTimer<= 0.75){
                targetPosition += Vector3.down*stageMultiplier1;
                stage = 1;
            }else{
                stage = 0;
            }
            if(stage!=lastStage){
                SE_door.playSound();
            }
            lastStage = stage;
            doors[i].position = Vector3.Lerp(doors[i].position, targetPosition,0.01f);

        }

        float percent = (1-timeLeft/startTimer);
        lever.fillAmount = percent;
        light2D.intensity = percent+1f;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            timeLeft -= Time.deltaTime*2;
            if(timeLeft<0){
                timeLeft = 0;
            }
        }
    }
}
