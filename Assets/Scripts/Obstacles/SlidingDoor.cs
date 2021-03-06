﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour, IObstacle
{
    private bool activeInteraction = false;

    [SerializeField]
    private bool isBroken = false;

    [SerializeField]
    private bool cannotBeTurnedOff = false;

    [SerializeField]
    private GameObject thisObstacle = null;

    [SerializeField]
    private GameObject feedbackText = null;

    public Transform startPosition;
    public Transform endPosition;

    [SerializeField]
    private float speed = 2.0f;
    private float minSpeed = 1.0f;
    private float maxSpeed = 4.0f;

    private float randomSpeedTimer = 2.0f;

    public void Interaction(bool interacting)
    {
        if (activeInteraction && cannotBeTurnedOff) return;
        activeInteraction = interacting;
    }

    private void Update()
    {
        if(activeInteraction)
        {
            if(isBroken)
            {
                Vector3 v = startPosition.position;
                v.y += Mathf.Abs(startPosition.position.y - endPosition.position.y) * Mathf.Abs(Mathf.Sin(Time.time * speed));
                thisObstacle.transform.position = v;


                /*if (GameManager.Instance.GetRemoteBroken())
                {
                    if (randomSpeedTimer >= 0f) randomSpeedTimer -= Time.deltaTime;
                    else GetRandomSpeed();
                }*/
            }
            else
            {
                thisObstacle.transform.position = Vector3.Slerp(thisObstacle.transform.position, endPosition.position, Time.deltaTime);
            }
            
        }
    }

    private void GetRandomSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    public void ShowFeedbackText(bool _active)
    {
        if (feedbackText != null) feedbackText.SetActive(_active);
    }

}
