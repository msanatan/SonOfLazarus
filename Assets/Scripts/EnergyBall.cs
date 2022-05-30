using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] float lifeSpan = 3f;
    
    void Awake()
    {
        Destroy(gameObject, lifeSpan);
    }
}
