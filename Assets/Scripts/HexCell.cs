using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private Material waterMat, defaultMat;
    [SerializeField] private new Renderer renderer;

    private CellType cellType;
    public CellType Type
    {
        get
        {
            return cellType;
        }

        set
        {
            cellType = value;

            if(cellType == CellType.Water)
            {
                renderer.material = waterMat;
            }
            else
            {
                renderer.material = defaultMat;
            }
        }
    }
    public Vector2 gridPosition;
    public CellNeighbor[] neighbors;

    public void SetNeighbor(CellDirection direction, HexCell cell)
    {
        neighbors[(int)direction].cell = cell;
    }


}

[System.Serializable]
public class CellNeighbor
{
    public string name;
    public CellDirection direction;
    public HexCell cell;

}
