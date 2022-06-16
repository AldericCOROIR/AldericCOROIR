using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MonsterManagement : MonoBehaviour, IDropHandler
{
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1 };
    public GameObject CardPrefab;
    public GameObject CardBoard;
    public List<GameObject> storedCardList;
    public GameObject player;
    public GameObject cemetery;
    public GameObject hand;
    public string monsterName;
    public int row;
    public int col;
    public int maxHealth;
    public int currentHealth;

    public GameObject healthBar;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
       
        storedCardList = new List<GameObject>();
        player = GameObject.Find("Player");
        cemetery = GameObject.Find("Cemetery");
        hand = GameObject.Find("Hand");
        maxHealth = 50;
        currentHealth = maxHealth;
        healthBar.transform.GetComponent<healthBar>().SetMaxHealth(maxHealth);
       // Canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    public void Init(string _name, int _row, int _col)
    {
        row = _row;
        col = _col;
        monsterName = _name;
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over Monster.");
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }


    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.transform.GetComponent<healthBar>().SetHealth(currentHealth);
        if (currentHealth <= 0)
            MainLoop.KillMonsterAtEndOfTurn(transform.gameObject);
    }
    //call on drop over monster
    public void OnDrop(PointerEventData eventData)
    {

            Debug.Log("drop no monster");

        if (eventData.pointerDrag != null)
        {
            //instanciate card clone
            GameObject newCradPrefab = Instantiate(eventData.pointerDrag, CardBoard.transform.position, Quaternion.identity);
            newCradPrefab.GetComponent<RectTransform>().SetParent(CardBoard.GetComponent<RectTransform>(), false);
            newCradPrefab.transform.GetComponent<cardMove>().rotationOver = false;
            //scale card clone
            newCradPrefab.transform.localScale = new Vector3((CardBoard.transform.localScale.y / CardBoard.transform.localScale.x)/4.0f, 1/4.0f, 0 * CardBoard.transform.localScale.z);
            //position card clone
            newCradPrefab.GetComponent<RectTransform>().anchoredPosition = CardBoard.GetComponent<RectTransform>().anchoredPosition;
            newCradPrefab.GetComponent<RectTransform>().localPosition = new Vector3(newCradPrefab.transform.localScale.x * (-3 + storedCardList.Count)*4, 0, storedCardList.Count);
            newCradPrefab.transform.GetComponent<cardMove>().rotationOver = false;
            //add combat action
            storedCardList.Add(newCradPrefab);
            player.transform.GetComponent<PlayerAction>().addAction(transform.gameObject, eventData.pointerDrag);

            //move card to cemtery
            /*eventData.pointerDrag.GetComponent<RectTransform>().SetParent(cemetery.GetComponent<RectTransform>(), false);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = cemetery.GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.transform.GetComponent<cardMove>().orignialPos = cemetery.transform.position;*/
            hand.transform.GetComponent<handManager>().RemoveCard(eventData.pointerDrag.transform.parent.gameObject);


        }
    }

    public void ClearCardBoard()
    {
        foreach(var card in storedCardList)
        {
            Destroy(card);
        }
        storedCardList.Clear();
    }
}
