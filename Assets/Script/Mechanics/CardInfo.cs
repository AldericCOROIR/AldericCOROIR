using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo 
{
    public List<(int, int)> aeraOfEffect;

    public int damage;

    public CardInfo(int _damage, List<(int,int)> _aeraOfEffect)
    {
        damage = _damage;
        aeraOfEffect = _aeraOfEffect;
    }

    public void Init(CardInfo card)
    {
        damage = card.damage;
        aeraOfEffect = card.aeraOfEffect;
    }
   
}
