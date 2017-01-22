using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Sprite Animation1;
    public Sprite Animation2;
    public Sprite Exploding;
    private int index = 0;
    private bool exploding = false;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = Animation1;

    }

    // Update is called once per frame
    void Update()
    {
        //if (!GetComponent<Animator>().IsInTransition(0))
        //{
        //    Destroy(gameObject);
        //}
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
        exploding = true;
        GetComponent<SpriteRenderer>().sprite = Exploding;
        Destroy(col.gameObject);
        Destroy(gameObject, 0.5f);
    }


}
