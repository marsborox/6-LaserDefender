using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    //different speed is defined in objects
    [SerializeField] Vector2 moveSpeed;
    //material is material
    //offset so coordinates
    Vector2 offset;
    Material material;


    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Start()
    {
        
    }

    void Update()
    {
        offset = moveSpeed*Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
