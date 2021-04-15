using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class LevelHandler : MonoBehaviour
{
    public GameObject emptyCell;

    public Sprite redPawn;
    public Sprite yellowPawn;

    public Cell[] cells;
    public List<Cell> cellsToCheck = new List<Cell>();

    public List<IndividualPlay> test = new List<IndividualPlay>();

    public int pawnsCount = 0;

    public int height = 6;
    public int length = 7;
    public int size;

    [Range(1,7)]
    public int matchToReach = 4;

    public static LevelHandler instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupGrid();
        test.Add(new IndividualPlay(0, 4));
        test.Add(new IndividualPlay(1, 6));
        GetMissingPlays(test);

        InvokeRepeating("GetGameState", 0f, 0.5f);
        InvokeRepeating("GetPlays", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pawnsCount >= size) GameManager.instance.Draw();
    }

    public void SetupGrid()
    {
        ClearGrid();

        size = length * height;
        cells = new Cell[size]; // Init grille

        for (int i = 0; i < (size); i++)
        {
            GameObject cellVisual = Instantiate(emptyCell, new Vector2(i % length + 0.5f, i/length + 0.5f), Quaternion.identity);
            Cell c = new Cell();
            cells[i] = c;
            c.index = i;
            c.value = CellValue.Empty;
            c.visual = cellVisual;

            cellsToCheck.Add(c);

            c.checker.Add(1, Init1(i));
            c.checker.Add(6, Init6(i));
            c.checker.Add(7, Init7(i));
            c.checker.Add(8, Init8(i));

            if (c.isDead) cellsToCheck.Remove(c);

        }

        Camera.main.transform.position = cells[17].visual.transform.position.ChangeZ(-10).OffsetY(0.5f);
    }
    public void ClearGrid()
    {
        foreach(Cell c in cells)
        {
            Destroy(c.visual);
        }

        cells = new Cell[0];
        cellsToCheck.Clear();
    }
    public bool Init1(int index)
    {
        int getHeight = (index/length) + 1;

        if (((float)(index + matchToReach) / (float)(length * getHeight)) <= 1f)
            return true;
        else return false;
    }
    public bool Init6(int index)
    {
        int getHeight = (index/length) + 1;

        if ((index + ((length - 1) * (matchToReach - 1))) / length + 1 >= getHeight + (matchToReach - 1) && (index + ((length - 1) * (matchToReach - 1))) < size) return true;
        else return false;

    }
    public bool Init7(int index)
    {
        if (index + (length*(matchToReach-1)) < size) return true;
        else return false;
    }
    public bool Init8(int index)
    {
        int getHeight = (index / length) + 1;

        if ((index + ((length + 1) * (matchToReach - 1))) / length + 1 == getHeight + (matchToReach - 1) && (index + ((length + 1) * (matchToReach - 1))) < size) return true;
        else return false;
    }

    public void DropPawn(int column, int playerID, bool bypass = false)
    {

        if (GameManager.gameOver == false && (Player.instance.canPlay || bypass))
        {
            column -= 1;
            for (int i = 0; i < height; i++)
            {
                Cell c = cells[column + (i * length)];
                if (c.value == CellValue.Empty)
                {
                    c.value = GetCellValueFromPlayerID(playerID);
                    c.visual.GetComponent<SpriteRenderer>().sprite = GetSpriteFromPlayerID(playerID);
                    GameManager.instance.AddPlay(new IndividualPlay(column, playerID));
                    CellsCheck();
                    GameManager.instance.SwitchPlayer();
                    pawnsCount++;
                    return;
                }
            }
        }
    }

    public void DropPawn(int column)
    {
        int playerID = GameManager.instance.currentPlayerID;

        if (GameManager.gameOver == false && Player.instance.canPlay)
        {
            column -= 1;
            for (int i = 0; i < height; i++)
            {
                Cell c = cells[column + (i * length)];
                if (c.value == CellValue.Empty)
                {
                    c.value = GetCellValueFromPlayerID(playerID);
                    c.visual.GetComponent<SpriteRenderer>().sprite = GetSpriteFromPlayerID(playerID);
                    GameManager.instance.AddPlay(new IndividualPlay(column, playerID));
                    CellsCheck();
                    GameManager.instance.SwitchPlayer();
                    pawnsCount++;
                    return;
                }
            }
        }
    } //For buttons

    public void GetMissingPlays(List<IndividualPlay> plays)
    {
        int count = GameManager.instance.allPlays.Count;

        for (int i = count; i < plays.Count; i++)
        {            
            DropPawn(plays[i].Column, plays[i].PlayerId, bypass: true);
            GameManager.instance.allPlays.Add(plays[i]);
        }

    }
    public void CellsCheck()
    {
        foreach(Cell c in cellsToCheck.ToList())
        {
            if(GameManager.gameOver == false && c.value != CellValue.Empty)
            {
                c.CheckDirections();
                c.DeadCellCheck();
            }
        }
    }


    public Sprite GetSpriteFromPlayerID(int ID)
    {
        return ID == 1 ? yellowPawn : redPawn;
    }

    public CellValue GetCellValueFromPlayerID(int ID)
    {
        return ID == 1 ? CellValue.Yellow : CellValue.Red;
    }

    public async Task<GameForm> GetGameForm()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/ConnectFour/game");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GameForm>(json);
    }

    public async Task<GameState> GetGameState()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/ConnectFour/{gameId}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GameState>(json);
    }

    public async Task<List<IndividualPlay>> GetPlays()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/ConnectFour/{gameId}/result");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<IndividualPlay>>(json);
    }
}

