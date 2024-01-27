using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexWorld
{
    public class CellEdge    
    {
        public CellEdge(Direction direction)
        {
            this.direction = direction;
            enviromentType = (EnviromentType)Random.Range(0, 2);
        }

        public void Initialise(Cell selfCell)
        {
            this.selfCell = selfCell;
        }

        public Direction Direction => direction;
        private Direction direction;

        public bool Movable
        {
            set
            {
                movable = value;
            }

            get
            {
                return movable;
            }
        }
        private bool movable;

        private Cell selfCell;
        private Cell neighbor;

        public EnviromentType EnviromentType => enviromentType;
        private EnviromentType enviromentType;

        public void SetNeighbor(Cell neighbor)
        {
            this.neighbor = neighbor;
            if (neighbor == null)
            {
                movable = false;
            }
            else
            {
                if (neighbor.Type.Name == "Blue")
                {
                    movable = true;
                }
                else
                {
                    movable = (Random.Range(0, 100) % 2 == 0) ? true : false;
                }
            }

            if (neighbor != null)
                neighbor.SetOppositeMovable(direction, movable);
        }

    }
}
