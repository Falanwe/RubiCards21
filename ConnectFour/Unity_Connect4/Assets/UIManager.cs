using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject wonPanel;
    public Text wonText;

    public Text yellowScore;
    public Text redScore;

    [HideInInspector] public bool stop = false;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stop == false)
        if(GameManager.gameOver)
        {
            wonPanel.SetActive(true);
            wonText.text = (GameManager.instance.currentPlayerID == 1 ? "Red" : "Yellow") + " won !";
            wonText.color = GameManager.instance.currentPlayerID == 1 ? Color.red : Color.yellow;

            if (GameManager.instance.currentPlayerID == 0) GameManager.yellowScore++;
            else GameManager.redScore++;

            redScore.text = GameManager.redScore.ToString();
            yellowScore.text = GameManager.yellowScore.ToString();
            stop = true;
        }
        else if (GameManager.draw)
        {
            wonPanel.SetActive(true);
            wonText.text = "DRAW !";
            wonText.color = Color.blue;
            stop = true;
        }
    }
}
