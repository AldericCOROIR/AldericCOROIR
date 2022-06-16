using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MainLoop : MonoBehaviour
{
    static GameObject Deck;
    static GameObject Hand;
    static GameObject Player;
    static GameObject Cemetery;
    static GameObject Monsters;
    static MonsterFactory MonsterFactory;
    static List<GameObject> cardToRemove;
    static List<GameObject> monsterToKill;

    static public Dictionary<string, float> clipTime;
    public Animator animator;
    static public bool isFightOnGoing;
    static public GameObject currentCard;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Monsters = GameObject.Find("Monsters");
        Deck = GameObject.Find("Deck");
        Cemetery = GameObject.Find("Cemetery");
        Hand = GameObject.Find("Hand");
        cardToRemove = new List<GameObject>();
        isFightOnGoing = false;
        clipTime = new Dictionary<string, float>();
        monsterToKill = new List<GameObject>();
        currentCard = null;
        MonsterFactory = Monsters.transform.GetComponent<MonsterFactory>();
        FetchAnimClipTime();

    }

    static public void DealDamage(GameObject monster, int damage, List<(int, int)> aeraOfEffect)
    {
        int baseRow = monster.GetComponent<MonsterManagement>().row;
        int baseCol = monster.GetComponent<MonsterManagement>().col;
        foreach (var aoe in aeraOfEffect)
        {
            int affectedRow = baseRow + aoe.Item1;
            int affectedCol = baseCol + aoe.Item2;
            MonsterFactory.DealDamageToMonster(affectedRow, affectedCol, damage);
        }
    }

    static public void SetCurrentCard(GameObject card)
    {
        currentCard = card;
    }
    static public void KillMonsterAtEndOfTurn(GameObject monster)
    {
       
        monsterToKill.Add(monster);
    }
    static public void RemoveCardAtEndOfTurn(GameObject card)
    {
        cardToRemove.Add(card);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchAnimClipTime()
    {
        Array clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            clipTime[clip.name] = clip.length;
        }
    }

    //when clicking endTurn Button => add the action for moving the player back to original position + start the fight animation.
    static public void EndTrunClicked()
    {
        Player.transform.GetComponent<PlayerAction>().addAction("backToOriginalPos");
        isFightOnGoing = true;
    }

    static public void EndOfTurnAnimation()
    {
        isFightOnGoing = false;

        //kill monster
        foreach (GameObject monster in monsterToKill)
        {
            int row = monster.GetComponent<MonsterManagement>().row;
            int col = monster.GetComponent<MonsterManagement>().col;
            MonsterFactory.Grid[row, col] = null;
            Destroy(monster);
        }
        monsterToKill.Clear();

        //clear monster board
        foreach (Transform monster in Monsters.transform)
        {
            monster.transform.GetComponent<MonsterManagement>().ClearCardBoard();
        }

        //destroy used card from hand
        foreach (GameObject card in cardToRemove)
        {
           Destroy(card);
        }
        cardToRemove.Clear();


        //draw for next turn
        Deck.transform.GetComponent<deckManager>().Draw(7);
    }
   
}
