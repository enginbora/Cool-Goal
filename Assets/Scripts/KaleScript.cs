using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaleScript : MonoBehaviour
{
    private GameController gameController;
    [SerializeField] private GameObject[] confetties;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.onGoal.AddListener(OnGoal);
    }

    private void OnGoal()
    {
        foreach (var cnf in confetties)
        {
            cnf.SetActive(true);
        }
    }
}
