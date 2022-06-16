using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endTurnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = transform.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
       
    }

    void TaskOnClick()
    {
        MainLoop.EndTrunClicked();

    }
    // Update is called once per frame
    void Update()
    {

      

    }
}
