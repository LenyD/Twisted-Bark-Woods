using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    public GameObject[] wolfs;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject wolf in wolfs)
        {
            wolf.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        foreach (GameObject wolf in wolfs)
        {
            wolf.SetActive(true);
        }
    }
}
