using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetOnClick : MonoBehaviour {

    public Button button;
    public Text butonText;
    public string playerSide;

    private GameController gameController;

	public void setText()
    {
        butonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }
    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }
}
