using System.Collections;
using UnityEngine;

namespace HexWorld
{
    [System.Serializable]
    public class Cell
    {
        public Vector2 FieldPosition
        {
            get
            {
                return fieldPosition * size;
            }
        }
        //public Vector2 FieldPosition => fieldPosition;
        private Vector2 fieldPosition;

        public float Size => size;
        private float size;

        public Cell(Vector2 fieldPosition, float size)
        {
            this.fieldPosition = fieldPosition;
            this.size = size;
        }


    }
}