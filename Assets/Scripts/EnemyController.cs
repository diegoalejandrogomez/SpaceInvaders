using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Sprite Animation1;
    public Sprite Animation2;
    public Sprite Exploding;
    public GameLogicController GameLogicController;
    public GameObject Bullet;
    public int EnemyType;
    public int X;
    public int Y;

    public float Speed = 1f;

    private int index = 0;
    private bool exploding = false;
    
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = Animation1;

    }

    void FixedUpdate()
    {
        Vector3 position = GetComponent<Transform>().position;
        position.x += Speed * 0.03f;
        GetComponent<Transform>().position = position;

    }

    private void LateUpdate()
    {
        if (exploding)
            return;

        GetComponent<SpriteRenderer>().sprite = index < 60 ? Animation1 : Animation2;
        index++;
        index = index % 120;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
        GameLogicController.CalculatePoints(X, Y);
    }

    public void Destroy()
    {
        exploding = true;
        GetComponent<SpriteRenderer>().sprite = Exploding;
        Destroy(gameObject, 0.5f);
    }

    public void Shoot()
    {
        GameObject bullet = UnityEngine.Object.Instantiate(Bullet);
        Vector3 position = GetComponent<Transform>().position;
        bullet.GetComponent<Transform>().position = new Vector3(position.x, position.y - 0.5f, position.z);
        bullet.GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
