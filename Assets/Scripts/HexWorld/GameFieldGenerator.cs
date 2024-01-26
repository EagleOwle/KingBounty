using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexWorld
{
    [CreateAssetMenu()]
    public class GameFieldGenerator : ScriptableObject
    {
        public GameField gameField;
        public Vector2Int FieldSize;
        public float CellSize = 1;

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

                    var cell = new Cell(new Vector2(pos.x, pos.y), CellSize);
                    gameField.AddCell(cell, new Vector2Int(x,y));
                }
            }
        }

        public void ClearField()
        {
            gameField.ClearAllCells();
        }

    }
}
