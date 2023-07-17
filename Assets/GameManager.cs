using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Text EndGameText;
    public GameObject EndUI;
    EnemySpawner enemySpawner;
    // Start is called before the first frame update
    public static GameManager instance;
    private void Start()
    {
        instance = this;
        enemySpawner = gameObject.GetComponent<EnemySpawner>();
    }

    public void LoseGame()
    {
        enemySpawner.Stop();
        EndUI.SetActive(true);
        EndGameText.text = "You Lose";

    }

    public void WinGame()
    {
        EndUI.SetActive(true);
        EndGameText.text = "You Win";
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnEndButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
