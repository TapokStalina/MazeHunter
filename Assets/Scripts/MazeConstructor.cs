using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    [SerializeField] private Material _mazeMaterial1;
    [SerializeField] private Material _mazeMaterial2;
    [SerializeField] private Material _materialTreasure;
    [SerializeField] private Material _materialStart;

    private MazeDataGenerator _dataGenerator;
    private MazeMeshGenerator _meshGenerator;

    public int[,] data { get; private set; }

    private void Awake()
    {
        data = new int[,]
        {
            {1, 1 , 1 },
            {1, 0 , 1 },
            {1, 1 , 1 }

        };

        _dataGenerator = new MazeDataGenerator();
        _meshGenerator = new MazeMeshGenerator();
    }

    public void GenerateNewMaze(int sizeRow, int sizeCols)
    {
        data = _dataGenerator.FromDimensions(sizeRow, sizeCols);
        DisplayMaze();
    }

    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = _meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { _mazeMaterial1, _mazeMaterial2 };
    }
}