[System.Serializable]
public class Cell
{
    public bool isDead;
    public GameObject visual;
    public CellValue value = CellValue.Empty;
    public int index;
    public Dictionary<int, bool> checker = new Dictionary<int, bool>();



    public void DeadCellCheck()
    {
        foreach(bool b in checker.Values)
        {
            if (b == true) return; // Si on peut encore faire un match4 avec cette case on ne la kill pas
        }

        LevelHandler.instance.cellsToCheck.Remove(this);
        isDead = true;
    }

    public void CheckDirections()
    {
        LevelHandler lh = LevelHandler.instance;

        int score = 1;

        foreach(int dir in checker.Keys.ToList())
        {
            score = 1;
            if(checker[dir] == true)
            {
                switch(dir)
                {
                    case 1:
                        Check(1, dir, ref score);
                        break;
                    case 6:
                        Check(lh.length - 1, dir, ref score);
                        break;
                    case 7:
                        Check(lh.length, dir, ref score);
                        break;
                    case 8:
                        Check(lh.length + 1, dir, ref score);
                        break;
                }
            }

            if (score == lh.matchToReach)
            {
                GameManager.instance.GameOver();
                return;
            }
        }

    }

    public void Check(int range, int dir, ref int score)
    {
        LevelHandler lh = LevelHandler.instance;
        for (int i = 1; i < lh.matchToReach; i++)
        {
            if (lh.cells[index + (i * range)].value == this.value) score++;
            else if (lh.cells[index + (i * range)].value == CellValue.Empty)
            {
                continue;
            }
            else
            {
                checker[dir] = false;
            }
        }
    }

}
public enum CellValue {Empty, Yellow, Red}




public struct GameForm
{
    public int GameId;
    public int PlayerId;
}

public struct IndividualPlay
{
    public IndividualPlay(int playerId, int column)
    {
        PlayerId = playerId;
        Column = column;
    }

    public int PlayerId { get; }
    public int Column { get; }
}
public enum GameState
{
    NoOnePlaying,
    PlayerOnePlaying,
    PlayerTwoPlaying,
    PlayerOneWon,
    PlayerTwoWon,
    Tie
}
