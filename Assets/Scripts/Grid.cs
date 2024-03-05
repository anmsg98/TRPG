using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance { get; set; }

    public Vector2Int currentLoc;
    void Awake()
    {
        instance = this;
    }
}
