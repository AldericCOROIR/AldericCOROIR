using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour
{

    static public Dictionary<string, CardInfo> Book = new Dictionary<string, CardInfo>
    {
        {"dagger", new CardInfo(40, new List<(int, int)>{ (0,0) } ) },
         {"sword", new CardInfo(10, new List<(int, int)>{ (0,0), (1,0) } ) }
      /*  { "dagger", 
            new CardInfo(10, new List<(int, int)> ( (0,0), (0,1)) )
        },*/
    };
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public CardInfo GetCardInfo(string _cardName)
    {
        return Book[_cardName];
    }
}
