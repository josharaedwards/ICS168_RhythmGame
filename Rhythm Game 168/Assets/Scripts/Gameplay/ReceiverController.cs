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
    private float receiverColliderOffset;
    private PlayerInput playerInput;

    [SerializeField] [Range(0f, 2f)] private float shadeAlpha;
    [SerializeField] private InputActionReference m_Keybind;

    private ScoreTracker scoreTracker;
    private bool /*superbPress, goodPress, almostPress,badPress,*/ validPress = false;
    private GameObject currentNote; // TODO: Make this a queue for somewhat overlapping notes

    private void OnEnable()
    {
        playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        playerInput.actions.Disable();
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
            laneSprite.color = initLaneColor;
            lineSprite.color = initLineColor;
            buttonSprite.color = initButtonColor;
        } 
    }
    

    void Start()
    {
        receiverColliderSize = GetComponent<BoxCollider2D>().size.y;
        receiverColliderOffset = GetComponent<BoxCollider2D>().offset.y;

        laneSprite = GetComponentInChildren<SpriteRenderer>();
        initLaneColor = laneSprite.color;
        pressedLaneColor = new Color(initLaneColor.r, initLaneColor.g, initLaneColor.b, initLaneColor.a * shadeAlpha);
        
        lineSprite = GameObject.Find("Judgement Line").GetComponent<SpriteRenderer>();
        initLineColor = lineSprite.color;
        pressedLineColor = new Color(initLineColor.r, initLineColor.g, initLineColor.b, initLineColor.a * shadeAlpha);

        buttonSprite = GetComponent<SpriteRenderer>();
        initButtonColor = buttonSprite.color;
        pressedButtonColor = new Color(initButtonColor.r, initButtonColor.g, initButtonColor.b, initButtonColor.a * shadeAlpha);

        scoreTracker = ScoreTracker.instance;
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
        if(buttonSprite != null && lineSprite != null && laneSprite != null)
        {
            laneSprite.color = pressedLaneColor;
            lineSprite.color = pressedLineColor;
            buttonSprite.color = pressedButtonColor;
        }

        HitType();
            
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

    private void HitType()
    {
        if (validPress)
        {
            float currentNotePos = currentNote.transform.position.y;
            float hitRangePercentage = (Mathf.Abs(currentNotePos - (receiverColliderOffset + transform.position.y))/receiverColliderSize) * 2;

            if (hitRangePercentage > .90f)
            {
                scoreTracker.gloomy += 1;
            }
            else if (hitRangePercentage > .75f)
            {
                scoreTracker.bad += 1;
            }
            else if (hitRangePercentage > .50f)
            {
                scoreTracker.good += 1;
            }
            else
            {
                scoreTracker.superb += 1;
            }

            if(currentNote != null)
            {
                currentNote.SetActive(false);
                currentNote = null;
            }
            validPress = false;
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
                scoreTracker.miss += 1;
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
