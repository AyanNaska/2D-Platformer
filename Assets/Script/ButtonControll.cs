using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonControll : MonoBehaviour
{
    private bool isLeftButtonPressed = false;
    private bool isRightButtonPressed = false;

    public float horizontalInput { get; private set; }
    public bool jumping;
    void Start()
    {
        // Get references to the left and right buttons
        GameObject leftButton = GameObject.Find("LeftButton");
        GameObject rightButton = GameObject.Find("RightButton");

        // Add EventTriggers to the buttons
        EventTrigger leftButtonTrigger = leftButton.AddComponent<EventTrigger>();
        EventTrigger rightButtonTrigger = rightButton.AddComponent<EventTrigger>();

        // Add pointer down events to the left button
        EventTrigger.Entry leftPointerDown = new EventTrigger.Entry();
        leftPointerDown.eventID = EventTriggerType.PointerDown;
        leftPointerDown.callback.AddListener((eventData) => { OnLeftPointerDown(); });
        leftButtonTrigger.triggers.Add(leftPointerDown);

        // Add pointer up events to the left button
        EventTrigger.Entry leftPointerUp = new EventTrigger.Entry();
        leftPointerUp.eventID = EventTriggerType.PointerUp;
        leftPointerUp.callback.AddListener((eventData) => { OnLeftPointerUp(); });
        leftButtonTrigger.triggers.Add(leftPointerUp);

        // Add pointer down events to the right button
        EventTrigger.Entry rightPointerDown = new EventTrigger.Entry();
        rightPointerDown.eventID = EventTriggerType.PointerDown;
        rightPointerDown.callback.AddListener((eventData) => { OnRightPointerDown(); });
        rightButtonTrigger.triggers.Add(rightPointerDown);

        // Add pointer up events to the right button
        EventTrigger.Entry rightPointerUp = new EventTrigger.Entry();
        rightPointerUp.eventID = EventTriggerType.PointerUp;
        rightPointerUp.callback.AddListener((eventData) => { OnRightPointerUp(); });
        rightButtonTrigger.triggers.Add(rightPointerUp);
    }

    void Update()
    {
        HandleInput();
    }

    void OnLeftPointerDown()
    {
        isLeftButtonPressed = true;
    }

    void OnLeftPointerUp()
    {
        isLeftButtonPressed = false;
    }

    void OnRightPointerDown()
    {
        isRightButtonPressed = true;
    }

    void OnRightPointerUp()
    {
        isRightButtonPressed = false;
    }

    void HandleInput()
    {
        horizontalInput = 0f;

        if (isLeftButtonPressed)
        {
            horizontalInput -= 1f;
        }

        if (isRightButtonPressed)
        {
            horizontalInput += 1f;
        }

    }
    public void JumpButton()
    {
        jumping = true;
        jumping = false;
    }
}