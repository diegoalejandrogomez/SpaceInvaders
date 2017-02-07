using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour {
    public int Life = 5;
	
    void OnCollisionEnter2D(Collision2D col)
    {
        
        Destroy(col.gameObject);

        Life--;
        if (Life == 0)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 scale = GetComponent<Transform>().localScale;
        scale.y -= 1;
        GetComponent<Transform>().localScale = scale;
        
    }
}
