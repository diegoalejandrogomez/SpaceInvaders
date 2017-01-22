using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnerController : MonoBehaviour {
    public GameObject Star;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SpawnStars();
    }

    private void SpawnStars()
    {
        for (int i = 0; i < Random.Range(0, 50); i++)
        {
            GameObject newStar = Object.Instantiate(Star);
            var position = newStar.GetComponent<Transform>().position;
            position.x += Random.Range(-50, 50);
            position.y += Random.Range(0, 20);
            newStar.GetComponent<Transform>().position = position;
            newStar.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(newStar, 2);
        }
    }
}
