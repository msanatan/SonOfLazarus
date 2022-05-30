using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    MeshRenderer meshRenderer;
    bool visible;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        visible = meshRenderer.enabled;
    }
    public void ToggleVisibility()
    {
        meshRenderer.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnergyBall")
        {
            Destroy(other.gameObject);
        }

        gameObject.SetActive(false);
    }
}