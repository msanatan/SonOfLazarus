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
        StartCoroutine(MakeVisible());
    }

    IEnumerator MakeVisible()
    {
        yield return new WaitForSeconds(2f);
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