using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
    
{
   
    Material material;
    Vector2 offset;

    //velocidad en la que se mueve el fondo
    public int xVelocity, yVelocity;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    
    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }

    void Update()
    {
            material.mainTextureOffset += offset * Time.deltaTime;
    }
}


//Charger games (Unity 2D Infinite Scrolling BG).