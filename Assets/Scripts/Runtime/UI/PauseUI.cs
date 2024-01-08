using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void ContinueGame()
    {
        Game.Manager.Player.Pause();
    }

    public void DeleteGame()
    {
        Game.Manager.DeleteGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
