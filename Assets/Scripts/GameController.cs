using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    private MazeConstructor _constructor;

    private void Start()
    {
        _constructor = GetComponent<MazeConstructor>();

        _constructor.GenerateNewMaze(9, 9);
        
    }


}
