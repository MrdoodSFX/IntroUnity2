using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Tooltip("degats"), Range(0, 100)]
    public float puissance = 30;
    public void OnCollisionEnter(Collision collision)
    {
        AIMover other = collision.gameObject.GetComponent<AIMover>();
        if (other != null)
        {
            other.life -= puissance;
            Destroy(gameObject);
        }
    }
}
