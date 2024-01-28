using HexWorld;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBase : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private MeshRenderer meshRender;
    [SerializeField] private Sprite[] sprites; 
    [SerializeField] private HexEdge[] edges;

    private HexEdge GetEdge(Direction direction)
    {
        return edges.FirstOrDefault(HexEdge => HexEdge.direction == direction);
    }

    public void Show(HexWorld.CellType type, CellEdge[] edges)
    {
        //spriteRender.sprite = type.sprite;
        meshRender.material = type.material;
        foreach (var item in edges)
        {
            HexEdge edge = GetEdge(item.Direction);
            edge.Show(item.Movable, item.EnviromentType);
        }
    }
}
