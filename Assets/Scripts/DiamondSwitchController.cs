using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSwitchController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] GameObject[] objectsToDestroy;
    [SerializeField] Material activatedMaterial;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnergyBall")
        {
            Destroy(other.gameObject);
            foreach (var obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }

        gameObject.GetComponentInChildren<MeshRenderer>().material = activatedMaterial;
    }
}
