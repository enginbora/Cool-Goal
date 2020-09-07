using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GoalieScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Animator anim;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private float speed;
    [SerializeField] private Collider mainCollider;
    [SerializeField] private List<Collider> rdColliders;
    [SerializeField] private List<Rigidbody> rdRbs;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        rdColliders = new List<Collider>();
        rdRbs = new List<Rigidbody>();
        rdColliders.AddRange(GetComponentsInChildren<Collider>());
        rdRbs.AddRange(GetComponentsInChildren<Rigidbody>());
        rdColliders.Remove(mainCollider);
        rdRbs.Remove(rb);
        
        SwitchRagdoll(false);
    }

    private void FixedUpdate()
    {
        if (transform.position.x >= pos1.position.x)
        {
            speed *= -1;

        }
        else if (transform.position.x <= pos2.position.x)
        {
            speed *= -1;
        }

        rb.velocity = Vector3.right * speed;
    }

    public void GetHit()
    {
        SwitchRagdoll(true);
    }

    private void SwitchRagdoll(bool myBool)
    {
        foreach (Rigidbody rig in rdRbs)
        {
            rig.isKinematic = !myBool;
            rig.useGravity = myBool;
        }

        foreach (var col in rdColliders)
        {
            col.enabled = myBool;
        }

        anim.enabled = !myBool;
    }
}
