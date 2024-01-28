using System.Collections;
using UnityEngine;
using SimplePool;
using System.Collections.Generic;

namespace HexWorld
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private GameField gameField;
        [SerializeField] private new Camera camera;
        [SerializeField] private GameFieldGenerator generator;
        [SerializeField] private bool drawGizmos = false;
        [SerializeField] private float viewDistance = 50;
        [SerializeField] private float hexSize = 10;
        
        private Dictionary<Vector3, PoolComponent> hexes = new Dictionary<Vector3, PoolComponent>();

        [SerializeField] private float screenPositionOffset = 0.1f;

        Vector3 cellPosition;
        Vector3 lastCamPosition;
        Vector3 currentCamPosition;
        PoolComponent hex;

        private void Start()
        {
            generator.CreateNewField();
            FindFovCells();
        }


        private void LateUpdate()
        {
            //currentCamPosition = RoundToNearest(camera.transform.position, 1);

            //if (currentCamPosition != lastCamPosition)
            {
                FindFovCells();
            }
        }

        private void FindFovCells()
        {
            for (int x = 0; x < gameField.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameField.Cells.GetLength(1); y++)
                {
                    cellPosition = new Vector3(gameField.Cells[x, y].FieldPosition.x, 0, gameField.Cells[x, y].FieldPosition.y);
                    if (IsInCameraView(cellPosition * hexSize))
                    {
                       if (hexes.ContainsKey(cellPosition) == false)
                        {
                            hex = Pool.Instance.GetObject("Hex", cellPosition * hexSize, Quaternion.identity);
                            hex.transform.localScale = Vector3.one * hexSize;
                            hexes.Add(cellPosition, hex);
                            hex.GetComponent<HexBase>().Show(gameField.Cells[x, y].Type, gameField.Cells[x, y].Edges);
                        }
                    }
                    else
                    {
                       if (hexes.TryGetValue(cellPosition, out PoolComponent hex))
                        {
                            hex.ReturnToPool();
                            hexes.Remove(cellPosition);
                            
                        }
                    }

                }
            }
        }

        private bool IsInCameraView(Vector3 targetPosition)
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(targetPosition);
            if (screenPoint.z + screenPositionOffset > 0 && 
                screenPoint.x + screenPositionOffset > 0 && 
                screenPoint.x - screenPositionOffset < 1 && 
                screenPoint.y + screenPositionOffset > 0 && 
                screenPoint.y - screenPositionOffset < 1)
            {
                if (Mathf.Abs(targetPosition.z - camera.transform.position.z) < viewDistance)
                {
                    return true;
                }
            }

            return false;
        }

        private float RoundToNearest(float value, int neares = 10)
        {
           return Mathf.Round(value / neares) * neares;
        }

        private Vector3 RoundToNearest(Vector3 value, int neares = 10)
        {
            return new Vector3(Mathf.Round(value.x / neares) * neares,
                               Mathf.Round(value.y / neares) * neares,
                               Mathf.Round(value.z / neares) * neares);
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos == false) return;

            for (int x = 0; x < gameField.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameField.Cells.GetLength(1); y++)
                {
                    cellPosition = new Vector3(gameField.Cells[x,y].FieldPosition.x, 0, gameField.Cells[x, y].FieldPosition.y);

                    Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.4f * hexSize, 6, gameField.Cells[x, y].Type.color);

                    if (IsInCameraView(cellPosition * hexSize))
                    {
                        Debug.DrawLine(cellPosition, cellPosition + Vector3.up, Color.white);
                        Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.5f * hexSize, 6, Color.white);
                    }
                    else
                    {
                        Debug.DrawLine(cellPosition, cellPosition + Vector3.up, Color.red);
                        Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.5f * hexSize, 6, Color.red);
                    }
                }
            }
        }

    }
}