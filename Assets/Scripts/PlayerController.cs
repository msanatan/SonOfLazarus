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
    Vector3 movement = Vector3.zero;
    bool isSpirit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    private void Move()
    {
        rb.MovePosition(transform.position + movement.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadlyObstacle" && !isSpirit)
        {
            // TODO: Kill player, enable spirit realm
            becomeSpirit.Invoke();
            isSpirit = true;
        }
        else if (other.gameObject.tag == "RejuvenationFlame" && isSpirit)
        {
            // TODO: Kill player, enable spirit realm
            becomeSpirit.Invoke();
            isSpirit = false;
        }
    }
}
