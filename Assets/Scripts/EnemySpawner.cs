using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyModel;
    public int Rows = 5;
    public int Columns = 10;
    public int Offset = 5;
    public Vector3 InitialPosition = new Vector3(-11.46f, 2.13f, -8.17f);
    public Sprite[] EnemiesSprite1;
    public Sprite[] EnemiesSprite2;
    public Sprite[] EnemiesSpriteExploding;

    // Use this for initialization
    void Start () {
        GameObject newEnemy = EnemyModel;
        Vector2 newPosition = InitialPosition;

        for (int i = 0; i < Rows; ++i)
        { 
            for (int j = 0; j < Columns; j++)
            {
                newEnemy = Object.Instantiate(newEnemy);
                newEnemy.name = string.Format("Enemy{0}", i * Columns + j);
                Vector3 position = newEnemy.GetComponent<Transform>().position;
                position.x = InitialPosition.x + Offset * (j + 1);
                position.y = InitialPosition.y - 1 * (i + 1);
                position.z = InitialPosition.z;
                newEnemy.GetComponent<Transform>().position = position;
                var index = Random.Range(0, 5);
                newEnemy.GetComponent<EnemyController>().Animation1 = EnemiesSprite1[index];
                newEnemy.GetComponent<EnemyController>().Animation2 = EnemiesSprite2[index];
                newEnemy.GetComponent<EnemyController>().Exploding = EnemiesSpriteExploding[index];
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
