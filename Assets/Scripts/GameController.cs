using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    [SerializeField] private FpsMovement player;
    [SerializeField] private Text timeLabel;
    [SerializeField] private Text scoreLabel;

    private MazeConstructor generator;

    private DateTime startTime;
    private int timeLimit;
    private int reduceLimitBy;
    private int diffNumber = 11;

    private int score;
    private bool goalReached;

    void Start()
    {
        generator = GetComponent<MazeConstructor>();

       // StartNewGame();
    }

    public void StartNewGame()
    {
        timeLimit = 85;
        reduceLimitBy = 5;
        startTime = DateTime.Now;

        score = 0;
        scoreLabel.text = score.ToString();

        StartNewMaze();
    }

    private Vector3 GetStartPosition()
    {
        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        Vector3 startPosition = new Vector3(x, y, z);
        return  startPosition;
    }

    private void StartNewMaze()
    {
        generator.GenerateNewMaze(diffNumber, diffNumber, OnStartTrigger, OnGoalTrigger);

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = GetStartPosition();
        Debug.Log("Position changed");
        player.GetComponent<CharacterController>().enabled = true;

        goalReached = false;
        player.enabled = true;

        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;
    }

    void Update()
    {
        if (!player.enabled)
        {
            return;
        }

        int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        int timeLeft = timeLimit - timeUsed;

        if (timeLeft > 0)
        {
            timeLabel.text = timeLeft.ToString();
        }
        else
        {
            timeLabel.text = "TIME UP";
            player.enabled = false;

            Invoke("StartNewGame", 4);
        }
    }

    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("Goal!");
        goalReached = true;

        score += 1;
        scoreLabel.text = score.ToString();

        Destroy(trigger);
    }

    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if (goalReached)
        {
            Debug.Log("Finish!");
            player.enabled = false;

            Invoke("StartNewMaze", 4);
        }
    }
    public void EasyDiff()
    {
        diffNumber = 11;
    }
    public void MediumDiff()
    {
        diffNumber = 17;
    }
    public void HardDiff()
    {
        diffNumber = 25;
    }
}
