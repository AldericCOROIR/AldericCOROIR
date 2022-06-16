using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handManager : MonoBehaviour
{
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1 };
    List<GameObject> cardInHand;
    public int maxHandSize;
    public int currentAmountOfCard;

    public int NbEmptySlot()
    {
        return maxHandSize - currentAmountOfCard;
    }
    // Update is called once per frame
    void Update()
    {


    }
    // Start is called before the first frame update
    void Start()
    {
        maxHandSize = 7;
        currentAmountOfCard = 0;
        //create hand List
        cardInHand = new List<GameObject>();
    }
    public void RemoveCard(GameObject cardPrefab)
    {
        cardInHand.Remove(cardPrefab);
//        cardPrefab.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<SpriteRenderer>().enabled = false;
        cardPrefab.SetActive(false);
        currentAmountOfCard--;
    }
    public void AddCard(GameObject cardPrefab)
    {
        cardPrefab.GetComponent<RectTransform>().SetParent(transform.GetComponent<RectTransform>(), false);
        cardInHand.Add(cardPrefab);
    }

    public void DeckToCardSlot(List<GameObject> cardList)
    {
        int totalCard = cardList.Count + cardInHand.Count;
        currentAmountOfCard = totalCard;
        //move card already in hand to adjusted slot
        foreach(var card in cardInHand)
        {
            MoveCardToSlot(card, totalCard--);
        }
        foreach (var card in cardList)
        {
            AddCard(card);
            MoveCardToSlot(card, totalCard--, true);
        }

    }

    public void MoveCardToSlot(GameObject card, int slot, bool fromDeck=false)
    {
        float cardWidth = card.gameObject.GetComponent<RectTransform>().rect.width;
    
        float handWidth = 12.0f;
        float step = handWidth / currentAmountOfCard; // cardWidth / handWidth;
        Vector3 slotPosition = new Vector3(step*slot - handWidth/2, 0.0f, 0.0f);
        
        if (fromDeck)
        {
            //place card Over deck
            Vector3 v = new Vector3(-8, 0.0f, 0.0f);
            card.GetComponent<RectTransform>().localPosition = v;
            card.transform.localPosition = new Vector3(card.transform.localPosition.x, card.transform.localPosition.y, slot);
            //active both face for translation
            /* card.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<SpriteRenderer>().enabled = true;
             card.transform.GetChild((int)childIndex.BACK_CARD).GetComponent<SpriteRenderer>().enabled = true;*/
            card.transform.GetComponent<ShowHideCard>().ShowSide((int)childIndex.FRONT_CARD);
            card.transform.GetComponent<ShowHideCard>().ShowSide((int)childIndex.BACK_CARD);
            card.transform.GetComponent<translate>().StartTranslate(slotPosition, 10);
            card.transform.GetComponent<flipCard>().StartFlip(true);
        }
        else
        {
            card.transform.localPosition = new Vector3(card.transform.localPosition.x, card.transform.localPosition.y, slot);
            card.transform.GetComponent<translate>().StartTranslate(slotPosition, 10);
        }

    }
}
