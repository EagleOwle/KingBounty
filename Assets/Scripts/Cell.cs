using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
   [SerializeField] private SpriteRenderer render;

    public Color SetColor
    {
        set
        {
            render.color = value;
        }
    }

}
