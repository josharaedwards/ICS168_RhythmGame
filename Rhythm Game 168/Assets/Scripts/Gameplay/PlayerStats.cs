using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    private const int maxHealth = 100;

    public int health = maxHealth;
    public int meter = 3;
    public int score = 0;

    public int wiggled = 0;

    public int superb = 0;
    public int good = 0;
    // public int almost = 0;
    public int bad = 0;
    public int gloomy = 0;
    // public int miss = 0;
    
    string LeftUp = "Left Up";
    string LeftDown = "Left Down";
    string RightUp = "Right Up";
    string RightDown = "Right Down";

    ReceiverController LeftStickUp, LeftStickDown, RightStickUp, RightStickDown;
    

    private PlayerInput powerUpInput;

    void Awake()
    {
        if (powerUpInput == null)
        {
            powerUpInput = GetComponent<PlayerInput>();
            powerUpInput.actions["Hinder (Lock Left)"].performed += LockLeftStick;
            powerUpInput.actions["Hinder (Lock Right)"].performed += LockRightStick;
            powerUpInput.actions["Hinder (Lock Both)"].performed += LockBothSticks;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        LeftStickUp = GameObject.Find(LeftUp).GetComponent<ReceiverController>();
        LeftStickDown = GameObject.Find(LeftDown).GetComponent<ReceiverController>();
        RightStickUp = GameObject.Find(RightUp).GetComponent<ReceiverController>();
        RightStickDown = GameObject.Find(RightDown).GetComponent<ReceiverController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LockLeftStick(InputAction.CallbackContext context)
    {
        if (meter >= 0)
        {
            if(LeftStickUp != null && LeftStickDown != null)
            {
                meter -= 1;
                wiggled = 0;
                LeftStickUp.Disable();
                LeftStickDown.Disable();
            }
        }
    }

    private void LockRightStick(InputAction.CallbackContext context)
    {
        if (meter >= 0)
        {
            if(RightStickUp != null && RightStickDown != null)
            {
                meter -= 1;
                wiggled = 0;
                RightStickUp.Disable();
                RightStickDown.Disable();
            }
        }
    }

    private void LockBothSticks(InputAction.CallbackContext context)
    {
        if (meter >= 0)
        {
            if(LeftStickUp != null && LeftStickDown != null && RightStickUp != null && RightStickDown != null)
            {
                meter -= 1;
                wiggled = 0;
                LeftStickUp.Disable();
                LeftStickDown.Disable();
                RightStickUp.Disable();
                RightStickDown.Disable();
            }
        }
    }

}
