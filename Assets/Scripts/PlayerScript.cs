using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameController gameController;
    private Animator anim;
    [SerializeField] private Transform runPos;
    [SerializeField] private float runTime;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.onRelease.AddListener(OnRelease);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnRelease()
    {
        if (!gameController.kickedTheBall)
        {
            anim.SetTrigger("Run");
            transform.DOMove(runPos.position, runTime).OnComplete(Kick);    
        }
    }

    private void Kick()
    {
        anim.SetTrigger("Kick");
        Invoke(nameof(ShootTheBall), 0.3f);
    }

    private void ShootTheBall()
    {
        gameController.OnKick();
    }
}