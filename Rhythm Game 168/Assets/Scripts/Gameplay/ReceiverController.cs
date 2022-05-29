using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReceiverController : MonoBehaviour
{
    public static event Action<ReceiverController> wonGame; //<--- Observer pattern
    private SpriteRenderer buttonSprite, laneSprite;

    [SerializeField] private SpriteRenderer lineSprite;

    private Color initButtonColor, initLineColor, initLaneColor;
    private Color pressedButtonColor, pressedLineColor, pressedLaneColor;

    private Color disabledButtonColor, disabledLineColor, disabledLaneColor;

    private BoxCollider2D boxCollider;
    private float receiverColliderSize;
    private float receiverColliderPosY, receiverColliderPosX;
    private PlayerInput playerInput;

    [SerializeField] [Range(0f, 1f)] private float shadeWhite, shadeAlpha;
    [SerializeField] private InputActionReference m_Keybind;

    [SerializeField] private AudioClip sfxHit;

    [SerializeField] private AudioClip sfxMiss;
    [SerializeField] private AudioClip sfxHitDisabled;
    private PlayerStats scoreAndHealth;

    private int wiggleToFree = 10;
    private bool disabled = false;
    // private bool isDead = false;
    private bool /*superbPress, goodPress, almostPress,badPress,*/ validPress = false;
    private GameObject currentNote; // TODO: Make this a queue for somewhat overlapping notes

    [SerializeField] private GameObject superbHit, goodHit, badHit, gloomyHit, miss;

    private void OnEnable()
    {
        playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        if(playerInput != null)
        {
            playerInput.actions.Disable();
        }
    }

    public void Disable()
    {
        laneSprite.color = disabledLaneColor;
        lineSprite.color = disabledLineColor;
        buttonSprite.color = disabledButtonColor;
        disabled = true;
    }

    public void DisableFully()
    {
        playerInput.actions.Disable();
        laneSprite.color = disabledLaneColor;
        lineSprite.color = disabledLineColor;
        buttonSprite.color = disabledButtonColor;
        boxCollider.enabled = false;
    }

    void Awake() {
        playerInput = GetComponentInParent<PlayerInput>();
        playerInput.actions[m_Keybind.action.name].performed += Hit;
        playerInput.actions[m_Keybind.action.name].canceled += notPressed;
        
        // m_Keybind.action.performed += ctx => HitOrMiss();
        // m_Keybind.action.performed += ctx => buttonSprite.color = pressedColor;
        // m_Keybind.action.canceled += ctx => buttonSprite.color = initColor;
    }

    void notPressed(InputAction.CallbackContext context)
    {
        if(buttonSprite != null && lineSprite != null && laneSprite != null)
        {
            if (!disabled)
            {
                laneSprite.color = initLaneColor;
                lineSprite.color = initLineColor;
                buttonSprite.color = initButtonColor;
            }
            else
            {
                laneSprite.color = disabledLaneColor;
                lineSprite.color = disabledLineColor;
                buttonSprite.color = disabledButtonColor;
            }
        } 
    }
    

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        receiverColliderSize = boxCollider.size.y;
        receiverColliderPosX = boxCollider.offset.x + transform.position.x;
        receiverColliderPosY = boxCollider.offset.y + transform.position.y;


        laneSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        initLaneColor = laneSprite.color;
        pressedLaneColor = Color.Lerp(initLaneColor, Color.white, shadeWhite);
        disabledLaneColor = new Color(initLaneColor.r, initLaneColor.g, initLaneColor.b, initLaneColor.a * shadeAlpha);
        
        initLineColor = lineSprite.color;
        pressedLineColor = Color.Lerp(initLineColor, Color.white, shadeWhite);
        disabledLineColor = new Color(initLineColor.r, initLineColor.g, initLineColor.b, initLineColor.a * shadeAlpha);

        buttonSprite = GetComponent<SpriteRenderer>();
        initButtonColor = buttonSprite.color;
        pressedButtonColor = Color.Lerp(initButtonColor, Color.white, shadeWhite);
        disabledButtonColor = new Color(initButtonColor.r, initButtonColor.g, initButtonColor.b, initButtonColor.a * shadeAlpha);

        scoreAndHealth = GetComponentInParent<PlayerStats>();
    }
    
    void Update()
    {
        
        if (scoreAndHealth.wiggled >= wiggleToFree)
        {
            disabled = false;
            laneSprite.color = initLaneColor;
            lineSprite.color = initLineColor;
            buttonSprite.color = initButtonColor;
        }
        
    }

    /*
    void Update() {
        if (Input.GetKeyDown(PRESS)) {
            HitOrMiss();
            buttonSprite.color = pressedColor;
        }
        if (Input.GetKeyUp(PRESS)) {
            buttonSprite.color = initColor;
        }
    }
    */
    // Update is called once per frame
    void Hit(InputAction.CallbackContext context)
    {
        if (!disabled)
        {
            if(buttonSprite != null && lineSprite != null && laneSprite != null)
            {
                laneSprite.color = pressedLaneColor;
                lineSprite.color = pressedLineColor;
                buttonSprite.color = pressedButtonColor;
            }
            HitType();
        }
        else
        {
            if(buttonSprite != null && lineSprite != null && laneSprite != null)
            {
                laneSprite.color = initLaneColor;
                lineSprite.color = initLineColor;
                buttonSprite.color = initButtonColor;
            }
            Wiggle();
        }    
        // if (validPress)
        // {
        //     scoreTracker.gloomy += 1;

        //     if(currentNote != null)
        //     {
        //         currentNote.SetActive(false);
        //         currentNote = null;
        //     }
        //     validPress = false;
        // }
        // else
        // {
        //     scoreTracker.miss += 1;
        // }

    }

    private void Wiggle()
    {
        AudioManager.instance.PlayBeat(sfxHitDisabled);
        scoreAndHealth.wiggled += 1;
        
    }

    private void HitType()
    {
        
        if (validPress)
        {
            AudioManager.instance.PlayBeat(sfxHit);
            float currentNotePos = currentNote.transform.position.y;
            float hitRangePercentage = (Mathf.Abs(currentNotePos - (receiverColliderPosY))/receiverColliderSize) * 2;

            if (hitRangePercentage > .90f)
            {
                scoreAndHealth.gloomyHit();
                Instantiate(gloomyHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }
            else if (hitRangePercentage > .75f)
            {
                scoreAndHealth.badHit();
                Instantiate(badHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }
            else if (hitRangePercentage > .50f)
            {
                scoreAndHealth.goodHit();
                Instantiate(goodHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }
            else
            {
                scoreAndHealth.superbHit();
                Instantiate(superbHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }

            if(currentNote != null)
            {
                currentNote.SetActive(false);
                currentNote = null;
            }
            validPress = false;
        }
        else
        {
            AudioManager.instance.PlayBeat(sfxMiss);
        }
        


        // if (superbPress)
        // {
        //     scoreTracker.superb += 1;

        //     superbPress = false;
        //     goodPress = false; 
        //     // almostPress = false; 
        //     badPress = false;
        //     validPress = false;
            // if(currentNote != null)
            // {
            //     currentNote.SetActive(false);
            //     currentNote = null;
            // }
            // (superbPress, goodPress, almostPress, badPress, validPress) = false;
            // superbPress = false;
//         }
//         else if (goodPress)
//         {
//             scoreTracker.good += 1;

//             superbPress = false;
//             goodPress = false; 
//             // almostPress = false; 
//             badPress = false;
//             validPress = false;
//             // if(currentNote != null)
//             // {
//             //     currentNote.SetActive(false);
//             //     currentNote = null;
//             // }
            
//             //superbPress = false;
//         }
// //         else if (almostPress)
// //         {
// //             scoreTracker.almost += 1;

// //             // if(currentNote != null)
// //             // {
// //             //     currentNote.SetActive(false);
// //             //     currentNote = null;
// //             // }
// // ;
// //             //validPress = false;
// //         }
//         else if (badPress)
//         {
//             scoreTracker.bad += 1;

//             superbPress = false;
//             goodPress = false; 
//             // almostPress = false; 
//             badPress = false;
//             validPress = false;

//             // if(currentNote != null)
//             // {
//             //     currentNote.SetActive(false);
//             //     currentNote = null;
//             // }

//             //validPress = false;
//         }
//         else if (validPress)
//         {
//             scoreTracker.gloomy += 1;

//             superbPress = false;
//             goodPress = false; 
//             // almostPress = false; 
//             badPress = false;
//             validPress = false;
//             //validPress = false;
//         }

//         if(currentNote != null)
//         {
//             currentNote.SetActive(false);
//             currentNote = null;
//         }

        // superbPress = false;
        // goodPress = false; 
        // // almostPress = false; 
        // badPress = false;
        // validPress = false;


        
        // else
        // {
        //     scoreTracker.miss += 1;
        // }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            validPress = true;
            if (currentNote == null)
            {
                currentNote = collision.gameObject;
            }
            Debug.Log("Note Enter");
        }
        else if (collision.tag == "Finish")
        {
            Debug.Log("Won Game!");
            wonGame(this);
            
        }
        // switch (collision.tag)
        // {
        //     case "Note":
        //         superbPress = true;
        //         break;
        //     case "Good Note":
        //         goodPress = true;
        //         break;
        //     // case "Almost Note":
        //     //     almostPress = true;
        //     //     break;
        //     case "Bad Note":
        //         badPress = true;
        //         break;
        //     case "Gloomy Note":
        //         validPress = true;
        //         currentNote = collision.transform.parent.gameObject;
        //         Debug.Log("Note Enter");
        //         break;

        // }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.activeSelf) //Checks if the GameObject is active, preventing the function from registering it being hit as a miss.
        {
            if (collision.tag == "Note")
            {
                validPress = false;
                // if (currentNote != null)
                // {
                scoreAndHealth.gloomyHit();
                Instantiate(gloomyHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
                // }
                currentNote = null;
                Debug.Log("Note Exit");
            }
        }
        // if (currentNote != null)
        // {
        //     if (currentNote.activeSelf) //Checks if the GameObject is active, preventing the function from registering it being hit as a miss.
        //     {
        //         switch (collision.tag)
        //         {
        //             case "Note":
        //                 superbPress = false;
        //                 break;
        //             case "Good Note":
        //                 goodPress = false;
        //                 break;
        //             // case "Almost Note":
        //             //     almostPress = false;
        //             //     break;
        //             case "Bad Note":
        //                 badPress = false;
        //                 break;
        //             case "Gloomy Note":
        //                 validPress = false;
        //                 scoreTracker.miss += 1;
        //                 // if (currentNote != null)
        //                 // {
        //                 //     scoreTracker.miss += 1;
        //                 // }
        //                 currentNote = null;
        //                 Debug.Log("Note Exit");
        //                 break;
        //         }
        //     }
        // }    
    }
}