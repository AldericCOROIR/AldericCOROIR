using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardManagement : MonoBehaviour
{

    public GameObject[,] grid;
    public GameObject CellPrefab;
    int maxRow;
    int maxCol;
    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[,] { { null, null, null, null }, { null, null, null, null }, { null, null, null, null } };

        System.Random rnd = new System.Random();
        maxRow = 3;
        maxCol = 4;
        for (int row = 0; row < maxRow; row++)
        {
            for (int col = 0; col < maxCol; col++)
            {
                GameObject cell = Instantiate(CellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                cell.GetComponent<RectTransform>().SetParent(transform, false);

                cell.transform.Find("Canvas/cell").GetComponent<CellManagement>().Canvas.transform.localPosition = new Vector3(0, 0, 0);
                Vector3 position = new Vector3(col * 110, row * 110, 0f);
                cell.transform.localPosition = position;
                cell.name = "Cell_" + row + "_" + col;
                cell.transform.Find("Canvas/cell").GetComponent<CellManagement>().Init(cell.name, row, col);
                grid[row, col] = cell;
            }
        }
     
    }

    public void HighLightCell(int row, int col, bool highLight)
    {
        Debug.Log(row + " < " + maxRow + " && " + col +" < "+maxCol);
        if (row < maxRow && col < maxCol)
            grid[row, col].transform.Find("Canvas/cell").GetComponent<CellManagement>().Show(highLight);
    }
    public void HighLightAll(bool hightLight)
    {
        for (int row = 0; row < maxRow; row++)
        {
            for (int col = 0; col < maxCol; col++)
            {
                grid[row, col].transform.Find("Canvas/cell").GetComponent<CellManagement>().Show(hightLight);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
