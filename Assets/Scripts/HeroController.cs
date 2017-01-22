using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {
    public GameObject Hero;
    public float Speed = 1;
    public GameObject Bullet;
    public float ShootCooldown;

    private bool shooting = false;
	// Use this for initialization
	void Start () {
		
	}

    private IEnumerator SetShootingCooldown()
    {
        shooting = true;
        yield return new WaitForSeconds(ShootCooldown);
        shooting = false;
    }

    void FixedUpdate () {
        Vector3 position = Hero.GetComponent<Transform>().position;
        position.x += Input.GetAxis("Horizontal") * Speed;
        Hero.GetComponent<Transform>().position = position;

        if (Input.GetKey(KeyCode.Space) && !shooting)
        {
            StartCoroutine(SetShootingCooldown());
            GameObject bullet = Object.Instantiate(Bullet);
            
            bullet.GetComponent<Transform>().position = new Vector3(position.x, position.y + 1, position.z);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }


    }
}
