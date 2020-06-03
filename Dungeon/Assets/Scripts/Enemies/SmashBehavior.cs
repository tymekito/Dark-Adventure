using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implmentacja zachowania Smash w maszynie stanów animatora
/// W momęcie startu tworzymy odniesenie do obiektu bossa 
/// Gdy czas animacji dojdzie do 0 maszyna przechodzi w losowp wybrany za pomocą numeru stan poprzez ustawienie odpowiednich flag w animatorze
/// </summary>
public class SmashBehavior : StateMachineBehaviour
{
    private int rand;
    bool check;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().SmashAbility();
        check = false;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("smash") == false)
        {
            if (!check)
            {
                check = true;
                animator.SetTrigger("idle");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

   
}
