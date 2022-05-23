using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static event Action<PlayerStats> imDead, wonMyGame; //<--- Observer pattern
    public static event Action<int> totScoreChange; //<--- Observer pattern
    private const int maxHealth = 100;
    public const float maxMeter = 3.0f;

    public bool editorMode = false;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI scoreAmount;

    public int health = maxHealth;
    private bool isDead = false;
    public float meterFill = 0.0f;
    public int score = 0;

    public int healthGloomyDmg = 5;
    public float meterGloomyDmg = 1.0f;
    public int scorePerBad = 100;
    public float meterPerBad = 0.01f;
    public int healthPerBad = 1;
    public int scorePerGood = 250;
    public float meterPerGood = 0.05f;
    public int healthPerGood = 3;
    public int scorePerSuperb = 500;
    public float meterPerSuperb = 0.1f;
    public int healthPerSuperb = 5;

    public int currentMultiplier = 1;
    public int multiplierTracker = 0;
    public int[] multiplierThresholds;
    public float multiplierTimeLimit = 5.0f;
    private bool multiStarted = false;

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
    
    [SerializeField] private Animator[] animators;
    private PlayerInput powerUpInput;

    

    void Awake()
    {
        if (powerUpInput == null)
        {
            powerUpInput = GetComponent<PlayerInput>();
            powerUpInput.actions["Hinder (Lock Left)"].performed += LockLeftStick;
            powerUpInput.actions["Hinder (Lock Right)"].performed += LockRightStick;
            powerUpInput.actions["Hinder (Lock Both)"].performed += LockBothSticks;

            // powerUpInput.actions["Help (Multiplier)"].performed += IncreaseMultiplier;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = (float)health/maxHealth;
        //[looking if our receiver reached the end song trigger]
        ReceiverController.wonGame += wonTheGame;//<---- Observer Pattern (subscribing) 

        LeftStickUp = GameObject.Find(LeftUp).GetComponent<ReceiverController>();
        LeftStickDown = GameObject.Find(LeftDown).GetComponent<ReceiverController>();
        RightStickUp = GameObject.Find(RightUp).GetComponent<ReceiverController>();
        RightStickDown = GameObject.Find(RightDown).GetComponent<ReceiverController>();

    }

    private void OnDestroy()
    {
        ReceiverController.wonGame -= wonTheGame; //<---- Observer Pattern (unsubscribing)
    }

    // Update is called once per frame
    void wonTheGame(ReceiverController sub)
    {
        wonMyGame(this);
    }

    private void LockLeftStick(InputAction.CallbackContext context)
    {
        if (meterFill >= 0)
        {
            if(LeftStickUp != null && LeftStickDown != null)
            {
                MeterLost(meterGloomyDmg);
                wiggled = 0;
                LeftStickUp.Disable();
                LeftStickDown.Disable();
            }
        }
    }

    private void LockRightStick(InputAction.CallbackContext context)
    {
        if (meterFill >= 0)
        {
            if(RightStickUp != null && RightStickDown != null)
            {
                MeterLost(meterGloomyDmg);
                wiggled = 0;
                RightStickUp.Disable();
                RightStickDown.Disable();
            }
        }
    }

    private void LockBothSticks(InputAction.CallbackContext context)
    {
        if (meterFill >= 0)
        {
            if(LeftStickUp != null && LeftStickDown != null && RightStickUp != null && RightStickDown != null)
            {
                MeterLost(meterGloomyDmg);
                wiggled = 0;
                LeftStickUp.Disable();
                LeftStickDown.Disable();
                RightStickUp.Disable();
                RightStickDown.Disable();
            }
        }
    }

    public void DisableAllControl()
    {
        LeftStickUp.DisableFully();
        LeftStickDown.DisableFully();
        RightStickUp.DisableFully();
        RightStickDown.DisableFully();
    }

    public void superbHit()
    {
        Debug.Log("Superb Hit!");
        superb += 1;
        Heal(healthPerSuperb);
        MeterFill(meterPerSuperb);
        totScoreChange(scorePerSuperb * currentMultiplier); //<---Observer Pattern (Notifying)
        score += scorePerSuperb * currentMultiplier;
        scoreAmount.text = score.ToString();
    }

    public void goodHit()
    {
        Debug.Log("Good Hit!");
        good += 1;
        Heal(healthPerGood);
        MeterFill(meterPerGood);
        totScoreChange(scorePerGood * currentMultiplier); //<---Observer Pattern (Notifying)
        score += scorePerGood * currentMultiplier;
        scoreAmount.text = score.ToString();
    }

    public void badHit()
    {
        Debug.Log("Bad Hit!");
        bad += 1;
        Heal(healthPerBad);
        MeterFill(meterPerBad);
        totScoreChange(scorePerBad * currentMultiplier); //<---Observer Pattern (Notifying)
        score += scorePerBad * currentMultiplier;
        scoreAmount.text = score.ToString();
    }

    public void gloomyHit()
    {
        Debug.Log("Gloomy Hit...");
        gloomy += 1;
        Damage(healthGloomyDmg);
        MeterLost(meterGloomyDmg);
        multiplierTracker = 0;
        currentMultiplier = multiplierThresholds[multiplierTracker];
        //score += 0 * currentMultiplier;
        
    }

    public void IncreaseMultiplier()
    {
        StartCoroutine(StartMultiTimeLimit());
        multiplierTracker += 1;
        if (multiplierTracker < multiplierThresholds.Length)
        {
            currentMultiplier = multiplierThresholds[multiplierTracker];
            
        }
    }

    public IEnumerator StartMultiTimeLimit()
    {
        if (multiStarted) //Checks if Coroutine already started.
        {
            yield break;
        }
        multiStarted = true;
        float normalizedTime = 1.0f;
        while(normalizedTime >= 0f)
        {
            //multiplierTimerBar.fillAmount = normalizedTime;
            normalizedTime -= Time.deltaTime/multiplierTimeLimit;
            yield return null;
        }
        multiStarted = false;
        multiplierTracker = 0;
        currentMultiplier = multiplierThresholds[multiplierTracker];
    }

    public void MeterFill(float meterFillAmt)
    {
        meterFill += meterFillAmt;
        if (meterFill > maxMeter)
        {
            meterFill = maxMeter;
        }
    }

    public void MeterLost(float meterLostAmt)
    {
        meterFill += meterLostAmt;
        if (meterFill < 0.0f)
        {
            meterFill = 0.0f;
        }
    }

    public void Heal(int healAmt)
    {
        health += healAmt;
        healthBar.fillAmount = (float)health/maxHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Damage(int dmgAmt)
    {
        health -= dmgAmt;
        healthBar.fillAmount = (float)health/maxHealth;
        if (health <= 0)
        {
            health = 0;

            if(editorMode == false)
            {
                DisableAllControl();
                if (!isDead)
                {
                    imDead(this);  //<---Observer Pattern (Notifying)
                    isDead = true;
                }
                //anim.SetBool("IsDead", true); <-- use observer method? Or link to animators?
            }
        }
    }
}

