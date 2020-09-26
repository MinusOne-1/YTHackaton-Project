using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperObject : MonoBehaviour
{
	
	public GameObject bullet;
	public Transform trunk;
	public float time_mid_shoot = 2f;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Drop());
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	IEnumerator Drop() {
		
		yield return new WaitForSeconds(time_mid_shoot);
		GameObject temp = Instantiate(bullet, trunk.transform.position, transform.rotation);
		temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0.2f, temp.GetComponent<Rigidbody2D>().velocity.y);
		StartCoroutine(Drop());
	}
}
