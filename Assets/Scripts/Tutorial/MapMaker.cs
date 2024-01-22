using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetHex
{
    bool GetHex(Vector2 position, out HexType cell);
}

public class MapMaker : MonoBehaviour, IGetHex
{
    [SerializeField] private Vector2 mapSize;
    [SerializeField] private HexType hexPrefab;
    [SerializeField] private GameObject holder;
    [SerializeField] private float hexScale = 5;
    [SerializeField] private int masses = 15;
    [SerializeField] private Vector2 grow = new Vector2(4, 7);
    [SerializeField] private int groundFreq = 3;
    [SerializeField] private float dessertFreq = 0.8f;
    [SerializeField] private float grassFreq = 0.8f;

    [SerializeField] private List<HexType> cells = new List<HexType>();

    private void Start()
    {
        Generate();
    }

    public void DestroyCell()
    {
        for (int i = 0; i < cells.Count; i++)
        {
           DestroyImmediate(cells[i].gameObject);
        }

        cells.Clear();
    }

    public void Generate()
    {
        DestroyCell();
        GenerateGround();
        AddSnow();
        AddDesert();
        AddGrass();
        CalculateHexEdges();
    }

    public void GenerateGround()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var pos = new Vector2(x, y * 0.86f);

                if (y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                var cell = Instantiate(hexPrefab, new Vector3(pos.x * hexScale, 0, pos.y * hexScale), Quaternion.identity, holder.transform);
                cell.Initialise(this as IGetHex, hexScale, pos);
                cells.Add(cell);
            }
        }

        int massTemp = masses;
        int attampts = 0;

        while(massTemp > 0 && attampts < masses * 2)
        {
            attampts += 1;
            Vector2 pos = new Vector2(Mathf.Round(Random.value * mapSize.x - 1), Mathf.Round(Random.value * mapSize.y - 1));

            if (pos.y % 2 == 0)
            {
                pos.x += 0.5f;
            }

            pos.y *= 0.86f;

            if(GetHex(pos, out HexType newHex))
            {
                if(newHex.type == 0)
                {
                    newHex.SwapType(2);
                    newHex.grow = Mathf.RoundToInt(grow.x + Random.value * (grow.y - grow.x));
                    newHex.freq = groundFreq;
                    newHex.width = (int)mapSize.x;
                    newHex.Grow();
                    massTemp -= 1;
                }
            }

        }

        //Invoke(nameof(AddSnow), Time.deltaTime);
    }

    private void AddSnow()
    {
        int yMax = (int)mapSize.y / 10;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                Vector2 pos = new Vector2(x, y);

                if (pos.y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                pos.y *= 0.86f;

                HexType hex = null;
                if (GetHex(pos, out hex))
                {
                    if (hex.type > 0 && Random.value <= 1 - (float)y / yMax)
                    {
                        hex.type = 5;
                        hex.SwapType(5);
                    }
                }


                pos = new Vector2(x, mapSize.y - y - 1);

                if (pos.y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                pos.y *= 0.86f;

                hex = null;
                if (GetHex(pos, out hex))
                {
                    if (hex.type > 0 && Random.value <= 1 - (float)y / yMax)
                    {
                        hex.type = 5;
                        hex.SwapType(5);
                    }
                }


            }
        }

        // Invoke(nameof(AddDesert), Time.deltaTime);
    }

    private void AddDesert()
    {
        int yMin = (int)mapSize.y / 3;
        int height = (int)mapSize.y - yMin * 2;
        int offset = height / 2 + yMin;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = yMin; y < mapSize.y - yMin; y++)
            {
                Vector2 pos = new Vector2(x, y);

                if (pos.y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                pos.y *= 0.86f;

                HexType hex = null;
                if (GetHex(pos, out hex))
                {

                    float odds = dessertFreq * ( 1 / (1 + (y - offset) * (y - offset) / (float)height));

                    if (hex.type > 0 && Random.value <= odds)
                    {
                        hex.type = 3;
                        hex.SwapType(3);
                    }
                }  

            }
        }
    }

    private void AddGrass()
    {
        int yMin = (int)mapSize.y / 8;
        int yMax = Mathf.RoundToInt(mapSize.y / 3);
        int height = yMax - yMin;
        int offset = height / 2 + yMin;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                Vector2 pos = new Vector2(x, y);

                if (pos.y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                pos.y *= 0.86f;

                HexType hex = null;
                if (GetHex(pos, out hex))
                {
                    float odds = grassFreq * (1 / (1 + (y - offset) * (y - offset) / (float)height));

                    if (hex.type > 0 && Random.value <= odds)
                    {
                        hex.type = 1;
                        hex.SwapType(1);
                    }
                }


                pos = new Vector2(x, mapSize.y - y - 1);

                if (pos.y % 2 == 0)
                {
                    pos.x += 0.5f;
                }

                pos.y *= 0.86f;

                hex = null;
                if (GetHex(pos, out hex))
                {
                    float odds = grassFreq * (1 / (1 + (y - offset) * (y - offset) / (float)height));

                    if (hex.type > 0 && Random.value <= odds)
                    {
                        hex.type = 1;
                        hex.SwapType(1);
                    }
                }


            }
        }

        // Invoke(nameof(AddDesert), Time.deltaTime);
    }

    private void CalculateHexEdges()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].FindNeighbor();
        }
    }

    public bool GetHex(Vector2 position, out HexType cell )
    {
        cell = null;
        for (int i = 0; i < cells.Count; i++)
        {
            if(cells[i].arrayPosition == position)
            {
                cell = cells[i];
                return true;
            }
        }

        return false;
    }

}
