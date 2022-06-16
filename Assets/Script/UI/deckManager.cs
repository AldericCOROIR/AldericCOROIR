using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckManager : MonoBehaviour
{
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1 };
    [SerializeField] public GameObject CardPrefab;
    [SerializeField] public GameObject Hand;
    [SerializeField] public Canvas canvas;
    GameObject backgroundCard;
    // Start is called before the first frame update
    void Start()
    {
        //instanciate card clone
        backgroundCard = Instantiate(CardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        backgroundCard.GetComponent<RectTransform>().SetParent(transform.GetComponent<RectTransform>(), false);
        //backgroundCard.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<SpriteRenderer>().enabled = false;
        backgroundCard.transform.GetComponent<ShowHideCard>().HideSide((int)childIndex.FRONT_CARD);


        //        Draw(7);
    }

    GameObject CreateCard(int i)
    {
        GameObject card = Instantiate(CardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        card.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<cardMove>().canvas = canvas;
        if (i%2 == 0)
            card.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<CardManager>().Instanciante("sword");
        else
            card.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<CardManager>().Instanciante("dagger");
        card.GetComponent<RectTransform>().SetParent(transform.GetComponent<RectTransform>(), false);


        //        card.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<SpriteRenderer>().enabled = false;
        card.transform.GetComponent<ShowHideCard>().HideSide((int)childIndex.FRONT_CARD);

        return card;
    }
    //draw a card : move it to hand space + give ownership to hand
    public void Draw(int amount)
    {
        List<GameObject> drawedCard = new List<GameObject>();
        int maxDraw = Hand.GetComponent<handManager>().NbEmptySlot();
        for (int i = 0; i < maxDraw; i++)
        {
            GameObject card = CreateCard(i);
            drawedCard.Add(card);
        }
        Hand.GetComponent<handManager>().DeckToCardSlot(drawedCard);

    }
    void MoveDrawCradToHand()
    {

      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
