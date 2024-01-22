using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellDirection
{
    NE, E, SE, SW, W, NW, NONE
}

public static class CellMetrics
{
    public const float outerRadius = 1f;
    public const float innerRadius = outerRadius * 0.866025404f;

    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };

    public static CellDirection FindNeighbor(HexCell currentCell, HexCell directionCell)
    {
        return CellDirection.NE;
    }

    /// <summary>
    /// ¬озвращает противоположное направление.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static CellDirection Opposite(this CellDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }

    public static Vector3 FindPositionByDirection(CellDirection direction)
    {
        Vector3 position = Vector3.zero;

        switch (direction)
        {
            case CellDirection.NE:
                position.x = CellMetrics.innerRadius;
                position.y = 0;
                position.z = CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.E:
                position.x = CellMetrics.innerRadius * 2;
                position.y = 0;
                position.z = 0;
                break;
            case CellDirection.SE:
                position.x = CellMetrics.innerRadius;
                position.y = 0;
                position.z = -CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.SW:
                position.x = -CellMetrics.innerRadius;
                position.y = 0;
                position.z = -CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.W:
                position.x = -CellMetrics.innerRadius * 2;
                position.y = 0;
                position.z = 0;
                break;
            case CellDirection.NW:
                position.x = -CellMetrics.innerRadius;
                position.y = 0;
                position.z = CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.NONE:
                break;
        }

        return position;
    }

    public static Vector2 FindGridPositionByDirection(CellDirection direction)
    {
        Vector2 position = Vector2.zero;

        switch (direction)
        {
            case CellDirection.NE:
                position.x = CellMetrics.innerRadius;
                position.y = CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.E:
                position.x = CellMetrics.innerRadius * 2;
                position.y = 0;
                break;
            case CellDirection.SE:
                position.x = CellMetrics.innerRadius;
                position.y = -CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.SW:
                position.x = -CellMetrics.innerRadius;
                position.y = -CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.W:
                position.x = -CellMetrics.innerRadius * 2;
                position.y = 0;
                break;
            case CellDirection.NW:
                position.x = -CellMetrics.innerRadius;
                position.y = CellMetrics.outerRadius * 1.5f;
                break;
            case CellDirection.NONE:
                break;
        }

        return position;
    }

}
