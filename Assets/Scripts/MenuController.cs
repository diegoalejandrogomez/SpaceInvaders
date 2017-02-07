using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    private static bool playing = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape) && playing)
        {
            
            SceneManager.LoadScene("Assets/Scenes/GameScene.unity");
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Assets/Scenes/GameScene.unity");
        if (playing)
        {
            Time.timeScale = 1;
            //Scene scene = SceneManager.GetActiveScene();
            //SceneManager.UnloadScene(scene);
            //SceneManager.LoadScene(scene.name);
        }

        playing = true;
    }
}
