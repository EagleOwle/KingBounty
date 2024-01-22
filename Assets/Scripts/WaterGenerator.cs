using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using System.Linq;

public class WaterGenerator : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private int randomPercent;
    [Range(0, 100)]
    [SerializeField] private int iteration;
    [SerializeField] private int currentIteration;

    [SerializeField]private List<HexCell> nextCells = new List<HexCell>();

    public void GenerateWater(List<HexCell> cells)
    {
        EditorCoroutineUtility.StopCoroutine(IEWaitForSeconds() as EditorCoroutine);

        nextCells = new List<HexCell>();
        currentIteration = iteration;

        for (int i = 0; i < cells.Count; i++)
        {
            foreach (var item in cells[i].neighbors)
            {
                if (item.cell == null)
                {
                    cells[i].Type = CellType.Water;

                    if (UnityEngine.Random.Range(0, 100) <= randomPercent)
                    {
                        nextCells.Add(cells[i]);
                    }

                    break;
                }
            }
        }

        SetNeighborsWater();
    }

    private void SetNeighborsWater()
    {
        HexCell[] tmpCells = nextCells.ToArray();
        nextCells = new List<HexCell>();

        for (int i = 0; i < tmpCells.Length; i++)
        {
            CellNeighbor[] neighbors = tmpCells[i].neighbors;

            System.Random rnd = new System.Random();
            neighbors = neighbors.OrderBy(x => (rnd.Next())).ToArray();

            foreach (var item in  neighbors)
            {
                if (item.cell != null && item.cell.Type != CellType.Water)
                {
                    item.cell.Type = CellType.Water;

                    if (UnityEngine.Random.Range(0, 100) <= randomPercent)
                    {
                        nextCells.Add(item.cell);
                    }

                    //break;
                }
            }
        }

        currentIteration--;

        if (currentIteration > 0 && nextCells.Count > 0)
        {
            EditorCoroutineUtility.StartCoroutine(IEWaitForSeconds(), this);
        }
        else
        {
            currentIteration = 0;
        }
    }

    private IEnumerator IEWaitForSeconds()
    {
        yield return new EditorWaitForSeconds(Time.deltaTime);
        SetNeighborsWater();
    }

    public void ClearWater()
    {
        foreach (var item in MapGenerator.Singleton.Cells)
        {
            if(item.Type == CellType.Water)
            {
                item.Type = CellType.Ground;
            }
        }
    }

}
