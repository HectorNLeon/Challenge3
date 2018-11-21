using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;




public class GameController : MonoBehaviour
{

    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int moveCount;
    private const string human = "X";
    private const string cpu = "O";

    private string playerSide;

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        gameOverPanel.SetActive(false);
        moveCount = 0;
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<SetOnClick>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void AIturn()
    {
        List<string> tempB = new List<string>();
        for(int i=0; i<buttonList.Length; i++)
        {
            tempB.Add(buttonList[i].text);
        }
        string[] newB = tempB.ToArray();
        int index = minimax(newB, playerSide);
        Debug.Log(index);
        buttonList[index].text = "O";
        buttonList[index].GetComponentInParent<Button>().interactable = false;
        EndTurn();
    }
    public bool checkWin(string[] bList, string player)
    {
        if (
            (bList[0] == player && bList[1] == player && bList[2] == player) ||
            (bList[3] == player && bList[4] == player && bList[5] == player) ||
            (bList[6] == player && bList[7] == player && bList[8] == player) ||
            (bList[0] == player && bList[3] == player && bList[6] == player) ||
            (bList[1] == player && bList[4] == player && bList[7] == player) ||
            (bList[2] == player && bList[5] == player && bList[8] == player) ||
            (bList[0] == player && bList[4] == player && bList[8] == player) ||
            (bList[2] == player && bList[4] == player && bList[6] == player))
        {
            return true;
        }
        return false;
    }
    public void EndTurn()
    {

        moveCount++;

        if (
             (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) ||
             (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) ||
             (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) ||
             (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) ||
             (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) ||
             (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) ||
             (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) ||
             (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide))
        {
            GameOver();
        }
        else if (moveCount >= 9)
        {
            SetGameOverText("It's a draw!");
        }

        ChangeSides();

    }
    int [] availSpots(string[] bList)
    {
        List<int> arrayL = new List<int>();
        for (int i = 0; i < bList.Length; i++)
        {
            if(bList[i] == "")
            {
                arrayL.Add(i);
            }
        }
        int[] array = arrayL.ToArray();
        return array;
    }
    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void GameOver()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {   
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        SetGameOverText(playerSide + " Wins!");
    }
    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    int minimax(string[] newBoard, string player)
    {
        
        int[] spots = availSpots(newBoard);
        
        if (checkWin(newBoard, human))
        {
            return -10;
        }
        else if (checkWin(newBoard, cpu))
        {
            return 10;
        }
        else if (spots.Length == 0)
        {
            return 0;
        }
        int result;
        string mindex;
        List<List<int>> moves = new List<List<int>>();
        for (int i = 0; i < spots.Length; i++)
        {
            //Debug.Log(newBoard[0] + " " + newBoard[1] + " " + newBoard[2] + "\n" + newBoard[3] + " " + newBoard[4] + " " + newBoard[5] + "\n" + newBoard[6]+" "+newBoard[7]+" " + newBoard[8]);
            //List<string> tempB = new List<string>();
            //for (int h = 0; h < newBoard.Length; h++)
            //{
            //   tempB.Add(newBoard[h]);
            //}
            //string[] tempBoard = tempB.ToArray();
            mindex = newBoard[spots[i]];
            newBoard[spots[i]] = player;
            if (player == cpu)
            {
                result = minimax(newBoard, human);
                moves.Add(new List<int>());
                moves[i].Add(spots[i]);
                moves[i].Add(result);
            }
            else
            {
                result = minimax(newBoard, cpu);
                moves.Add(new List<int>());
                moves[i].Add(spots[i]);
                moves[i].Add(result);
            }
            newBoard[spots[i]] = mindex;

        }
        int bestmove = 0;
        if (player == cpu)
        {
            int bestScore = -10000;
            for (int i = 0; i < moves.Count; i++)
            {
                
                if (moves[i][1] > bestScore)
                {
                    bestScore = moves[i][1];
                    bestmove = moves[i][0];
                }
            }
        }
        else
        {
            int bestScore = 10000;
            for (int i = 0; i < moves.Count; i++)
            {
                
                if (moves[i][1] < bestScore)
                {
                    bestScore = moves[i][1];
                    bestmove = moves[i][0];
                }
            }
        }
        return bestmove;
    }
}
