using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUpController : MonoBehaviour
{
    
    string LeftUp = "Left Up";
    string LeftDown = "Left Down";
    string RightUp = "Right Up";
    string RightDown = "Right Down";

    ReceiverController LeftStickUp, LeftStickDown, RightStickUp, RightStickDown;
    private ScoreTracker scoreTracker;

    private PlayerInput powerUpInput;

    void Awake()
    {
        powerUpInput = GetComponentInParent<PlayerSpawner>().assignedPlayerInput;
        powerUpInput.actions["Hinder (Lock Left)"].performed += LockLeftStick;
        powerUpInput.actions["Hinder (Lock Right)"].performed += LockRightStick;
        powerUpInput.actions["Hinder (Lock Both)"].performed += LockBothSticks;

    }
    // Start is called before the first frame update
    void Start()
    {
        LeftStickUp = GameObject.Find(LeftUp).GetComponent<ReceiverController>();
        LeftStickDown = GameObject.Find(LeftDown).GetComponent<ReceiverController>();
        RightStickUp = GameObject.Find(RightUp).GetComponent<ReceiverController>();
        RightStickDown = GameObject.Find(RightDown).GetComponent<ReceiverController>();

        scoreTracker = ScoreTracker.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LockLeftStick(InputAction.CallbackContext context)
    {
        if (scoreTracker.meter >= 0)
        {
            if(scoreTracker != null && LeftStickUp != null && LeftStickDown != null)
            {
                scoreTracker.meter -= 1;
                scoreTracker.wiggled = 0;
                LeftStickUp.Disable();
                LeftStickDown.Disable();
            }
        }
    }

    private void LockRightStick(InputAction.CallbackContext context)
    {
        if (scoreTracker.meter >= 0)
        {
            if(scoreTracker != null && RightStickUp != null && RightStickDown != null)
            {
                scoreTracker.meter -= 1;
                scoreTracker.wiggled = 0;
                RightStickUp.Disable();
                RightStickDown.Disable();
            }
        }
    }

    private void LockBothSticks(InputAction.CallbackContext context)
    {
        if (scoreTracker.meter >= 0)
        {
            if(scoreTracker != null && LeftStickUp != null && LeftStickDown != null && RightStickUp != null && RightStickDown != null)
            {
                scoreTracker.meter -= 1;
                scoreTracker.wiggled = 0;
                LeftStickUp.Disable();
                LeftStickDown.Disable();
                RightStickUp.Disable();
                RightStickDown.Disable();
            }
        }
    }

}
