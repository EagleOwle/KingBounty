using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEdge : MonoBehaviour
{
    public Direction direction;

    [SerializeField] private GameObject mount;
    [SerializeField] private GameObject forest;

    //public bool Movable
    //{
    //    set
    //    {
    //        movable = value;
    //        environment.SetActive(!movable);
    //    }

    //    get
    //    {
    //        return movable;
    //    }
    //}
    //private bool movable;

    public void Show(bool movable, HexWorld.EnviromentType type)
    {
        mount.SetActive(false);
        forest.SetActive(false);

        if (movable == false)
        {
            switch (type)
            {
                case HexWorld.EnviromentType.Mount:
                    mount.SetActive(true);
                    break;
                case HexWorld.EnviromentType.Forest:
                    forest.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    //[System.Serializable]
    //public class Enviroment
    //{
    //    public HexWorld.EnviromentType type;
    //    public GameObject EnviromentObject;
    //}

}
