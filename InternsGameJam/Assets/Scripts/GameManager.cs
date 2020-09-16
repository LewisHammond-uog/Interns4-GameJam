using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get => instance; set => instance = value; }

    private int score;
    public int CurrentScore { private set => score = value; get => score; }
    private int scorePerKill = 1000;

    //Event for score change
    public delegate void ScoreChangeEvent(int newScore);
    public static event ScoreChangeEvent ReportScoreIncrease;
    public static event ScoreChangeEvent ReportScoreDecrease;
    public static event ScoreChangeEvent ReportScoreReset;


    private float gameSpeed = 1f;

    public float GameSpeed
    {
        get => gameSpeed;
        set
        {
            gameSpeed = value;
            //Trigger event
            GameSpeedChanged?.Invoke();
        }
    }

    //Event for when the game speed is changed
    public delegate void GameManagerEvent();
    public static event GameManagerEvent GameSpeedChanged;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        
        instance = this;
    }

    private void EnemyDeathIncreaseScore()
    {
        //Increase the score
        CurrentScore += scorePerKill;
        ReportScoreIncrease?.Invoke(CurrentScore);
    }

    public void TestGameSpeed(int speed)
    {
        GameSpeed += speed;

        Debug.Log("The game speed is now " + GameSpeed);
    }

    public IEnumerator SlowTimescaleForSetTime(float gameSpeed, float time)
    {
        this.gameSpeed = gameSpeed; 
        yield return new WaitForSecondsRealtime(time);
        this.gameSpeed = 1;
    }

    private void OnEnable()
    {
        EnemyHealth.EnemyDeath += EnemyDeathIncreaseScore;
    }
    private void OnDisable()
    {
        EnemyHealth.EnemyDeath -= EnemyDeathIncreaseScore;
    }
}
