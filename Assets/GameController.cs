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

    private float chance2Win = 1.0f;
    private static int difficulty = 0;

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        gameOverPanel.SetActive(false);
        moveCount = 0;

        difficulty = PlayerPrefs.GetInt("Difficulty");
        switch (difficulty) {
        	case 0: 
        		chance2Win = 0.2f;
        		break;
        	case 1:
        		chance2Win = 0.6f;
        		break;
        	case 2:
        		chance2Win = 1.0f;
        		break;
        	default:
        		chance2Win = 1.0f;
        	break;
        }
        if (ModeData.Mode == 2)
        {
            onlyAI();
        }
    }
    public void onlyAI()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        while (moveCount <= 9)
        {
            AIturn();
        }
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


        //difficulty
        int index = 0;
        float randomN = Random.value;
        if (randomN < chance2Win) {
        	index = minimax(newB, playerSide,0);
    	} else {
    		int[] spots = availSpots(newB);

    		int i = Random.Range(0, spots.Length);
    		index = spots[i];
    	}

    	Debug.Log("Random:" + randomN.ToString());
    	Debug.Log("Chance 2 win:" + chance2Win.ToString());
        Debug.Log(index);
        buttonList[index].text = playerSide;
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

    int minimax(string[] newBoard, string player, int depth)
    {

        int[] spots = availSpots(newBoard);

        if (checkWin(newBoard, human))
        {
            return depth - 10;

        }
        else if (checkWin(newBoard, cpu))
        {
            return 10 - depth;
        }
        else if (spots.Length == 0)
        {
            return 0;
        }
        depth++;
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
                result = minimax(newBoard, human, depth);
                //Debug.Log(result);
                moves.Add(new List<int>());
                moves[i].Add(spots[i]);
                moves[i].Add(result);
            }
            else
            {
                result = minimax(newBoard, cpu, depth);
                //Debug.Log(result);
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
