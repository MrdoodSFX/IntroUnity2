using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{

    private void Start()
    {
        Renderer myRenderer = GetComponent<Renderer>();
        colorRandom(myRenderer);
    }
    void colorRandom(Renderer platformRenderer)
    {
        Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        platformRenderer.material.color = color;
    }
}
