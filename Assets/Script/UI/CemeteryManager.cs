using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeteryManager : MonoBehaviour
{
    // Start is called before the first frame update
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1, CEMETERY_CARD = 2 };
    [SerializeField] public GameObject CardPrefab;
    GameObject backgroundCard;
    void Start()
    {
        //instanciate card clone
        backgroundCard = Instantiate(CardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        backgroundCard.GetComponent<RectTransform>().SetParent(transform.GetComponent<RectTransform>(), false);
        backgroundCard.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<SpriteRenderer>().enabled = false;
        backgroundCard.transform.GetChild((int)childIndex.BACK_CARD).GetComponent<SpriteRenderer>().enabled = false;
        backgroundCard.transform.GetChild((int)childIndex.CEMETERY_CARD).GetComponent<SpriteRenderer>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
