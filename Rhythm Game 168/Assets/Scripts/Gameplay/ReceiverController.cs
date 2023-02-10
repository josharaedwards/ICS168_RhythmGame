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
    private Vector3 boxColliderOffset, receiverColliderPos;
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
    // private bool /*superbPress, goodPress, almostPress,badPress,*/ validPress = false;
    private Note currentNote; // TODO: Make this a queue for somewhat overlapping notes

    [SerializeField] private GameObject superbHit, goodHit, badHit, gloomyHit;

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
        playerInput.actions[m_Keybind.action.name].performed += Pressed;
        playerInput.actions[m_Keybind.action.name].canceled += notPressed;
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
        boxColliderOffset = boxCollider.offset;
        receiverColliderSize = boxCollider.size.y;
        receiverColliderPosX = boxColliderOffset.x + transform.position.x;
        receiverColliderPosY = boxColliderOffset.y + transform.position.y;
        receiverColliderPos = boxColliderOffset + transform.position;


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

    // Update is called once per frame
    void Pressed(InputAction.CallbackContext context)
    {
        if (!disabled)
        {
            if(buttonSprite != null && lineSprite != null && laneSprite != null)
            {
                laneSprite.color = pressedLaneColor;
                lineSprite.color = pressedLineColor;
                buttonSprite.color = pressedButtonColor;
            }
            Hit();
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
    }

    private void Wiggle()
    {
        AudioManager.instance.PlayBeat(sfxHitDisabled);
        scoreAndHealth.wiggled += 1;
        
    }

    // Send hit signal to current note
    private void Hit()
    {
        
        if (currentNote != null && currentNote.gameObject.activeSelf)
        {
            currentNote.Hit(this);
        }
        else
        {
            AudioManager.instance.PlayBeat(sfxMiss);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            currentNote = collision.gameObject.GetComponent<Note>();
            //Debug.Log("Note Enter");
        }
        else if (collision.tag == "Finish")
        {
            Debug.Log("Won Game!");
            wonGame(this);          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public void GloomyHit()
    {
        scoreAndHealth.gloomyHit();
        Instantiate(gloomyHit, new Vector3(buttonSprite.transform.position.x, buttonSprite.transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void BadHit()
    {
        scoreAndHealth.badHit();
        Instantiate(badHit, new Vector3(buttonSprite.transform.position.x, buttonSprite.transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void GoodHit()
    {
        scoreAndHealth.goodHit();
        Instantiate(goodHit, new Vector3(buttonSprite.transform.position.x, buttonSprite.transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void SuperbHit()
    {
        scoreAndHealth.superbHit();
        Instantiate(superbHit, new Vector3(buttonSprite.transform.position.x, buttonSprite.transform.position.y, transform.position.z), Quaternion.identity);
    }

    public float HitRangePercentage(Vector3 currentNotePos)
    {
        return (Mathf.Abs(Vector3.Distance(currentNotePos, receiverColliderPos))) / receiverColliderSize * 2;
    }
}