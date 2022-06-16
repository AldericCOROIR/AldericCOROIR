using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCard : MonoBehaviour
{
    public GameObject CardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //0 = front, 1 == back 
    public void HideSide(int side)
    {
        transform.GetChild(side).gameObject.SetActive(false);
    }
    public void ShowSide(int side)
    {
        transform.GetChild(side).gameObject.SetActive(true);
    }

    public void changeZ(float value)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, value);
        foreach (Transform child in transform)
        {
            child.position = new Vector3(child.position.x, child.position.y, value);
        }
    }

}
