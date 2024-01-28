using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexWorld
{
    [System.Serializable]
    public class Cell
    {
        public Cell(Vector2 fieldPosition, CellType type, GameField gameField)
        {
            this.fieldPosition = fieldPosition;
            //this.size = size;
            this.type = type;
            this.gameField = gameField;

            edges = new CellEdge[6]
            {
                new CellEdge(Direction.RightUp),
                 new CellEdge(Direction.Right),
                  new CellEdge(Direction.RightDown),
                   new CellEdge(Direction.LeftDown),
                    new CellEdge(Direction.Left),
                     new CellEdge(Direction.LeftUp)
        };

        }

        public Vector2 FieldPosition {get{return fieldPosition;}}
        private Vector2 fieldPosition;

        //public float Size => size;
        //private float size;

        public CellEdge[] Edges => edges;
        [SerializeField] private CellEdge[] edges;

        public CellType Type => type;
        private CellType type;

        public void SwapType(CellType n)
        {
            type = n;
        }

        private GameField gameField;

        public void FindNeighbor()
        {
            foreach (var item in edges)
            {
                Vector2 neighborCoor = NegthborGridPosition(item.Direction);
                gameField.GetHex(neighborCoor, out Cell cell);
                item.SetNeighbor(cell);
            }
        }

        public Vector2 NegthborGridPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.RightUp:
                    return new Vector2(fieldPosition.x + 0.5f, fieldPosition.y + 0.86f);
                case Direction.Right:
                    return new Vector2(fieldPosition.x + 1, fieldPosition.y);
                case Direction.RightDown:
                    return new Vector2(fieldPosition.x + 0.5f, fieldPosition.y - 0.86f);
                case Direction.LeftDown:
                    return new Vector2(fieldPosition.x - 0.5f, fieldPosition.y - 0.86f);
                case Direction.Left:
                    return new Vector2(fieldPosition.x - 1, fieldPosition.y);
                case Direction.LeftUp:
                    return new Vector2(fieldPosition.x - 0.5f, fieldPosition.y + 0.86f);
                default:
                    return Vector2.zero;
            }
        }

        public void SetOppositeMovable(Direction direction, bool movable)
        {
            foreach (var item in edges)
            {
                if (type.Name == "Blue")
                {
                    item.Movable = true;
                    continue;
                }

                if (item.Direction == OppositDirection(direction))
                {
                    item.Movable = movable;
                }
            }
        }

        private Direction OppositDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.RightUp:
                    return Direction.LeftDown;
                case Direction.Right:
                    return Direction.Left;
                case Direction.RightDown:
                    return Direction.LeftUp;
                case Direction.LeftDown:
                    return Direction.RightUp;
                case Direction.Left:
                    return Direction.Right;
                case Direction.LeftUp:
                    return Direction.RightDown;
                default:
                    return Direction.None;
            }
        }

       

    }
}