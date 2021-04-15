using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int currentPlayerID;

    public static bool gameOver = false;
    public static bool draw = false;

    public static int yellowScore = 0;
    public static int redScore = 0;


    public List<IndividualPlay> allPlays = new List<IndividualPlay>();

    private void Awake()
    {
        instance = this;
    }

    public void Replay()
    {
        gameOver = false;
        draw = false;
        UIManager.instance.stop = false;
        LevelHandler.instance.SetupGrid();
        LevelHandler.instance.pawnsCount = 0;
    }


    public void AddPlay(IndividualPlay play)
    {
        allPlays.Add(play);
    }

    public void SwitchPlayer()
    {
        currentPlayerID = currentPlayerID == 1 ? 0 : 1;
        Player.instance.playerID = currentPlayerID;
        //Player.instance.canPlay = !Player.instance.canPlay;
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public void Draw()
    {
        draw = true;
    }
}