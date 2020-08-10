using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 parallaxMultiplier;
    private Transform cam;
    private Vector3 lastCamPosition;
    private float textureUnitSizeX;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        lastCamPosition = cam.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - lastCamPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x,deltaMovement.y * parallaxMultiplier.y,0);
        lastCamPosition = cam.position;
        if(Mathf.Abs(cam.position.x -transform.position.x) >= textureUnitSizeX*transform.localScale.x){
            float offset = (cam.position.x - transform.position.x) %textureUnitSizeX;
            transform.position = new Vector3(cam.position.x + offset,transform.position.y,transform.position.z);
        }
    }
}
