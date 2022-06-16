using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translate : MonoBehaviour
{
    enum childIndex { FRONT_CARD = 0, BACK_CARD = 1 };
    public int timer;
    public GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTranslate(Vector3 target, float speed)
    {
        StartCoroutine(CalculateTranslate(target, speed));
    }

    IEnumerator CalculateTranslate(Vector3 target, float speed)
    {
        while ((cardPrefab.transform.position.x + (Vector3.right * 0.01f * speed).x) <= target.x)
        {
            cardPrefab.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<cardMove>().translationOver = false;
            yield return new WaitForSeconds(0.01f);
            cardPrefab.transform.Translate(Vector3.right * 0.01f * speed, Space.World);
            timer++;
        }

        cardPrefab.transform.position = new Vector3(target.x, cardPrefab.transform.position.y, cardPrefab.transform.position.z);
        //Debug.Log("card moved at " + cardPrefab.transform.position);
        timer = 0;
        cardPrefab.transform.GetChild((int)childIndex.FRONT_CARD).GetComponent<cardMove>().translationOver  = true;
    }
}
