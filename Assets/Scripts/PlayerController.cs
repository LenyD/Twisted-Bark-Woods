using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody2D rb;
    public float speed, acceleration, jumpHeight,camDistance;
    public Color invulFlashColor;
    public GameObject camTarget;
    public SpriteRenderer sprite;
    public Image healthUIIcon;
    public TMP_Text healthUIText;
    public Animator animator;
    public ParticleSystem blood;
    public SoundEffect SE_footstep, SE_hurt;
    private int maxNbJump = 2, nbJump = 0,nbFlash = 5;
    private  float horizontalAxis;
    private bool isLookingRight = true, isInvulnerable = false,isWinner = false,isGrounded = false, isAbleToMove = true;
    private float invulTimer = 2f;
    private Color baseColor;
    private MenuManager menuController;
    int maxHealth = 5, health;
    

    public bool IsLookingRight{
        get{return isLookingRight;}
    }
    public bool IsAbleToMove{
        get{return isAbleToMove;}
        set{isAbleToMove = value;}
    }
    public float VelocityX{
        get{return rb.velocity.x;}
        set{rb.velocity = new Vector2(value,rb.velocity.y);}
    }
    public bool IsGrounded{
        get{return isGrounded;}
    }
    public int Health{
        get{ return health;}
        set{
            health = value;
            healthUIText.SetText(health.ToString());
            Color ci = healthUIIcon.color;
            Color ct = healthUIText.color;
            ci.a = 1;
            ct.a = 1;
            healthUIIcon.color = ci;
            healthUIText.color = ct;
        }
    }
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SE_footstep = GetComponent<SoundEffect>();
        menuController = MenuManager.instance;
        Health = maxHealth;
        baseColor = sprite.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(horizontalAxis);
        if(rb.velocity.y < -10f && !isGrounded){
            nbJump = Mathf.Max(1,nbJump);

        }
        animator.SetFloat("isJumping",nbJump);
    }
    private void Update() {
        horizontalAxis = Input.GetAxis("Horizontal");
        animator.SetFloat("isMoving", Mathf.Min(Mathf.Abs(horizontalAxis),Mathf.Abs(rb.velocity.x)));
        //animator.SetFloat("isJumping",nbJump);
        if(healthUIIcon.color.a>0){
            FadeOutHealth();
        }
        if(horizontalAxis!=0){
            isLookingRight = (horizontalAxis>0);
        }
        if(Input.GetButtonDown("Jump")){
            Jump();
        }
        
    }
    private void Move(float direction){
        if(direction<0){
            sprite.flipX = true;
        }
        if(direction>0){
            sprite.flipX = false;
        }
        if(!isAbleToMove){
            return;
        }
        Vector3 targetVelocity = new Vector2(direction *speed* 5f, rb.velocity.y);
		rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity,acceleration *Time.deltaTime);
        
        camTarget.transform.position = Vector3.Lerp(camTarget.transform.position,transform.position + transform.right*direction*camDistance,Time.deltaTime);
    }
    private void FadeOutHealth(){
        float duration = 3;
        Color ci = healthUIIcon.color;
        Color ct = healthUIText.color;
        ci.a -= Time.deltaTime*1/duration;
        ct.a -= Time.deltaTime*1/duration;
        healthUIIcon.color = ci;
        healthUIText.color = ct;
    }
    private void Jump(){
        if(nbJump<maxNbJump){
            nbJump++;
            rb.velocity = Vector3.right * rb.velocity;
            rb.AddForce(Vector2.up * jumpHeight);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Floor"){
            nbJump = 0;
            isGrounded = true;
            SE_footstep.playSound();

        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Floor"){
            Debug.Log("Grounded");
            isGrounded = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Floor"){
            if(rb.velocity.y<=0 && rb.velocity.y>=-5f){
                nbJump = 0;
                isGrounded = true;
            }
        }
    }
    public void SetWinner(){
        isWinner = true;
    }
    public void Damage(){
        if(isInvulnerable){
            return;
        }
        StartCoroutine("Invulnerable");
        blood.Play();
        SE_hurt.playSound();
        Health--;
        if(health<=0){
            if(isWinner){
                menuController.Ending();
            }else{
                menuController.GameOver();
            }
        }
    }
    public void Heal(){
        Health = maxHealth;
    }

    IEnumerator Invulnerable(){
        isInvulnerable = true;
        StartCoroutine("Flash");
        yield return new WaitForSeconds(invulTimer);
        isInvulnerable = false;
    }
    IEnumerator Flash(){
        float secPerSwitch =invulTimer/ (nbFlash*2)*.9f;
        for (int i = 0; i < nbFlash; i++)
        {
            sprite.color = invulFlashColor;
            yield return new WaitForSeconds(secPerSwitch);
            sprite.color = baseColor;
            yield return new WaitForSeconds(secPerSwitch);
        }
    }

}
    
    
