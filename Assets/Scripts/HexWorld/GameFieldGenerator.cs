using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexWorld
{
    [CreateAssetMenu()]
    public class GameFieldGenerator : ScriptableObject
    {
        public CellTypeData cellTypeData;
        public GameField gameField;
        
        public Vector2Int FieldSize;
        //public float CellSize = 1;
        [Header("Количество стровов")]
        public int masses = 15;
        [Header("Площадь островов")]
        public Vector2Int grow = new Vector2Int(4, 7);
        [Header("Частота земли")]
        public int groundFreq = 3;
        [Header("Частота пустыни")]
        public float dessertFreq = 0.8f;
        [Header("Частота травы")]
        public float grassFreq = 0.8f;

        public void CreateNewField()
        {
            gameField.CreateNewField(FieldSize);

            for (int x = 0; x < FieldSize.x; x++)
            {
                for (int y = 0; y < FieldSize.y; y++)
                {
                    var pos = new Vector2(x, y * 0.86f);

                    if (y % 2 == 0)
                    {
                        pos.x += 0.5f;
                    }

                    var cell = new Cell(new Vector2(pos.x, pos.y), cellTypeData.CellTypes[0], gameField);
                    gameField.AddCell(cell, new Vector2Int(x,y));
                }
            }

            
            GenerateGround();
            FindAllNeighbor();
        }

        public void GenerateGround()
        {
            int massTemp = masses;
            int attampts = 0;

            while (massTemp > 0 && attampts < masses * 2)
            {
                attampts += 1;
                Vector2 pos = new Vector2(Mathf.Round(Random.value * FieldSize.x - 1), Mathf.Round(Random.value * FieldSize.y - 1));

                if (pos.y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                pos.y *= 0.86f;

                if (gameField.GetHex(pos, out Cell newCell))
                {
                    if (newCell.Type == cellTypeData.CellTypes[0])
                    {
                        newCell.SwapType(cellTypeData.CellTypes[2]);
                        int newGrow = Mathf.RoundToInt(grow.x + Random.value * (grow.y - grow.x));
                        int freq = groundFreq;
                        int width = FieldSize.x;

                        SetCellGrow(newCell, newGrow, freq, width, newCell.Type);
                        massTemp -= 1;
                    }
                }

            }
        }

        public void ClearField()
        {
            gameField.ClearAllCells();
        }

        private void FindAllNeighbor()
        {
            for (int x = 0; x < gameField.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameField.Cells.GetLength(1); y++)
                {
                    gameField.Cells[x, y].FindNeighbor();
                }
            }
        }

        private List<Vector2> NeighborCoordinate(Vector2 arrayPosition)
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

        public void SetCellGrow(Cell cell, int grow, int freq, int width, CellType type)
        {
            List<Vector2> coord = NeighborCoordinate(cell.FieldPosition);

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

                if (gameField.GetHex(coord[m], out Cell newCell))
                {
                    if (newCell.Type == cellTypeData.CellTypes[0])
                    {
                        newCell.SwapType(type);

                        if (grow - 1 > 0)
                        {
                            SetCellGrow(newCell, grow - 1, freq, width, type);
                        }
                    }
                }

            }

        }

    }
}
