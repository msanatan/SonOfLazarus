using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] UnityEvent toggleSpiritWorld;
    [SerializeField] GameObject flashScreen;
    [SerializeField] GameObject doorScreen;
    [SerializeField] GameObject energyBall;
    [SerializeField] float energyBallSpeed = 5f;
    [SerializeField] float energyBallWaitTime = 1f;
    CharacterController characterController;
    Animator animator;
    Animator flashScreenAnimator;
    Animator doorAnimator;
    Transform energyBallSpawnPoint;
    Vector3 movement = Vector3.zero;
    bool canMove = true;
    bool isSpirit = false;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        flashScreenAnimator = flashScreen.GetComponent<Animator>();
        doorAnimator = doorScreen.GetComponent<Animator>();
        energyBallSpawnPoint = gameObject.transform.Find("EnergyBallSpawnPoint");

        if (toggleSpiritWorld == null)
        {
            toggleSpiritWorld = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ProcessInput()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
        }
        else if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.LeftShift)) && isSpirit && canShoot)
        {
            canShoot = false;
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Move()
    {
        if (canMove)
        {
            var newPosition = transform.position + movement.normalized;
            if (Physics.CheckSphere(newPosition, 0.5f))
            {
                characterController.SimpleMove(movement.normalized * speed);
            }
            transform.LookAt(newPosition);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadlyObstacle" && !isSpirit)
        {
            toggleSpiritWorld.Invoke();
            canMove = false;
            isSpirit = true;
            animator.SetBool("isDead", true);
            animator.SetBool("isRevived", false);
        }
        else if (other.gameObject.tag == "ReviveLight" && isSpirit)
        {
            toggleSpiritWorld.Invoke();
            canMove = false;
            isSpirit = false;
            animator.SetBool("isDead", false);
            animator.SetBool("isRevived", true);
            flashScreenAnimator.SetBool("isSpirit", false);
            flashScreenAnimator.SetBool("isHuman", true);
        }
        else if (other.gameObject.tag == "Door" && !isSpirit)
        {
            canMove = false;
            doorScreen.SetActive(true);
            doorAnimator.SetBool("reachedDoor", true);
        }
    }

    public void BecomeSpirit()
    {
        flashScreen.SetActive(true);
        flashScreenAnimator.SetBool("isSpirit", true);
        flashScreenAnimator.SetBool("isHuman", false);
    }

    public void SpiritReady()
    {
        canMove = true;
        animator.SetBool("isDead", false);
    }

    public void HumanReady()
    {
        flashScreen.SetActive(false);
        canMove = true;
        animator.SetBool("isRevived", false);
    }

    public void Shoot()
    {
        animator.SetBool("isShooting", false);
        var energyBallProjectile = Instantiate(energyBall, energyBallSpawnPoint.position, energyBallSpawnPoint.rotation);
        energyBallProjectile.SetActive(true);
        energyBallProjectile.GetComponent<Rigidbody>().velocity = energyBallSpawnPoint.forward * energyBallSpeed;
        // energyBallProjectile.GetComponent<MeshCollider>().convex = true;
        StartCoroutine(EnableShoot());
    }

    IEnumerator EnableShoot()
    {
        yield return new WaitForSeconds(energyBallWaitTime);
        canShoot = true;
    }
}
