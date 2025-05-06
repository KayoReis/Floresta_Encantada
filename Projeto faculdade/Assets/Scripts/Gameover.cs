using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameController gameController;

    public void Reset()
    {
        gameController.Endgame.gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameController.TotalTries = 1;
        gameController.skipAddTries = true;
        gameController.DeathReset();
        gameController.UpdateTriesText();
    }

    public void NextLevel(string level)
    {
        
        SceneManager.LoadScene(level);
        gameController.Endgame.gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameController.TotalTries = 1;
        gameController.skipAddTries = true;
        gameController.DeathReset();
        gameController.UpdateTriesText();

    }

    public void Quit()
    {

        Application.Quit();
    }


}
