using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject multiplayerUI, singlePlayerUI; 
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI completionTitleText, songTitleText;

    [SerializeField] private Animator singlePlayerHealthBar, singlePlayerScoreBoard, singlePlayerScoreText, p1HealthBar, p2HealthBar;
    [SerializeField] private Animator p1ScoreBoard, p2ScoreBoard, p1ScoreText, p2ScoreText, TotalScoreText, completionTitle, songTitle;

    [SerializeField] private NumberCounter singlePlayerScore, singlePlayerHealth, p1Score, p2Score, p1Health, p2Health, totalScore;
    [SerializeField] private NumberCounter spSuperb, spGood, spBad, spGloomy, p1Superb, p1Good, p1Bad, p1Gloomy, p2Superb, p2Good, p2Bad, p2Gloomy;

    private bool isSinglePlayer = (GameManager.gameState == GameManager.GameStates.SinglePlayer);

    public float fadeTime = 1.0f;

    private float countTime;

    private string SelfSupportiveSymphony = "Self Supportive Symphony";
    private string HealthyHarmony = "Healthy Harmony";
    private string PhysicalBreakDown = "PHYSICAL BREAKDOWN";
    private string MentalBreakDown = "MENTAL BREAKDOWN";
    private string CompleteCacophany = "...COMPLETE CACOPHANY...";

    void Awake()
    {
        countTime = singlePlayerScore.Duration;

        singlePlayerHealth.enabled = false;
        singlePlayerScoreBoard.enabled = false;
        singlePlayerScoreText.enabled = false;
        p1HealthBar.enabled = false;
        p1ScoreBoard.enabled = false;
        p1ScoreText.enabled = false;
        p2HealthBar.enabled = false;
        p2ScoreBoard.enabled = false;
        p2ScoreText.enabled = false;
        TotalScoreText.enabled = false;
        songTitle.enabled = false;
        completionTitle.enabled = false;

        if (isSinglePlayer)
        {
            multiplayerUI.SetActive(false);
            singlePlayerUI.SetActive(true);
        }
        else
        {
            multiplayerUI.SetActive(true);
            singlePlayerUI.SetActive(false);

        }
    }

    void Start()
    {
        continueButton.gameObject.SetActive(true);
        continueButton.Select();

        StartCoroutine(ResultScreenSequence()); 
    }

    private IEnumerator ResultScreenSequence()
    {
        yield return StartCoroutine(SongTitle());
        yield return StartCoroutine(HealthBars());
        yield return StartCoroutine(Score());
        yield return StartCoroutine(ScoreBoard());
        yield return StartCoroutine(TotalScore());
        yield return StartCoroutine(CompletionTitle());
    }

    private IEnumerator Animation(Animator playingAnimation)
    {
        playingAnimation.enabled = true;
        playingAnimation.SetTrigger("Start");

        yield return new WaitForSeconds(fadeTime);
    }

    private IEnumerator SongTitle()
    {
        songTitleText.text = "| " + PlayerPrefs.GetString("SongTitle") + " |";
        StartCoroutine(Animation(songTitle));
        yield return new WaitForSeconds(countTime);
    }

    private IEnumerator HealthBars()
    {
        if (isSinglePlayer)
        {
            yield return StartCoroutine(Animation(singlePlayerHealthBar));
            singlePlayerHealth.Value = PlayerPrefs.GetInt("HealthPlayer1");
            yield return new WaitForSeconds(countTime);
        }

        else
        {
            StartCoroutine(Animation(p1HealthBar));
            yield return StartCoroutine(Animation(p2HealthBar));

            p1Health.Value = PlayerPrefs.GetInt("HealthPlayer1");
            p2Health.Value = PlayerPrefs.GetInt("HealthPlayer2");
            yield return new WaitForSeconds(countTime);
        }

        
    }

    private IEnumerator Score()
    {
        if (isSinglePlayer)
        {
            yield return StartCoroutine(Animation(singlePlayerScoreText));

            singlePlayerScore.Value = PlayerPrefs.GetInt("ScorePlayer1");
            yield return new WaitForSeconds(countTime);
        }

        else
        {
            StartCoroutine(Animation(p1ScoreText));
            yield return StartCoroutine(Animation(p2ScoreText));

            p1Score.Value = PlayerPrefs.GetInt("ScorePlayer1");
            p2Score.Value = PlayerPrefs.GetInt("ScorePlayer2");
            yield return new WaitForSeconds(countTime);
        }

        
    }

    private IEnumerator ScoreBoard()
    {
        if (isSinglePlayer)
        {
            yield return StartCoroutine(Animation(singlePlayerScoreBoard));

            

            spGloomy.Value = PlayerPrefs.GetInt("GloomyHitPlayer1");
            yield return new WaitForSeconds(countTime);
            spBad.Value = PlayerPrefs.GetInt("BadHitPlayer1");
            yield return new WaitForSeconds(countTime);
            spGood.Value = PlayerPrefs.GetInt("GoodHitPlayer1");
            yield return new WaitForSeconds(countTime);
            spSuperb.Value = PlayerPrefs.GetInt("SuperbHitPlayer1");
            yield return new WaitForSeconds(countTime);
        }

        else
        {
            StartCoroutine(Animation(p1ScoreBoard));
            yield return StartCoroutine(Animation(p2ScoreBoard));

            

            p1Gloomy.Value = PlayerPrefs.GetInt("GloomyHitPlayer1");
            yield return new WaitForSeconds(countTime);
            p1Bad.Value = PlayerPrefs.GetInt("BadHitPlayer1");
            yield return new WaitForSeconds(countTime);
            p1Good.Value = PlayerPrefs.GetInt("GoodHitPlayer1");
            yield return new WaitForSeconds(countTime);
            p1Superb.Value = PlayerPrefs.GetInt("SuperbHitPlayer1");
            yield return new WaitForSeconds(countTime);

            p2Gloomy.Value = PlayerPrefs.GetInt("GloomyHitPlayer2");
            yield return new WaitForSeconds(countTime);
            p2Bad.Value = PlayerPrefs.GetInt("BadHitPlayer2");
            yield return new WaitForSeconds(countTime);
            p2Good.Value = PlayerPrefs.GetInt("GoodHitPlayer2");
            yield return new WaitForSeconds(countTime);
            p2Superb.Value = PlayerPrefs.GetInt("SuperbHitPlayer2");
            yield return new WaitForSeconds(countTime);
        }

        
    }

    private IEnumerator TotalScore()
    {
        if(isSinglePlayer)
        {
            yield break;
        }
        else
        {
            yield return StartCoroutine(Animation(TotalScoreText));

            totalScore.Value = PlayerPrefs.GetInt("TotalScore");
            yield return new WaitForSeconds(countTime);
        }

        
    }

    private IEnumerator CompletionTitle()
    {
        
        if(isSinglePlayer)
        {
            if(singlePlayerHealth.Value > 0)
            {
                if(spGloomy.Value == 0 && spBad.Value == 0)
                {
                    completionTitleText.text = SelfSupportiveSymphony;
                }
                else
                {
                    completionTitleText.text = HealthyHarmony;
                }
            }
            else
            {
                completionTitleText.text = CompleteCacophany;
            }
                


        }
        else
        {
            if(p1Health.Value > 0 && p2Health.Value > 0)
            {
                if(p1Gloomy.Value == 0 && p1Bad.Value == 0 && p2Gloomy.Value == 0 && p2Bad.Value == 0)
                {
                    completionTitleText.text = SelfSupportiveSymphony;
                }
                else
                {
                    completionTitleText.text = HealthyHarmony;
                }
            }
            else if (p1Health.Value > 0 && p2Health.Value == 0)
            {
                completionTitleText.text = MentalBreakDown;
            }
            else if (p1Health.Value == 0 && p2Health.Value > 0)
            {
                completionTitleText.text = PhysicalBreakDown;
            }
            else
            {
                completionTitleText.text = CompleteCacophany;
            }
        }

        yield return StartCoroutine(Animation(completionTitle));
        
    }


}
