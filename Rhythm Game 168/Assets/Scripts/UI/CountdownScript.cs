using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownScript : MonoBehaviour
{
    public static event Action<CountdownScript> countdownEnded;

    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private Animation countdownAnim;

    [SerializeField] private int countdown = 3;

    [SerializeField] private float countdownCount = 1.0f;

    void Start()
    {
        PlayerManager.allPlayersReady += PlayersNowReady; //<---- Observer Pattern (subscribing)
    }

    void OnDestroy()
    {
        PlayerManager.allPlayersReady -= PlayersNowReady; //<---- Observer Pattern (unsubscribing)
    }

    private void PlayersNowReady(PlayerManager sub)
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        
        for (int i = countdown; i > 0 ; i--)
        {
            countdownText.text = i.ToString();
            countdownAnim.Play("Countdown");
            yield return new WaitForSeconds(countdownCount);
            
        }

        countdownText.text = "GO!";
        countdownAnim.Play("Countdown");
        yield return new WaitForSeconds(countdownCount);
        countdownAnim.gameObject.SetActive(false);
        countdownEnded(this);
    }
}
