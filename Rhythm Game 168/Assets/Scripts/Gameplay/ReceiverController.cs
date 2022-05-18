using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiverController : MonoBehaviour
{
    private SpriteRenderer buttonSprite, lineSprite, laneSprite;
    private Color initButtonColor, initLineColor, initLaneColor;
    private Color pressedButtonColor, pressedLineColor, pressedLaneColor;

    private float receiverColliderSize;
    private float receiverColliderPosY, receiverColliderPosX;
    private PlayerInput playerInput;

    [SerializeField] [Range(0f, 2f)] private float shadeAlpha;
    [SerializeField] private InputActionReference m_Keybind;

    [SerializeField] private AudioClip sfxHit;

    [SerializeField] private AudioClip sfxMiss;
    [SerializeField] private AudioClip sfxHitDisabled;
    private ScoreTracker scoreTracker;

    private int wiggleToFree = 10;
    private bool disabled = false;
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
        laneSprite.color = pressedLaneColor;
        lineSprite.color = pressedLineColor;
        buttonSprite.color = pressedButtonColor;
        disabled = true;
    }

    void Awake() {
        playerInput = GetComponentInParent<PlayerInput>();;
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
                laneSprite.color = pressedLaneColor;
                lineSprite.color = pressedLineColor;
                buttonSprite.color = pressedButtonColor;
            }
        } 
    }
    

    void Start()
    {
        receiverColliderSize = GetComponent<BoxCollider2D>().size.y;
        receiverColliderPosX = GetComponent<BoxCollider2D>().offset.x + transform.position.x;
        receiverColliderPosY = GetComponent<BoxCollider2D>().offset.y + transform.position.y;


        laneSprite = GetComponentInChildren<SpriteRenderer>();
        initLaneColor = laneSprite.color;
        pressedLaneColor = new Color(initLaneColor.r, initLaneColor.g, initLaneColor.b, initLaneColor.a * shadeAlpha);
        
        lineSprite = transform.parent.Find("Judgement Line").GetComponent<SpriteRenderer>();
        initLineColor = lineSprite.color;
        pressedLineColor = new Color(initLineColor.r, initLineColor.g, initLineColor.b, initLineColor.a * shadeAlpha);

        buttonSprite = GetComponent<SpriteRenderer>();
        initButtonColor = buttonSprite.color;
        pressedButtonColor = new Color(initButtonColor.r, initButtonColor.g, initButtonColor.b, initButtonColor.a * shadeAlpha);

        scoreTracker = ScoreTracker.instance;
    }
    
    void Update()
    {
        
        if (scoreTracker.wiggled >= wiggleToFree)
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
        AudioManager.instance.PlaySFX(sfxHitDisabled);
        scoreTracker.wiggled += 1;
        
    }

    private void HitType()
    {
        
        if (validPress)
        {
            AudioManager.instance.PlaySFX(sfxHit);
            float currentNotePos = currentNote.transform.position.y;
            float hitRangePercentage = (Mathf.Abs(currentNotePos - (receiverColliderPosY))/receiverColliderSize) * 2;

            if (hitRangePercentage > .90f)
            {
                scoreTracker.gloomy += 1;
                Instantiate(gloomyHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }
            else if (hitRangePercentage > .75f)
            {
                scoreTracker.bad += 1;
                Instantiate(badHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }
            else if (hitRangePercentage > .50f)
            {
                scoreTracker.good += 1;
                Instantiate(goodHit, new Vector3(receiverColliderPosX, receiverColliderPosY, transform.position.z), Quaternion.identity);
            }
            else
            {
                scoreTracker.superb += 1;
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
            AudioManager.instance.PlaySFX(sfxMiss);
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
                scoreTracker.gloomy += 1;
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