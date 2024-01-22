using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Voronoi
{
    public class VoronoiDiagram : MonoBehaviour
    {
        public int seedsCount = 5;
        public int mapSize = 50;
        public int minDistance = 10;
        public GameObject seedPrefab;

        [SerializeField] private List<GameObject> cells = new List<GameObject>();
        [SerializeField] private List<SeedHolder> holders = new List<SeedHolder>();

        public void Generate()
        {
            DestroyCells();
            GenerateSeeds();
            GenerateVoronoiDiagram();
            VisualizeVoronoiDiagram();
        }

        public void Clear()
        {
            DestroyCells();
        }

        private void GenerateSeeds()
        {
            for (int i = 0; i < seedsCount; i++)
            {
                holders.Add(new SeedHolder(GetSeedPosition(), Random.ColorHSV()));
            }
        }

        private Vector2 GetSeedPosition()
        {
            float x = 0;
            float y = 0;
            int iteration = 100;
            bool end = false;

            while (end == false)
            {
                end = true;
                x = Random.Range(0, mapSize);
                y = Random.Range(0, mapSize);
                iteration--;

                if (x < minDistance || x > mapSize - minDistance ||
                    y < minDistance || y > mapSize - minDistance)
                {
                    end = false;
                }

                foreach (var item in holders)
                {
                    if (Vector2.Distance(new Vector2(x, y), item.position) < minDistance)
                    {
                        end = false;
                    }
                }

                if (iteration <= 0)
                {
                    Debug.Log("Iteration end");
                    break;
                }

            }

            return new Vector2(x, y);

        }

        private void GenerateVoronoiDiagram()
        {
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    Vector2 point = new Vector2(x, y);
                    int closestSeedIndex = FindClosestSeedIndex(point, holders);
                    holders[closestSeedIndex].cellPosition.Add(point);

                }
            }
        }

        private int FindClosestSeedIndex(Vector2 point, List<SeedHolder> holders)
        {
            int closestIndex = 0;
            float closestDistance = Vector2.Distance(point, holders[0].position);

            for (int i = 1; i < holders.Count; i++)
            {
                float distance = Vector2.Distance(point, holders[i].position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        private void VisualizeVoronoiDiagram()
        {
            for (int i = 0; i < holders.Count; i++)
            {
                foreach (Vector2 point in holders[i].cellPosition)
                {
                    Vector3 position = new Vector3(point.x, 0, point.y);
                    GameObject sphere = Instantiate(seedPrefab, position, Quaternion.identity, transform);
                    sphere.GetComponentInChildren<SpriteRenderer>().color = holders[i].color;
                    cells.Add(sphere);
                }
            }
        }

        public void DestroyCells()
        {
            if (cells == null)
            {
                return;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i] != null)
                {
                    DestroyImmediate(cells[i].gameObject);
                }
            }

            cells.Clear();
            holders.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < holders.Count; i++)
            {
                Debug.DrawLine(new Vector3(holders[i].position.x, 0, holders[i].position.y), new Vector3(holders[i].position.x, 1, holders[i].position.y));
            }
        }


    }
}
