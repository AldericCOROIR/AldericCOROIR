using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterFactory : MonoBehaviour
{
    public GameObject MonsterPrefab;
    public GameObject Monsters;
    public GameObject ChessBoard;
    int maxCol;
    int maxRow;
    public GameObject[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new GameObject[,] { { null, null, null, null }, { null, null, null, null }, { null, null, null, null } };

        System.Random rnd = new System.Random();
        maxRow = 3;
        maxCol = 4;
        GenerateMonster();
    }

    public bool PositionDoesntExist(Vector3 position)
    {
        foreach (Transform monster in transform)
        {
            if (monster.transform.localPosition == position)
            {
                return false;
            }
        }
        return true;
    }
    public void DealDamageToMonster(int row, int col , int damage)
    {
            if (row < maxRow && col < maxCol && Grid[row, col] != null)
                Grid[row, col].GetComponent<MonsterManagement>().TakeDamage(damage);
    }
       
    public void GenerateMonster()
    {

        System.Random rnd = new System.Random();
        int nbMonster = 20;// rnd.Next(3, 8);
        Debug.Log("nb monster = " +  nbMonster);
        for (int i = 0; i < nbMonster; i++)
        {
            //base case is 100 add 10 for visibillity
            int row = rnd.Next(0, 3);
            int col = rnd.Next(0, 4);
            Vector3 position = new Vector3(col * 110, row * 110, 0f); 
            bool isNotDuplicate = true;
            if (PositionDoesntExist(position) == false)
                continue;

            GameObject newMonsterPrefab = Instantiate(MonsterPrefab, new Vector3(0,0,0), Quaternion.identity);
            newMonsterPrefab.GetComponent<RectTransform>().SetParent(transform, false);

            newMonsterPrefab.GetComponent<MonsterManagement>().Canvas.transform.localPosition = new Vector3(0, 0, 0);
            newMonsterPrefab.transform.localPosition = position;
            newMonsterPrefab.name = "Monster_" + row + "_" + col;
            newMonsterPrefab.GetComponent<MonsterManagement>().Init(newMonsterPrefab.name, row, col);
            Grid[row, col] = newMonsterPrefab;
            /* newMonsterPrefab.GetComponent<RectTransform>().anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
             newMonsterPrefab.GetComponent<RectTransform>().localPosition = new Vector3(0, 0.0f, 0);*/
            // newMonsterPrefab.GetComponent<RectTransform>().SetParent(transform.GetComponent<RectTransform>(), false);
            //newMonsterPrefab.transform.position = new Vector3(rnd.Next(0, 5)*70, rnd.Next(0, 2) * -70,0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
