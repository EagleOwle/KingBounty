using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Cell cellPrefab;
    public float cellSize;
    public Vector2 mapSize;
    public int biomCount = 1;
    public List<Cell> cells = new List<Cell>();

    private void Start()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(x, 0, y) * cellSize, Quaternion.identity);
                cells.Add(cell);
            }
        }

        cellPrefab.gameObject.SetActive(false);
    }

    public void CreateFirstCell()
    {
        
    }
}
