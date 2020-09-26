using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFlipper : MonoBehaviour
{
	
	public float speed = -1f;
	public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position =new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z - 5f);
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 position = target.position;
		position.y = target.position.y + 3f;
		position.z = target.position.z - 3.5f;
		
		transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
	}
	
	
}
