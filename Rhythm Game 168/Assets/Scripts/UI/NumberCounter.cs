using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberCounter : MonoBehaviour
{
    public TextMeshProUGUI ScoreText = null; 
    public TextMeshProUGUI TotalScoreText = null;
    public Image HealthBar = null;
    private const int maxHealth = 100;
    public int CountFPS = 30;
    public float Duration = 2f;
    public string NumberFormat = "N0";
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
    }
    private Coroutine CountingCoroutine;

    private void Awake()
    {
        // ScoreText = GetComponent<TextMeshProUGUI>();
        if(HealthBar != null)
        {
            _value = maxHealth;
        }
    }

    private void UpdateText(int newValue)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }

        CountingCoroutine = StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
        }

        if (previousValue < newValue)
        {
            while(previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                if(TotalScoreText != null)
                {
                    TotalScoreText.SetText(previousValue.ToString(NumberFormat));
                }

                if(HealthBar != null)
                {
                    HealthBar.fillAmount = (float)previousValue/maxHealth;
                }

                if(ScoreText != null)
                {
                    ScoreText.SetText("Score: " + previousValue.ToString(NumberFormat));
                }

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                if(TotalScoreText != null)
                {
                    TotalScoreText.SetText(previousValue.ToString(NumberFormat));
                }

                if(ScoreText != null)
                {
                    ScoreText.SetText("Score: " + previousValue.ToString(NumberFormat));
                }

                if(HealthBar != null)
                {
                    HealthBar.fillAmount = (float)previousValue/maxHealth;
                }

                yield return Wait;
            }
        }
    }
}
