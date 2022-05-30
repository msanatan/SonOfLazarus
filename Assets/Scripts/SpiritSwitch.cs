using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] GameObject flashScreen;
    [SerializeField] bool showInEditor = true;
    [SerializeField] bool isSpirit = false;
    Animator flashScreenAnimator;

    void Awake()
    {
        ToggleVisibility();
    }

    void Start()
    {
        flashScreenAnimator = flashScreen.GetComponent<Animator>();
    }

    public void AnimateWorldChange()
    {
        StartCoroutine(ChangeWorld());
    }

    IEnumerator ChangeWorld()
    {
        yield return new WaitForSeconds(1f);
        flashScreenAnimator.SetBool("isSpirit", isSpirit);
        flashScreenAnimator.SetBool("isHuman", !isSpirit);
        yield return new WaitForSeconds(1f);
        ToggleVisibility();
    }

    // public void BecomeSpirit()
    // {
    //     flashScreenAnimator.SetBool("isSpirit", true);
    //     flashScreenAnimator.SetBool("isHuman", false);
    // }

    // public void BecomeHuman()
    // {
    //     flashScreenAnimator.SetBool("isSpirit", false);
    //     flashScreenAnimator.SetBool("isHuman", true);
    // }

    public void ToggleVisibility()
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(isSpirit || (showInEditor && Application.isEditor));
        }

        isSpirit = !isSpirit;
    }
}
