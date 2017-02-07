using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour {
    public GameObject Hero;
    public float Speed = 1;
    public GameObject Bullet;
    public float ShootCooldown;
    public AudioClip ShootAudio;
    public AudioClip ExplotionAudio;
    public UnityEngine.UI.Text LifeText;
    public static int Lifes = 3;
    private bool shooting = false;
	// Use this for initialization
	void Start () {
        LifeText.text = string.Format("Life: {0}", Lifes);
    }

    private IEnumerator SetShootingCooldown()
    {
        shooting = true;
        yield return new WaitForSeconds(ShootCooldown);
        shooting = false;
    }

    void FixedUpdate () {
        Vector3 position = Hero.GetComponent<Transform>().position;
        int input = 0;
        if (Input.GetAxis("Horizontal") > 0)
            input = 1;
        else if (Input.GetAxis("Horizontal") < 0)
            input = -1;

        position.x += input * 0.15f  * Speed;
        Hero.GetComponent<Transform>().position = position;

        if (Input.GetKey(KeyCode.Space) && !shooting)
        {
            AudioSource.PlayClipAtPoint(ShootAudio, position);
            StartCoroutine(SetShootingCooldown());
            GameObject bullet = Object.Instantiate(Bullet);
            
            bullet.GetComponent<Transform>().position = new Vector3(position.x, position.y + 1, position.z);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            Destroy(col.gameObject);
            AudioSource.PlayClipAtPoint(ExplotionAudio, GetComponent<Transform>().position);
            Lifes--;
            LifeText.text = string.Format("Life: {0}", Lifes);
            if (Lifes == 0)
            {
                RestartGame();
            }
            StartCoroutine(Respawn());
        }
        
    }

    private static void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private IEnumerator Respawn()
    {
        GetComponent<Animator>().SetBool("Exploding", true);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Exploding", false);
    }
}
