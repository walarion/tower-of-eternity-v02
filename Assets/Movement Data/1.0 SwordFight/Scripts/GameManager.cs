using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameCompleted = false;

   [HideInInspector] public int totalEnemies = 3;
    public static event UnityAction OnGameCompleted = delegate { };
    public static event UnityAction OnPlayerDead = delegate { };

    private void Start()
    {
        totalEnemies = EnemyCounter.instance.totalEnemies;
    }

    public GameObject GameOverPanel;
    public void GameEnd()
    {
        GameOverPanel.SetActive(true);
    }

    public void EnemyKilled()
    {
        totalEnemies -= 1;
        if (totalEnemies <=0)
        {
            totalEnemies = 0;
            isGameCompleted = true;
            if (OnGameCompleted!=null)
            {
                OnGameCompleted();
            }
        }
    }

    public void PlayerDead() {
        if (OnPlayerDead!=null)
        {
            OnPlayerDead();
        }
    }
    public void LoadNextLevel(int sceneNumber) {
        //Application.LoadLevel(2); // next level
         Application.LoadLevel(sceneNumber);

    }
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void LoadMainMenu() {
        Application.LoadLevel(0); // main menu
    }

    public void LoadGame() {
        Application.LoadLevel(1); // level 1
    }
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
           // DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
