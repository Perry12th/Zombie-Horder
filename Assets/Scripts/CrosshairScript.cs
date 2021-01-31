using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairScript : MonoBehaviour
{
    Vector2 CrosshairStartingPosition;

    public Vector2 CurrentMousePosition { get; private set; }

    public bool Inverted = false;

    public Vector2 MouseSensitivity = Vector2.zero;

    [SerializeField, Range(0.0f, 1.0f)]
    float HorizontalPercentageConstrain;
    [SerializeField, Range(0.0f, 1.0f)]
    float VectricalPercentageConstrain;

    float HorizontalConstrain;
    float VectricalConstrain;

    Vector2 CurrentLookDelta = Vector2.zero;

    float MinHorizontalConstrainValue;
    float MaxHorizontalConstrainValue;

    float MinVectricalConstrainValue;
    float MaxVectricalConstrainValue;

    GameInputActions InputActions;

    void Awake()
    {
        InputActions = new GameInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.CursorActive)
        {
            AppEvents.Invoke_MouseCursorEnable(false);
        }

        CrosshairStartingPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

        HorizontalConstrain = (Screen.width * HorizontalConstrain) / 2f;
        MinHorizontalConstrainValue = -(Screen.width / 2) + HorizontalConstrain;
        MaxHorizontalConstrainValue = (Screen.width / 2) - HorizontalConstrain;

        VectricalConstrain = (Screen.height * VectricalConstrain) / 2f;
        MinVectricalConstrainValue = -(Screen.width / 2) + VectricalConstrain;
        MaxVectricalConstrainValue = (Screen.width / 2) - VectricalConstrain;
    }

    // Update is called once per frame
    void Update()
    {
        float crosshairXPosition = CrosshairStartingPosition.x + CurrentLookDelta.x;
        float crosshairYPosition = Inverted ? CrosshairStartingPosition.y - CurrentLookDelta.y : CrosshairStartingPosition.y + CurrentLookDelta.y;

        CurrentMousePosition = new Vector2(crosshairXPosition, crosshairYPosition);

        transform.position = CurrentMousePosition;
    }

    void OnLook(InputAction.CallbackContext delta)
    {
        Vector2 mouseDelta = delta.ReadValue<Vector2>();

        CurrentLookDelta.x += mouseDelta.x * MouseSensitivity.x;
        if (CurrentLookDelta.x >= MaxHorizontalConstrainValue || CurrentLookDelta.x <= MinHorizontalConstrainValue)
        {
            CurrentLookDelta.x -= mouseDelta.x * MouseSensitivity.x;
        }

        CurrentLookDelta.y += mouseDelta.y * MouseSensitivity.y;
        if (CurrentLookDelta.y >= MaxVectricalConstrainValue || CurrentLookDelta.y <= MinVectricalConstrainValue)
        {
            CurrentLookDelta.y -= mouseDelta.y * MouseSensitivity.y;
        }
    }

    void OnEnable()
    {
        InputActions.Enable();
        InputActions.ThirdPerson.Look.performed += OnLook;
    }

    void OnDisable()
    {
        InputActions.Disable();
        InputActions.ThirdPerson.Look.performed -= OnLook;
    }

}
