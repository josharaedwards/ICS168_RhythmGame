using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReceiverCotroller : MonoBehaviour
{
    private SpriteRenderer buttonSprite;
    private Color initColor;
    private Color pressedColor;

    [SerializeField][Range(0f,2f)] private float shadeAlpha;
    [SerializeField] private KeyCode keybind;
    [SerializeField] private InputAction playerControls;

    private ScoreTracker scoreTracker;
    private bool validPress = false;
    private GameObject currentNote; // TODO: Make this a queue for somewhat overlapping notes

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
        initColor = buttonSprite.color;
        pressedColor = new Color(initColor.r * shadeAlpha, initColor.g * shadeAlpha, initColor.b * shadeAlpha, initColor.a);

        scoreTracker = ScoreTracker.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keybind))
        {
            buttonSprite.color = pressedColor;
        }
        else
        {
            buttonSprite.color = initColor;
        }

        if (Input.GetKeyDown(keybind))
        {
            if (validPress)
            {
                scoreTracker.score += 1;
                currentNote.SetActive(false);
                currentNote = null;
            }
            else
            {
                scoreTracker.miss += 1;
            }
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
