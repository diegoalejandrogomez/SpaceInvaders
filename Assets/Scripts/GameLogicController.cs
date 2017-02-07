using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicController : MonoBehaviour {

    public GameObject EnemySpawnerObject;

    private EnemySpawner enemySpawnerController;
    private List<List<GameObject>> enemyPositions;
    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Text HighScoreText;
    public float Cooldown = 3;

    public  List<int> Fibonacci;
    private static int Score = 0;

    // Use this for initialization
    void Start () {
        Initialize();
        CalculateFibonacci(enemyPositions[0].Count + 1);
        InvokeRepeating("Shoot", 2.0f, 2.0f);
        InvokeRepeating("ChangeMovementDirection", 0, 2.0f);
        DisplayScore();
    }

    private void Initialize()
    {
        enemySpawnerController = EnemySpawnerObject.GetComponent<EnemySpawner>();
        enemySpawnerController.SpawnEnemies();
        enemyPositions = enemySpawnerController.GetEnemyPositions();
    }

    public void CalculatePoints(int x, int y)
    {
        List<GameObject> row = enemyPositions[x];
        int sequence = 1;

        if (row[y] != null)
        {
            for (int l = y - 1; l >= 0; l--)
            {
                if (row[l] != null && row[l].GetComponent<EnemyController>().EnemyType == row[y].GetComponent<EnemyController>().EnemyType)
                {
                    sequence++;
                    row[l].GetComponent<EnemyController>().Destroy();
                    row[l] = null;
                }
                else
                {
                    break;
                }
            }

            for (int r = y + 1; r < row.Count; r++)
            {
                if (row[r] != null && row[r].GetComponent<EnemyController>().EnemyType == row[y].GetComponent<EnemyController>().EnemyType)
                {
                    sequence++;
                    row[r].GetComponent<EnemyController>().Destroy();
                    row[r] = null;
                }
                else
                {
                    break;
                }
            }


            row[y].GetComponent<EnemyController>().Destroy();
        }

        row[y] = null;

        if (row.All(i => i == null))
            enemyPositions.ElementAt(x).Clear();

        CalculatePoints(sequence);
        ValidateLevel();
    }

    private void ValidateLevel()
    {
        if (enemyPositions == null || enemyPositions.All(enemyRow => enemyRow == null || enemyRow.Count == 0))
        {
            NextLevel();
        }
    }

    private void CalculatePoints(int sequence)
    {
        Score += sequence * Fibonacci[sequence] * 10;

        if (Score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", Score);

        DisplayScore();
    }

    private void DisplayScore()
    {
        ScoreText.text = string.Format("Score {0}", Score);
        HighScoreText.text = string.Format("High Score {0}", PlayerPrefs.GetInt("HighScore"));
    }

    private void NextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void CalculateFibonacci(int number)
    {
        int a = 0;
        int b = 1;
        int c = 1;

        Fibonacci.Add(1);
        for (int i = 0; i < number; i++)
        {
            c = b + a;
            a = b;
            b = c;
            Fibonacci.Add(c);
        }
    }

    private void Shoot()
    {
        if (enemyPositions == null || enemyPositions.Count == 0)
            return;
        var shootingRow = enemyPositions.Where(i=> i != null && i.Count != 0).Last();
        var notNullGameObjects = shootingRow.Where(i => i != null);

        var shootingEnemy = notNullGameObjects.ElementAt(Random.Range(0, notNullGameObjects.Count()));
        shootingEnemy.GetComponent<EnemyController>().Shoot();

    }

    private void ChangeMovementDirection()
    {
        foreach(var row in enemyPositions)
        {
            foreach(var enemy in row)
            {
                if (enemy != null)
                    enemy.GetComponent<EnemyController>().Speed *= -1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0; //pause
            SceneManager.LoadScene("Assets/Scenes/Menu.unity", LoadSceneMode.Additive);
        }
    }
}
