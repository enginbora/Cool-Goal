using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SplineController : MonoBehaviour
{
    [SerializeField] private SplineComputer spline;
    [SerializeField] private float sensPoint0;  //movement sensitivity of the bezier curves control points
    [SerializeField] private float sensPoint1;
    [SerializeField] private float sensPoint2;
    [SerializeField] private float sensPoint3;
    [SerializeField] private GameObject[] splineDots;  //trajectory dots

    private bool areDotsActive;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.onHold.AddListener(OnHold);
        gameController.onRelease.AddListener(OnRelease);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeDotVisibility(false);
        
    }

    private void ChangeDotVisibility(bool visibility)
    {
        areDotsActive = visibility;
        foreach (var dot in splineDots)
        {
            dot.GetComponent<MeshRenderer>().enabled = visibility;
        }
    }

    private void OnHold()
    {
        if (!areDotsActive && !gameController.kickedTheBall)
        {
            ChangeDotVisibility(true);
        }
    }

    private void OnRelease()
    {
        if (areDotsActive)
        {
            ChangeDotVisibility(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ArrangeDots();
        }
    }

    private void ArrangeDots() //setting and clamping the position of the trajectory dots
    {
        spline.SetPointPosition(1, spline.GetPoint(1).position + Vector3.right * (Input.GetAxis("Mouse X") * sensPoint1));
        spline.SetPointPosition(2, spline.GetPoint(2).position + Vector3.right * (Input.GetAxis("Mouse X") * sensPoint2));
        spline.SetPointPosition(3, spline.GetPoint(3).position + Vector3.left * (Input.GetAxis("Mouse X") * sensPoint3));

        spline.SetPointPosition(1,
            new Vector3(Mathf.Clamp(spline.GetPoint(1).position.x, -4, 4), spline.GetPoint(1).position.y,
                spline.GetPoint(1).position.z));
        spline.SetPointPosition(2,
            new Vector3(Mathf.Clamp(spline.GetPoint(2).position.x, -4, 4), spline.GetPoint(2).position.y,
                spline.GetPoint(2).position.z));
        spline.SetPointPosition(3,
            new Vector3(Mathf.Clamp(spline.GetPoint(3).position.x, -1, 1), spline.GetPoint(3).position.y,
                spline.GetPoint(3).position.z));
    }
}