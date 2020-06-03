using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implmentacja zachowania Idle w maszynie stanów animatora
/// W momęcie startu tworzymy odniesenie do obiektu bossa 
/// Gdy czas animacji dojdzie do 0 maszyna przechodzi w losowp wybrany za pomocą numeru stan poprzez ustawienie odpowiednich flag w animatorze
/// </summary>
public class IdleBehavior : StateMachineBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    private GameObject boss;
    private bool check;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        check = false;
        boss = GameObject.FindGameObjectWithTag("Boss");
        timer = Random.Range(minTime, maxTime);
        boss.GetComponent<Boss>().Idle();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (!check)
            {
                check = true;
                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    animator.SetBool("smash", true);
                }
                else if (rand == 1)
                {
                    animator.SetBool("smash", true);
                }
                else
                {
                    animator.SetTrigger("shoot");
                }
            }

        }
        else
        {
            timer -= Time.deltaTime;
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
