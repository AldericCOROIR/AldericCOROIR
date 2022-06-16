using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq.Expressions;
using CallBackEffect = System.Func<UnityEngine.GameObject, UnityEngine.GameObject, UnityEngine.GameObject, bool>;
public class Effect 
{

    //the instance of the method of this call to call
    public MethodInfo callBack;
    //timer used to know when animation is over
    public float timer;
    //bool to allow timer count
    private bool isTimerStarted;

    GameObject hand;

    
    //basic CTOR takes the name of the C# method to call
    public Effect(string callBackName)
    {
        callBack = findCallBackByName(callBackName);
        hand = GameObject.Find("Hand");
    }

    //find the assocaited method of this class based on string arg
    private MethodInfo findCallBackByName(string callBackName)
    {
        var methodArray = this.GetType().GetTypeInfo().DeclaredMethods;
        foreach (var method in methodArray)
        {
            if (method.Name == callBackName)
                return method;
        }
        return null;
         
    }
    //call the callback member method
    public bool Call(GameObject player, GameObject target, GameObject card)
    {
        object[] parameters = { player, target, card };
        return (bool)callBack.Invoke(this, parameters);
    }

    //allow for counting time
    public void StartTimer()
    {
            isTimerStarted = true;
    }
    //actually count time is allowed else nothing
    public void CountTime()
    {
        if (!isTimerStarted)
                return;
        timer += Time.deltaTime;
    }
    //prevent time for being counted and reset it
    public void StopTimer()
    {
        isTimerStarted = false;
        timer = 0;
    }
    //stop the time after anim len time
    // used to tell if the action is ongoing
    public bool StopTimerWhenAnimIsOver(string animName)
    {
        if (isAnimOver( animName))
        {
            StopTimer();
            return false;
        }
        return true;
    }
   
    //tell if time is over anim name lenght
    public bool isAnimOver(string animName)
    {
        return timer > MainLoop.clipTime[animName];
    }



    // Effect for player to return to originalPosision
    private bool BackToOriginalPos(GameObject player, GameObject target, GameObject card)
    {

        bool onGoing = true;
        Vector3 originalPos = player.transform.GetComponent<PlayerAction>().originalPos;
        player.transform.position = Vector3.MoveTowards(player.transform.position, originalPos, Time.deltaTime * 20.0f);
        if (player.transform.position.x - originalPos.x < 0.1f && player.transform.position.y - originalPos.y < 0.1f)
        {
            onGoing = false;
            player.transform.position = originalPos;
        }
        return onGoing;
    }

    // Effect for player to move to Monster while rising the sword
    private bool MoveToMonster(GameObject player, GameObject target, GameObject card)
    {
        bool onGoing = true;

        Transform monster = target.transform;
        Animator animator = player.transform.GetComponent<PlayerAction>().animator;

        player.transform.position = Vector3.MoveTowards(player.transform.position, monster.position, Time.deltaTime * 40.0f);
        animator.SetInteger("playerAttackState", 1);
        if (animator.GetInteger("playerAttackState") ==  1)
                StartTimer();
        CountTime();
        // if ((player.transform.position.x - monster.position.x) < 0.1f && player.transform.position.y - monster.position.y < 0.1f)
        if (Vector3.Distance(player.transform.position, monster.position) < 0.1f)
        {
            //player.transform.position = monster.position;
            onGoing = StopTimerWhenAnimIsOver("move_attack");
        }

        return onGoing;
    }

    //Effect of sword slash animation
    private bool Sword(GameObject player, GameObject target, GameObject card)
    {
        bool onGoing = true;
        Transform monster = target.transform;
        Animator animator = player.transform.GetComponent<PlayerAction>().animator;
        //active sword move animation
        animator.SetInteger("playerAttackState", 2);
        
        if (animator.GetInteger("playerAttackState") == 2)
            StartTimer();

        CountTime();
        onGoing = StopTimerWhenAnimIsOver("sliceAttack_char");
        //deal damage when anim is over
        if (onGoing == false )
        {
            CardInfo info = card.transform.GetComponent<CardManager>().cardInfo;
            MainLoop.DealDamage(target, info.damage, info.aeraOfEffect);
            MainLoop.RemoveCardAtEndOfTurn(card);
        }
        return onGoing;

    }
    //Effect of sword slash animation
    private bool Dagger(GameObject player, GameObject target, GameObject card)
    {
        bool onGoing = true;
        Transform monster = target.transform;
        Animator animator = player.transform.GetComponent<PlayerAction>().animator;
        //active sword move animation
        animator.SetInteger("playerAttackState", 2);

        if (animator.GetInteger("playerAttackState") == 2)
            StartTimer();

        CountTime();
        onGoing = StopTimerWhenAnimIsOver("sliceAttack_char");
        //deal damage when anim is over
        if (onGoing == false)
        {
            CardInfo info = card.transform.GetComponent<CardManager>().cardInfo;
            MainLoop.DealDamage(target, info.damage, info.aeraOfEffect);
            MainLoop.RemoveCardAtEndOfTurn(card);
        }
        return onGoing;

    }
}
