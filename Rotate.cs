using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    public Bounds bounds;
    public float speed = 1.0F;
    // Use this for initialization
    void Start () {
        var collider = gameObject.GetComponent<BoxCollider> ();
        if (collider == null) {
            collider = gameObject.AddComponent<BoxCollider>();
            Debug.Log("No Collider is Detected");
        }
        bounds = collider.bounds;

    }

    // Update is called once per frame
    void Update () {

        if(Input.GetMouseButton(0)){
            if(bounds.size.magnitude > 0){
                var dtx = Input.GetAxis("Mouse X")*speed;
                var dty = Input.GetAxis("Mouse Y")*speed;
                var pivot = bounds.center;

                transform.RotateAround(pivot,Vector3.up,dtx);
                transform.RotateAround(pivot,Vector3.left,dty);
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
            }
        }
    }
}