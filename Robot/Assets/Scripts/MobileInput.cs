using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public const float DEADZONE = 75.0f;
    public static MobileInput Instance { set; get; }

    public bool tap, swipeUp, swipeDown, swipeLeft, swipeRight;
    public Vector2 swipeDelta, startTouch;
    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        //reset
        tap = swipeUp = swipeDown = swipeLeft = swipeRight = false;

        //input

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }

        }

        #endregion

        //Calculate Distance
        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            //check with mobile
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }

            //check with standalone
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //calculate deadzone
        if (swipeDelta.magnitude >= DEADZONE)
        {
            //swipe confirmed
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Math.Abs(x) < Math.Abs(y))
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            if (Math.Abs(x) >= Math.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
