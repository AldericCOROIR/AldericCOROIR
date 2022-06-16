using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CallBackEffect = System.Func<UnityEngine.GameObject, UnityEngine.GameObject, bool>;

public class CardManager : MonoBehaviour
{
    public string cardName;
    public GameObject skin;
    public CardInfo cardInfo;
    public void Instanciante(string _cardName)
    {
        
        cardName = _cardName;
        string path = "Sprites/" + _cardName + "_skin"; // filename.png should be stored in your Assets/Resources folder
       
        Sprite sprite = Resources.Load<Sprite>(path);
        Debug.Log(path + " " +  sprite == null);
        skin.transform.GetComponent<SpriteRenderer>().sprite = sprite;
        cardInfo = CardFactory.GetCardInfo(cardName);
        
    }


    public Queue<Effect>  GetEffect()
    {
        return EffectFactory.effectMap[cardName];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}