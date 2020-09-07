using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class FootballScript : MonoBehaviour
{
    private GameController gameController;
    private Rigidbody rb;
    private SplineFollower followerSc;
    private bool launched;
    private RaycastHit hit;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.onKick.AddListener(OnKick);
        gameController.onGoal.AddListener(OnGoal);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        followerSc = GetComponent<SplineFollower>();
    }

    private void OnKick()
    {
        followerSc.autoFollow = true;
    }

    public void LaunchBall()
    {
        rb.useGravity = true;
        followerSc.enabled = false;
        rb.AddForce(transform.forward * 50, ForceMode.Impulse);
    }

    private void OnGoal()
    {
        rb.velocity *= 0.3f;
    } 

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward,out hit,2))
        {
            if (!launched && hit.collider.gameObject.CompareTag("Enemy") && followerSc.enabled)
            {
                LaunchBall();
                launched = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && !gameController.scored)
        {
            other.gameObject.SendMessage("GetHit");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            gameController.OnGoal();
        }
    }
}