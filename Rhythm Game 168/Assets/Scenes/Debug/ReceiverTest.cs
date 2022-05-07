using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiverTest : MonoBehaviour
{
    private SpriteRenderer buttonSprite;
    private Color initColor;
    private Color pressedColor;

    [SerializeField][Range(0f,2f)] private float shadeAlpha;
    [SerializeField] private InputActionReference m_Keybind = null;


    private ScoreTracker scoreTracker;
    private bool validPress = false;
    private GameObject currentNote; // TODO: Make this a queue for somewhat overlapping notes

    private void OnEnable()
    {
        m_Keybind.action.Enable();
    }

    private void OnDisable()
    {
        m_Keybind.action.Disable();
    }

    void Awake()
    {
        m_Keybind.action.performed += ctx => HitOrMiss();
        m_Keybind.action.performed += ctx => buttonSprite.color = pressedColor;
        m_Keybind.action.canceled += ctx => buttonSprite.color = initColor;
    }

    void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
        initColor = buttonSprite.color;
        pressedColor = new Color(initColor.r * shadeAlpha, initColor.g * shadeAlpha, initColor.b * shadeAlpha, initColor.a);

        scoreTracker = ScoreTracker.instance;
    }

    // Update is called once per frame
    void HitOrMiss()
    {
        if (validPress)
        {
            scoreTracker.superb += 1;
            currentNote.SetActive(false);
            currentNote = null;
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
        if(collision.gameObject.activeSelf) //Checks if the GameObject is active, preventing the function from registering it being hit as a miss.
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
