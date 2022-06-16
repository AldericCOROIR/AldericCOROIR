using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CallBackEffect = System.Func<UnityEngine.GameObject, UnityEngine.GameObject, UnityEngine.GameObject, bool>;

public class PlayerAction : MonoBehaviour
{
    public class Action
    {
        public GameObject player;
        public GameObject target;
        public GameObject card;
        public bool onGoing;
        public Queue<Effect> effectQ;
        public string effectName;
        private Effect currentEffect;
        public Action(GameObject _player, GameObject _target, GameObject _card)
        {
            player = _player;
            target = _target;
            card = _card;
            effectQ = new Queue<Effect>(card.transform.GetComponent<CardManager>().GetEffect());
            effectName = card.transform.GetComponent<CardManager>().cardName;
            onGoing = true;
        }
        public Action(GameObject _player, string _effectName)
        {
            player = _player;
            effectQ = new Queue <Effect> (EffectFactory.effectMap[_effectName]);
            effectName = _effectName;
            onGoing = true;
        }

        public void DoIt()
        {
            if (effectQ.Count > 0 && (currentEffect == null || onGoing == false))
            {
               currentEffect = effectQ.Dequeue();
            }
            onGoing = currentEffect.Call(player, target, card);
        }

        public bool OnGoing()
        {
            return onGoing || effectQ.Count > 0;
        }
    }

    Queue<Action> actionQueue;
    Action currentAction;
    public Vector3 originalPos;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        actionQueue = new Queue<Action>();
        originalPos = transform.position;
    }
   
    // Update is called once per frame
    void Update()
    {
       
        if (MainLoop.isFightOnGoing)
        {
            //if action available and no action ongoing pick one
            if (actionQueue.Count > 0 && (currentAction == null || currentAction?.OnGoing() == false))
            {
                    currentAction = actionQueue.Dequeue();
            }
            
            //if current action exist do it
            if (currentAction != null)
                currentAction.DoIt();
            //Debug.Log(currentAction?.effectName + " " + actionQueue.Count + " "   +transform.GetComponent<PlayerAction>().animator.GetInteger("playerAttackState"));
            //if current action is over and no more action available stop the fight for this turn
            if ((currentAction == null || currentAction?.OnGoing() == false) && actionQueue.Count == 0)
            {
                currentAction = null;
                MainLoop.EndOfTurnAnimation();
            }
        }
    }

    
    public void addAction(GameObject monster, GameObject card)
    {

        actionQueue.Enqueue(new Action(transform.gameObject, monster, card));
    }
    public void addAction(string name)
    {
        actionQueue.Enqueue(new Action(transform.gameObject, name));
    }
    
}
