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
        private Dictionary<Vector3, PoolComponent> hexes = new Dictionary<Vector3, PoolComponent>();

        Vector3 cellPosition;
        Vector3 lastCamPosition;
        Vector3 currentCamPosition;
        PoolComponent hex;

        private void Start()
        {
            generator.CreateNewField();
            FindFovCells();
        }


        private void Update()
        {
            currentCamPosition = new Vector3(Mathf.Round(camera.transform.position.x / 10) * 10,
                                             0,
                                             Mathf.Round(camera.transform.position.z / 10) * 10);

            if (currentCamPosition != lastCamPosition)
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
                    if (IsInCameraView(cellPosition))
                    {
                       if (hexes.ContainsKey(cellPosition) == false)
                        {
                            hex = Pool.Instance.GetObject("Hex", cellPosition, Quaternion.identity);
                            hex.transform.localScale = Vector3.one * gameField.Cells[x, y].Size;
                            hexes.Add(cellPosition, hex);
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
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                if (Mathf.Abs(targetPosition.z - camera.transform.position.z) < viewDistance)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos == false) return;

            for (int x = 0; x < gameField.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameField.Cells.GetLength(1); y++)
                {
                    cellPosition = new Vector3(gameField.Cells[x,y].FieldPosition.x, 0, gameField.Cells[x, y].FieldPosition.y);
                    if (IsInCameraView(cellPosition))
                    {
                        Debug.DrawLine(cellPosition, cellPosition + Vector3.up, Color.white);
                        Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.5f * gameField.Cells[x, y].Size, 6, Color.white);
                    }
                    else
                    {
                        Debug.DrawLine(cellPosition, cellPosition + Vector3.up, Color.red);
                        Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.5f * gameField.Cells[x, y].Size, 6, Color.red);
                    }
                }
            }

            //foreach (var item in GameField.Cells)
            //{
            //    cellPosition = new Vector3(item.Value.FieldPosition.x, 0, item.Value.FieldPosition.y);
            //    if (IsInCameraView(cellPosition))
            //    {
            //        Debug.DrawLine(cellPosition, cellPosition + Vector3.up, Color.white);
            //        Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.5f * item.Value.Size, 6, Color.white);
            //    }
            //    else
            //    {
            //        Debug.DrawLine(cellPosition, cellPosition + Vector3.up, Color.red);
            //        Debug.DrawCircle(cellPosition, Quaternion.Euler(90, 90, 0), 0.5f * item.Value.Size, 6, Color.red);
            //    }

            //}
        }

    }
}