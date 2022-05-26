using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        PlayerStats.imDead += Died;
        //PlayerStats.iPoweredUp += PowerUp;
        PlayerStats.wonMyGame += wonGame;
        PlayerStats.highHealth += Healthy;
        PlayerStats.lowHealth += Unhealthy;
        PlayerStats.iGotHurt += Hurt;
    }

    void OnDestroy()
    {
        PlayerStats.imDead -= Died;
        //PlayerStats.iPoweredUp -= PowerUp;
        PlayerStats.highHealth -= Healthy;
        PlayerStats.lowHealth -= Unhealthy;
        PlayerStats.iGotHurt -= Hurt;
    }

    private bool CheckIfMine(PlayerStats sub)
    {
        if(this.transform.parent == sub.gameObject.transform)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Died(PlayerStats sub)
    {
        if(CheckIfMine(sub))
        {
            anim.SetBool("Lose", true);
            anim.SetBool("Normal",  false);
            anim.SetBool("Scared",  false);
        }
    }

    void PowerUp(PlayerStats sub)
    {
        if(CheckIfMine(sub))
        {
            anim.SetTrigger("Happy");
        }
    }

    void wonGame(PlayerStats sub)
    {
        if(CheckIfMine(sub))
        {
            anim.SetTrigger("Happy");
        }
    }

    void Hurt(PlayerStats sub)
    {
        if(CheckIfMine(sub))
        {
            anim.SetTrigger("Miss");
        }
    }

    void Healthy(PlayerStats sub)
    {
        if(CheckIfMine(sub))
        {
            anim.SetBool("Normal",  true);
            anim.SetBool("Scared",  false);
            anim.SetBool("Lose", false);
        }
    }

    void Unhealthy(PlayerStats sub)
    {
        if(CheckIfMine(sub))
        {
            anim.SetBool("Scared",  true);
            anim.SetBool("Normal",  false);
            anim.SetBool("Lose", false);
        }
    }

    



    
}
