using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 2.0f;
    public List<GameObject> prefabs;

    public TextMeshProUGUI scoreText; // This the TMP object, not a string
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;


    private int score = 0;
    public bool gameActive = false;

    public void StartGame(int diff)
    {
        gameActive = true;
        score = 0;
        spawnRate = spawnRate / diff; // could also use: spawnRate /= diff
        Debug.Log("Game Spawn Rate = " + spawnRate);
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    // this method is made public so that target class can access it
    public void GameOver()
    {
        gameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    IEnumerator SpawnTarget()
    {
        while(gameActive) // Checking to see if the game is active before spawning
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
        }
    }

    public void UpdateScore(int scoreDelta)
    {
        score += scoreDelta;
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "SCORE: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
