﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implmentacja zachowania Shoot w maszynie stanów animatora
/// W momęcie startu tworzymy odniesenie do obiektu bossa 
/// Gdy czas animacji dojdzie do 0 maszyna przechodzi w losowp wybrany za pomocą numeru stan poprzez ustawienie odpowiednich flag w animatorze
/// </summary>
public class ShootBehavior : StateMachineBehaviour
{
    //every shoot need 0.5 timer;
    [SerializeField] private float timer;//time of animation
    [SerializeField] public int numberOfShoots;
    private GameObject boss;
    private bool check=false;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        check = false;
        boss = GameObject.FindGameObjectWithTag("Boss");
        boss.GetComponent<Boss>().StartShootingAbility(numberOfShoots);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (!check)
            {
                check = true;
                boss.GetComponent<Boss>().Idle();
                animator.SetTrigger("idle");
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
