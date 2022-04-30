using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiverController : MonoBehaviour
{
    private SpriteRenderer buttonSprite;
    private Color initColor;
    private Color pressedColor;

    private PlayerInput playerInput;

    [SerializeField] [Range(0f, 2f)] private float shadeAlpha;
    [SerializeField] private InputActionReference m_Keybind;

    private ScoreTracker scoreTracker;
    private bool validPress = false;
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
        playerInput.actions[m_Keybind.action.name].performed += HitOrMiss;
        playerInput.actions[m_Keybind.action.name].performed += notPressed;
        // m_Keybind.action.performed += ctx => HitOrMiss();
        // m_Keybind.action.performed += ctx => buttonSprite.color = pressedColor;
        // m_Keybind.action.canceled += ctx => buttonSprite.color = initColor;
    }

    void notPressed(InputAction.CallbackContext context)
    {
        if(buttonSprite != null)
        {
            buttonSprite.color = initColor;
        }    
    }
    

    void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
        initColor = buttonSprite.color;
        pressedColor = new Color(initColor.r * shadeAlpha, initColor.g * shadeAlpha, initColor.b * shadeAlpha, initColor.a);

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
    void HitOrMiss(InputAction.CallbackContext context)
    {
        if(buttonSprite != null)
        {
            buttonSprite.color = pressedColor;
        }
            
        if (validPress)
        {
            scoreTracker.score += 1;

            if(currentNote != null)
            {
                currentNote.SetActive(false);
                currentNote = null;
            }
            validPress = false;
        }
        else
        {
            scoreTracker.miss += 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            validPress = true;
            currentNote = collision.gameObject;
            Debug.Log("Note Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.activeSelf) //Checks if the GameObject is active, preventing the function from registering it being hit as a miss.
        {
            if (collision.tag == "Note")
            {
                validPress = false;
                if (currentNote != null)
                {
                    scoreTracker.miss += 1;
                }
                currentNote = null;
                Debug.Log("Note Exit");
            }
        }
    }
}
