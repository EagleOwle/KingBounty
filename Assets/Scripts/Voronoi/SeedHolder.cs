using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Voronoi
{
    [System.Serializable]
    public class SeedHolder
    {
        public SeedHolder(Vector2 position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        public Vector2 position;
        public Color color;
        public List<Vector2> cellPosition = new List<Vector2>();
    }
}