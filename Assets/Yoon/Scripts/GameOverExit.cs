using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverExit : MonoBehaviour
{
    public void GoMainMenu() => SceneManager.LoadScene("MainMenu");
    public void ExitGame() => Application.Quit();
}
