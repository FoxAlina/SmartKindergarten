using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    private InputControl inputControl;

    private void Awake()
    {
        inputControl = new InputControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Start()
    {
        inputControl.Touch.TouchPress.started += ctx => StartTouch(ctx);
        inputControl.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("Start position: " + inputControl.Touch.TouchPosition.ReadValue<Vector2>());

        if (OnStartTouch != null) OnStartTouch(inputControl.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        //Debug.Log("End");

        if (OnEndTouch != null) OnEndTouch(inputControl.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }
}
