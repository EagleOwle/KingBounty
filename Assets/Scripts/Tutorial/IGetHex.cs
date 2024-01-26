using UnityEngine;


namespace HexWorld
{
    public interface IGetHex
    {
        bool GetHex(Vector2 position, out HexType cell);
    }
}
