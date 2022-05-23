using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] bool showInEditor = true;
    [SerializeField] bool currentValue = false;

    // Start is called before the first frame update
    void Start()
    {
        ToggleVisibility();
    }

    public void ToggleVisibility()
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(currentValue || (showInEditor && Application.isEditor));
        }

        currentValue = !currentValue;
    }
}
