using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private AudioClip sfxHit;
    [SerializeField] private AudioClip sfxMiss;
    [SerializeField][Range(0f,1f)] private float gloomyThreshold = 0.90f;
    [SerializeField] [Range(0f, 1f)] private float badThreshold = 0.75f;
    [SerializeField] [Range(0f, 1f)] private float goodThreshold = 0.50f;
    //[SerializeField] [Range(0f, 1f)] private float superbThreshold = 0;

    ReceiverController receiver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Hit(ReceiverController receiver)
    {
        AudioManager.instance.PlayBeat(sfxHit);
        float currentNotePos = transform.position.y;
        float hitRangePercentage = receiver.HitRangePercentage(currentNotePos);

        if (hitRangePercentage > gloomyThreshold)
        {
            receiver.GloomyHit();
        }
        else if (hitRangePercentage > badThreshold)
        {
            receiver.BadHit();
        }
        else if (hitRangePercentage > goodThreshold)
        {
            receiver.GoodHit();
        }
        else
        {
            receiver.SuperbHit();
        }

        gameObject.SetActive(false);

    }

    public void Miss(ReceiverController receiver)
    {
        receiver.GloomyHit();
        StartCoroutine(Despawn());
    }

    // Depawn the note after a delay
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            receiver = collision.gameObject.GetComponent<ReceiverController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (gameObject.activeSelf)
            {
                Miss(receiver);
            }
        }
    }
}
