using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [HideInInspector] public UnityEvent onKick;
    [HideInInspector] public UnityEvent onGoal;
    [HideInInspector] public UnityEvent onHold;
    [HideInInspector] public UnityEvent onRelease; //when the player releases the input
    [HideInInspector] public UnityEvent onFail; 
    public bool kickedTheBall = false;
    public bool scored;
    public bool gameStarted;
    private bool failed;
    public bool canRestart;
    
    public void OnKick()
    {
        onKick.Invoke();
        kickedTheBall = true;
        StartCoroutine(CheckIfFail());
    }

    private void OnHold()
    {
        onHold.Invoke();
        gameStarted = true;
    }

    private void OnRelease()
    {
        onRelease.Invoke();
    }

    public void OnGoal()
    {
        onGoal.Invoke();
        scored = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator CheckIfFail()
    {
        yield return new WaitForSeconds(3);
        if (!scored)
        {
            OnFail();
        }
        StopCoroutine(CheckIfFail());
    }

    private void OnFail()
    {
        onFail.Invoke();
        failed = true;
        canRestart = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnHold();

            if (canRestart)
            {
                RestartLevel();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnRelease();
        }
    }
}
