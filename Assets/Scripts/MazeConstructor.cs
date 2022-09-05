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

    public float hallWidth { get; private set; }
    public float hallHeight { get; private set; }
    public int startRow { get; private set; }
    public int startCol { get; private set; }
    public int goalRow { get; private set; }
    public int goalCol { get; private set; }

    public int[,] data { get; private set; }

    private void Awake()
    {

        _dataGenerator = new MazeDataGenerator();
        _meshGenerator = new MazeMeshGenerator();
        
    }

    public void GenerateNewMaze(int sizeRow, int sizeCols, TriggerEventHandler startCallback = null, TriggerEventHandler goalCallback = null)
    {
        DisposeOldMaze();

        data = _dataGenerator.FromDimensions(sizeRow, sizeCols);

       FindStartPosition();
       FindGoalPosition();

        hallWidth = _meshGenerator.width;
        hallHeight = _meshGenerator.height;

        DisplayMaze();

        PlaceStartTrigger(startCallback);
        PlaceGoalTrigger(goalCallback);
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

    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    private void FindStartPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; i <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    startRow = i;
                    startCol = j;
                    return;
                }
            }
        }
        
    }

    private void FindGoalPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = cMax; i >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    goalRow = i;
                    goalCol = j;
                    return;
                }
            }
        }

    }

    private void PlaceStartTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(startRow * hallWidth, 0.5f, startCol * hallWidth);
        go.name = "StartTrigger";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = _materialStart;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }

    private void PlaceGoalTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(goalRow * hallWidth, 0.5f, goalCol * hallWidth);
        go.name = "GoalTrigger";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = _materialTreasure;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }
}
