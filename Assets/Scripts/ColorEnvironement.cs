using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEnvironement : MonoBehaviour
{
    [SerializeField] private Renderer myObject;
    void Start()
    {
        myObject.material.color = Color.red;
    }
  
}
