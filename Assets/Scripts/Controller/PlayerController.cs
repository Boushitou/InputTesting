using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The ID serve to know which player it is.")]
    private int _id = 1;
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    private ControlScheme _controlScheme;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        StartCoroutine(TimerControllerCheck());
    }

    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        JumpingInput();
        MovementInput();
        FireInput();
    }

    private void JumpingInput()
    {
        if (CustomInputSystem.GetKeyDown("Jump" + _id.ToString(), _controlScheme))
        {
            _playerMovement.Jump();
        }
    }

    private void MovementInput()
    {
        float movementX = CustomInputSystem.GetAxis("Horizontal" + _id.ToString(), _controlScheme);
        _playerMovement.MovementInput = movementX;
    }

    private void FireInput()
    {
        if (CustomInputSystem.GetKeyDown("Fire" + _id.ToString(), _controlScheme))
        {
            Debug.Log("Fire !");
        }
    }

    private bool CheckConnectedController()
    {
        string[] joystickNames = Input.GetJoystickNames();

        if (joystickNames.Length >= _id)
        {
            for (int i = 0;  i < joystickNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(joystickNames[_id - 1]))
                {
                    _controlScheme = ControlScheme.Gamepad;
                    return true;
                }
            }
        }

        _controlScheme = ControlScheme.Keyboard;
        return false;
    }

    private IEnumerator TimerControllerCheck()
    {
        while (gameObject.activeSelf)
        {
            CheckConnectedController();
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
