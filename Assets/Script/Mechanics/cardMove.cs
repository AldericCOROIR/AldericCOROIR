using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class cardMove : MonoBehaviour , IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IDragHandler,IDropHandler
{
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1 };
    private bool isDragged;
    public Vector3 orignialPos;
    public Quaternion orignialRot;
    public Vector2 originalScale;
    private float _height100;
    float speed = 10;
    private bool isGoingBack;
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public bool isMovementAllowed;
    public bool rotationOver;
    public bool translationOver;

    void OnMouseOver()
    {
        if (isMovementAllowed && orignialPos == new Vector3(0, 0, 0))
        {
            orignialPos = transform.position;
        }
        //If your mouse hovers over the GameObject with the script attached, output this message
        if (orignialPos == transform.position)
        {
            if (transform.localScale.x  < originalScale.x + 0.5)
                transform.localScale += new Vector3(0.1F, 0.1F, 0);
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        StartCoroutine(DeZoomCard());
    }

    IEnumerator DeZoomCard()
    {
        while (originalScale.x < transform.localScale.x)
        {
            yield return new WaitForSeconds(0.01f / 5);
            transform.localScale -= new Vector3(0.1F, 0.1F, 0);
            if (originalScale.x > transform.localScale.x)
                transform.localScale = originalScale;
        }

    }

    //call at start
    void Start()
    {
        originalScale = (Vector2)transform.localScale;
        _height100 = Screen.height - orignialPos.y;
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    //call when  start dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (!isMovementAllowed)
        {
            eventData.pointerDrag = null;
            return;
        }
        MainLoop.SetCurrentCard(transform.gameObject) ;

        //take original pos
        // orignialPos = transform.position;
        //reset rotation to 0 
        transform.rotation = new Quaternion();

        //center card on mouse
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
        transform.position = canvas.transform.TransformPoint(pos);

        //blockRay Cast for handler onDrop to proc on monster
        canvasGroup.blocksRaycasts = false;
    }
    //call during dragging
    public void OnDrag(PointerEventData eventData)
    {
       
        rectTransform.anchoredPosition += eventData.delta/ (canvas.scaleFactor * 39);
        isDragged = true;
        transform.localScale = ScaleByPos(transform.localScale);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        if (Input.GetMouseButtonUp(0) && isDragged)
        {
            isDragged = false;
            isGoingBack = true;
        }
        MainLoop.SetCurrentCard(null);
        canvasGroup.blocksRaycasts = true;
    }
    


    // Update is called once per frame
    void Update()
    {
        if (!isGoingBack)
            isMovementAllowed = rotationOver && translationOver;
        isMovementAllowed = !MainLoop.isFightOnGoing;
        //going back to original pos
        if (isGoingBack)
        {
            isMovementAllowed = false;
            Vector3 v = Vector3.MoveTowards(transform.position, orignialPos, speed * Time.deltaTime);
            v.z = transform.position.z;
            transform.position = v;
            transform.localScale = Vector2.MoveTowards(transform.localScale, originalScale, Time.deltaTime);
            // if nearly at origninal pos fix to origninal pos 
            if (transform.position.x - orignialPos.x < 0.1f && transform.position.y - orignialPos.y < 0.1f)
            {
                isGoingBack = false;
                transform.rotation = orignialRot;
                transform.position = orignialPos;
            }
        }
    }

    
    Vector2 ScaleByPos(Vector2 baseScale)
    {
        float ratio = (Camera.main.WorldToScreenPoint(transform.position).y - orignialPos.y) * 100 / _height100;
        if (ratio >= 30 &&  (ratio - 30) / 30 > 0)
        {
            Vector2 newScale = originalScale - originalScale * (ratio - 30) / 30;
            if (newScale.x > originalScale.x / 4 && newScale.x < originalScale.x)
                baseScale = newScale;
        }
        return baseScale;
        
    }

}
