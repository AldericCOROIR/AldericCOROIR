using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipCard : MonoBehaviour
{
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1 };
    public float x, y, z;
    public GameObject cardPrefab;
    public bool cardBackIsActive;
    public int timer;
    public GameObject frontCard;
    public GameObject backCard;
    // Start is called before the first frame update
    void Start()
    {
        y = 1.0f;
        cardBackIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
            StartFlip(false);*/
    }

    public void StartFlip(bool backOnTop)
    {
       
        frontCard.SetActive(!backOnTop);
        backCard.SetActive(backOnTop);
        cardBackIsActive = backOnTop;
        StartCoroutine(CalculateFlip());
    }

    public void Flip()
    {
        if (cardBackIsActive)
        {
            frontCard.SetActive(false);
            cardBackIsActive = false;
            backCard.SetActive(true);
        }
        else
        {
            backCard.SetActive(false);
            frontCard.SetActive(true);
            cardBackIsActive = true;
        }
    }

    IEnumerator CalculateFlip()
    {
        for(int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(0.01f/5);
            cardPrefab.transform.Rotate(new Vector3(x,y,z));
            timer++;
            if (timer == 90 || timer == -90)
            { 
                Flip();
            }
        }
        backCard.SetActive(false);
        //reset rotation to 0 instead of 180/-180
        cardPrefab.transform.rotation = new Quaternion();
        timer = 0;
        cardPrefab.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<cardMove>().rotationOver = true;
    }


}
