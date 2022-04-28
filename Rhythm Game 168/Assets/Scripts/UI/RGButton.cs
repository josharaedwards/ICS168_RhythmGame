using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RGButton : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSFX;

    protected Button self;
    private AudioManager audioManager; 

    protected void Setup()
    {
        self = GetComponent<Button>();
        self.onClick.AddListener(() => OnRGButtonClick());
        audioManager = AudioManager.instance;
    }

    protected virtual void OnRGButtonClick()
    {
        audioManager.PlayUI(buttonSFX);
    }
}
