using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace HexWorld
{
    [CreateAssetMenu()]
    public class GameField : ScriptableObject
    {
        //#region Singleton
        //public static GameField Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = Resources.Load<GameField>("GameField") as GameField;
        //        }

        //        return instance;
        //    }
        //}
        //private static GameField instance;
        //#endregion

        //public static Dictionary<Vector2, Cell> Cells => Instance.cells;
        //private Dictionary<Vector2, Cell> cells = new Dictionary<Vector2, Cell>();

        public  Cell[,] Cells => cells;
        private Cell[,] cells = new Cell[0, 0];

        public void CreateNewField(Vector2Int fieldSize)
        {
            cells = new Cell[fieldSize.x, fieldSize.y];
        }

        public static void AddCell(Cell cell)
        {
            //if (Instance.cells.ContainsKey(cell.FieldPosition)) return;
            //    Instance.cells.Add(cell.FieldPosition, cell);
        }

        public void AddCell(Cell cell, Vector2Int index)
        {
            //if (Instance.cells.ContainsKey(cell.FieldPosition)) return;
            //    Instance.cells.Add(cell.FieldPosition, cell);

            cells[index.x,index.y] = cell;
        }

        public void ClearAllCells()
        {
            cells = new Cell[0, 0];
        }

    }
}