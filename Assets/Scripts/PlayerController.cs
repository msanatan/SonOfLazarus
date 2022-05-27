using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] UnityEvent becomeSpirit;
    Rigidbody rb;
    Animator animator;
    Vector3 movement = Vector3.zero;
    bool isSpirit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (becomeSpirit == null)
        {
            becomeSpirit = new UnityEvent();
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
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Move()
    {
        var newPosition = transform.position + movement.normalized;
        rb.MovePosition(transform.position + movement.normalized * speed * Time.deltaTime);
        transform.LookAt(newPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadlyObstacle" && !isSpirit)
        {
            // TODO: Kill player, enable spirit realm
            becomeSpirit.Invoke();
            isSpirit = true;
            animator.SetBool("isDead", true);
        }
        else if (other.gameObject.tag == "RejuvenationFlame" && isSpirit)
        {
            // TODO: Kill player, enable spirit realm
            becomeSpirit.Invoke();
            isSpirit = false;
        }
    }
}
