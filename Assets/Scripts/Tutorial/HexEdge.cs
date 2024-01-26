using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexWorld
{
    public class HexEdge : MonoBehaviour
    {
        public Direction Direction => direction;
        [SerializeField] private Direction direction;

        public bool Movable
        {
            set
            {
                movable = value;
                //bridge.SetActive(movable);
                if (selfHex.type != 0)
                {
                    environment.SetActive(!movable);
                }
                else
                {
                    environment.SetActive(false);
                }
            }

            get
            {
                return movable;
            }
        }
        private bool movable;

        // [SerializeField] private GameObject bridge;
        [SerializeField] private GameObject environment;

        private HexType selfHex;
        private HexType neighbor;

        public void Initialise(HexType selfHex)
        {
            this.selfHex = selfHex;

        }

        public void SetNeighbor(HexType neighbor)
        {
            this.neighbor = neighbor;
            if (neighbor == null)
            {
                movable = false;
            }
            else
            {
                if (neighbor.type == 0)
                {
                    movable = true;
                }
                else
                {
                    movable = (Random.Range(0, 100) % 2 == 0) ? true : false;
                }
            }

            //bridge.SetActive(movable);

            if (selfHex.type != 0)
            {
                environment.SetActive(!movable);
            }
            else
            {
                environment.SetActive(false);
            }

            if (neighbor)
                neighbor.SetOppositeMovable(direction, movable);
        }
    }
}
