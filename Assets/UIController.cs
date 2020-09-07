using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameController gameController;
    [SerializeField] private GameObject tutorialHand;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.onHold.AddListener(OnGameStart);
        gameController.onFail.AddListener(OnLose);
        gameController.onGoal.AddListener(OnWin);
    }

    private void OnGameStart()
    {
        tutorialHand.SetActive(false);
    }

    private void OnLose()
    {
        loseScreen.SetActive(true);
    }

    private void OnWin()
    {
        Invoke("WinScreen", 1);
    }

    private void WinScreen()
    {
        winScreen.SetActive(true);
        gameController.canRestart = true;
    }
    
}
