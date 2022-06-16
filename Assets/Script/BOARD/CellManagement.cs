using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellManagement : MonoBehaviour, IDropHandler
{
    public GameObject Canvas;
    public GameObject Cell;
    public string cellName;
    public int row;
    public int col;
    private CanvasGroup canvasGroup;
    private GameObject ChessBoard;
    private bool wasOverCell;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        ChessBoard = GameObject.Find("ChessBoard");
        wasOverCell = false;
    }

    public void Init(string _name, int _row, int _col)
    {
        row = _row;
        col = _col;
        cellName = _name;
    }
    void OnMouseOver()
    {
        /* //If your mouse hovers over the GameObject with the script attached, output this message
         Debug.Log("Mouse is over CELL." + cellName);
         if (MainLoop.currentCard != null)
         {
             transform.GetComponent<SpriteRenderer>().enabled = true;
         }*/
    }

    void OnMouseExit()
    {
       /* //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        transform.GetComponent<SpriteRenderer>().enabled = false;*/
    }

    public void OnDrop(PointerEventData eventData)
    {

      /*  Debug.Log("drop on cell");*/

    }
    public void Show(bool show)
    {
        transform.GetComponent<SpriteRenderer>().enabled = show;

    }
    // Update is called once per frame
    void Update()
    {
       
        Vector2 mousePos = Input.mousePosition;

        //green when mouse over
        Vector2 lp;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), mousePos, Camera.main, out lp);
        bool isOverCell = transform.GetComponent<RectTransform>().rect.Contains(lp);
        if (isOverCell)
        {
            if (MainLoop.currentCard != null)
            {
                List<(int, int)> aeraOfEffect = MainLoop.currentCard.GetComponent<CardManager>().cardInfo.aeraOfEffect;
                foreach (var aoe in aeraOfEffect)
                {
                    int affectedRow = row + aoe.Item1;
                    int affectedCol = col + aoe.Item2;
                    Debug.Log(affectedRow + " " + affectedCol);
                    ChessBoard.GetComponent<ChessBoardManagement>().HighLightCell(affectedRow, affectedCol, true);
                    /*                    Debug.Log(affectedRow + " " + affectedCol + " "  + ChessBoard.GetComponent<ChessBoardManagement>().grid[affectedRow, affectedCol]);*/

                }
            }
        }
        if (isOverCell == false && wasOverCell == true)
        {
            ChessBoard.GetComponent<ChessBoardManagement>().HighLightAll(false);
        }
        wasOverCell = isOverCell;


        /*  Vector2 mousePos = Input.mousePosition;

          //green when mouse over
          Vector2 lp;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), mousePos, Camera.main, out lp);
          transform.GetComponent<SpriteRenderer>().enabled = transform.GetComponent<RectTransform>().rect.Contains(lp);*/

    }

    /* void OnMouseOver()
     {
         transform.GetComponent<SpriteRenderer>().enabled = true;

     }

     void OnMouseExit()
     {
         transform.GetComponent<SpriteRenderer>().enabled = false;
         Debug.Log("Exit " + cellName);
     }*/
}
