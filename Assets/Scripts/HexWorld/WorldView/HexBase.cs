using HexWorld;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBase : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite[] sprites; 
    [SerializeField] private HexEdge[] edges;

    private HexEdge GetEdge(Direction direction)
    {
        return edges.FirstOrDefault(HexEdge => HexEdge.direction == direction);
    }

    public void Show(HexWorld.CellType type, CellEdge[] edges)
    {
        render.sprite = type.sprite;
        foreach (var item in edges)
        {
            HexEdge edge = GetEdge(item.Direction);
            //edge.Movable = item.Movable;
            edge.Show(item.Movable, item.EnviromentType);
        }
    }
}
