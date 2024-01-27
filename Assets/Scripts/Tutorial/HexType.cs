using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexWorld
{
    public class HexType : MonoBehaviour
    {
        [SerializeField] private Transform hexTransform;
        [SerializeField] private SpriteRenderer render;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private CellEdge[] edges;

        public Vector2 arrayPosition;
        public int type = 0;
        public int grow = 1;
        public int width = 40;
        public int freq = 1;

        private IGetHex hexHolder;

        public void Initialise(IGetHex hexHolder, float scale, Vector2 arrayPosition)
        {
            this.hexHolder = hexHolder;
            //hexTransform.localScale = Vector3.one * scale;
            this.arrayPosition = arrayPosition;
            foreach (var item in edges)
            {
               // item.Initialise(this);
            }
        }

        public void FindNeighbor()
        {
            //foreach (var item in edges)
            //{
            //    Vector2 neighborCoor = NegthborGridPosition(item.Direction);
            //    hexHolder.GetHex(neighborCoor, out HexType newHex);
            //    item.SetNeighbor(newHex);
            //}
        }

        public void SwapType(int n)
        {
            type = n;
            render.sprite = sprites[type];
        }

        public void Grow()
        {
            List<Vector2> coord = GetNeighborCoordinate(arrayPosition);

            for (int i = 0; i < freq; i++)
            {
                int m = Mathf.RoundToInt(Random.value * 5);

                if (coord[m].x < 0)
                {
                    Vector2 newCoord = new Vector2(coord[m].x + width, coord[m].y);
                    coord[m] = newCoord;
                }
                else
                {
                    if (coord[m].x >= width)
                    {
                        Vector2 newCoord = new Vector2(coord[m].x - width, coord[m].y);
                        coord[m] = newCoord;
                    }
                }

                if (hexHolder.GetHex(coord[m], out HexType newHex))
                {
                    if (newHex.type == 0)
                    {
                        newHex.SwapType(type);

                        if (grow - 1 > 0)
                        {
                            newHex.grow = grow - 1;
                            newHex.freq = freq;
                            newHex.width = width;
                            newHex.Grow();
                        }
                    }
                }

            }

        }

        private List<Vector2> GetNeighborCoordinate(Vector2 arrayPosition)
        {
            List<Vector2> coord = new List<Vector2>();
            coord.Add(new Vector2(arrayPosition.x + 1, arrayPosition.y));
            coord.Add(new Vector2(arrayPosition.x - 1, arrayPosition.y));
            coord.Add(new Vector2(arrayPosition.x - 0.5f, arrayPosition.y + 0.86f));
            coord.Add(new Vector2(arrayPosition.x + 0.5f, arrayPosition.y + 0.86f));
            coord.Add(new Vector2(arrayPosition.x - 0.5f, arrayPosition.y - 0.86f));
            coord.Add(new Vector2(arrayPosition.x + 0.5f, arrayPosition.y - 0.86f));
            return coord;
        }

        public Vector2 NegthborGridPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.RightUp:
                    return new Vector2(arrayPosition.x + 0.5f, arrayPosition.y + 0.86f);
                case Direction.Right:
                    return new Vector2(arrayPosition.x + 1, arrayPosition.y);
                case Direction.RightDown:
                    return new Vector2(arrayPosition.x + 0.5f, arrayPosition.y - 0.86f);
                case Direction.LeftDown:
                    return new Vector2(arrayPosition.x - 0.5f, arrayPosition.y - 0.86f);
                case Direction.Left:
                    return new Vector2(arrayPosition.x - 1, arrayPosition.y);
                case Direction.LeftUp:
                    return new Vector2(arrayPosition.x - 0.5f, arrayPosition.y + 0.86f);
                default:
                    return Vector2.zero;
            }
        }

        public void SetOppositeMovable(Direction direction, bool movable)
        {
            foreach (var item in edges)
            {
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