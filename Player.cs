using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour

{
    Rigidbody rb;
	public int hp = 3;
	public string[] inventary = new string[6];
    public float speed = 8f;
	public float angal_pow = 0;
	public bool save_from_rot = true;
	public float smooth = 2f;
	float up2down_where = 1f;
	public SpriteWithNapr sprite;
	GameObject enter_dialod = null;
	DialogTrigger enter_battle = null;
	
	
    void Start(){ 
	rb = GetComponent<Rigidbody>();
		for(int i = 0; i < 6; i++){
			print(i);
			inventary[i] = "default";
		}
    }
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.T)){ 
            if(enter_dialod != null){
				enter_dialod.GetComponent<DialogTrigger>().hintView(false);
				enter_dialod.GetComponent<DialogTrigger>().dialog_panel.SetActive(true);
			}
        }
		if (Input.GetKeyDown(KeyCode.B)){ 

			GetComponent<Player>().enabled = false;
            if(enter_battle != null){
				enter_battle.hintView(false);
				SceneManager.LoadScene (enter_battle.scene);
			}
        }
		
	
	}
	public int getCurrHP(){
		return hp;
	}
	
    //Обновление физических компонентов
    void FixedUpdate(){
		//изменение координаты по нажатию на кнопки
		sprite.Update_sprite();
		if(angal_pow == 0){
		rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
		}
		if(angal_pow == 1){
			rb.velocity = new Vector3(0, 0, 0);
			if (Input.GetAxis("Vertical") != 0){
				rb.velocity = new Vector3(rb.velocity.x + (-speed * Input.GetAxis("Vertical") * 0.7f), 0, rb.velocity.z + (speed * Input.GetAxis("Vertical")  * 0.7f));
			}
			if(Input.GetAxis("Horizontal")!= 0){
				rb.velocity = new Vector3(rb.velocity.x + (speed * Input.GetAxis("Horizontal")  * 0.7f), 0, rb.velocity.z +( speed * Input.GetAxis("Horizontal")  * 0.7f));
			}
		}
		if(angal_pow == 2){
			rb.velocity = new Vector3(Input.GetAxis("Vertical") * -speed, 0, Input.GetAxis("Horizontal") * speed);
		}
		if(angal_pow == 3){
			rb.velocity = new Vector3(0, 0, 0);
			if (Input.GetAxis("Vertical") != 0){
				rb.velocity = new Vector3(rb.velocity.x + (-speed * Input.GetAxis("Vertical")  * 0.7f), 0, rb.velocity.z + (-speed * Input.GetAxis("Vertical")  * 0.7f));
			}
			if(Input.GetAxis("Horizontal")!= 0){
				rb.velocity = new Vector3(rb.velocity.x + (-speed * Input.GetAxis("Horizontal")  * 0.7f), 0, rb.velocity.z +(speed * Input.GetAxis("Horizontal") * 0.7f));
			}
		}
		if(angal_pow == 4){
		rb.velocity = new Vector3(Input.GetAxis("Horizontal") * -speed, 0, Input.GetAxis("Vertical") * -speed);
		}
		if(angal_pow == 5){
			rb.velocity = new Vector3(0, 0, 0);
			if (Input.GetAxis("Vertical") != 0){
				rb.velocity = new Vector3(rb.velocity.x + (speed * Input.GetAxis("Vertical") * 0.7f), 0, rb.velocity.z + (-speed * Input.GetAxis("Vertical")  * 0.7f));
			}
			if(Input.GetAxis("Horizontal")!= 0){
				rb.velocity = new Vector3(rb.velocity.x + (-speed * Input.GetAxis("Horizontal") * 0.7f), 0, rb.velocity.z +(-speed * Input.GetAxis("Horizontal") * 0.7f));
			}
		}
		if(angal_pow == 6){
			rb.velocity = new Vector3(Input.GetAxis("Vertical") * speed, 0, Input.GetAxis("Horizontal") * -speed);
		}
		if(angal_pow == 7){
			rb.velocity = new Vector3(0, 0, 0);
			if (Input.GetAxis("Vertical") != 0){
				rb.velocity = new Vector3(rb.velocity.x + (speed * Input.GetAxis("Vertical") * 0.7f), 0, rb.velocity.z + (speed * Input.GetAxis("Vertical")  * 0.7f));
			}
			if(Input.GetAxis("Horizontal")!= 0){
				rb.velocity = new Vector3(rb.velocity.x + (speed * Input.GetAxis("Horizontal") * 0.7f), 0, rb.velocity.z +(-speed * Input.GetAxis("Horizontal") * 0.7f));
			}
		}
		CameraFlip();
		//Up2down2up();
}

void Up2down2up(){
	Vector3 position = transform.position;
	position.y = position.y + up2down_where;
	transform.position = Vector3.Lerp(transform.position, position, 2f);
	up2down_where = -up2down_where;
}


void CameraFlip(){
	if(save_from_rot){
	if (Input.GetKeyDown(KeyCode.Q)){
			angal_pow += 1;
			if (angal_pow > 7){
				angal_pow = 0;
			}
			save_from_rot = false;
		StartCoroutine(NoRotation());
	}
	if (Input.GetKeyDown(KeyCode.E)){
			angal_pow -= 1;
			if (angal_pow < 0){
				angal_pow = 7;
			}		
		StartCoroutine(NoRotation());
			save_from_rot = false;
			}
	}
	
	Transform to = transform;
			to.rotation = Quaternion.Euler(0f, -45f * angal_pow, 0f);
		transform.rotation = Quaternion.Lerp(transform.rotation, to.rotation, Time.deltaTime * smooth);
	}

	private void OnTriggerEnter(Collider collision){
		
		if (collision.gameObject.tag == "Dialog"){
			enter_dialod = collision.gameObject;
			collision.gameObject.GetComponent<DialogTrigger>().hintView(true);
		}
		if (collision.gameObject.tag == "Battle"){
			enter_battle = collision.gameObject.GetComponent<DialogTrigger>();
			collision.gameObject.GetComponent<DialogTrigger>().hintView(true);
		}
	}
	
	private void OnTriggerExit(Collider collision){
		
		if (collision.gameObject.tag == "Dialog"){
			enter_dialod.GetComponent<DialogTrigger>().dialog_panel.GetComponent<Dialog>().CloseDialog();
			enter_dialod = null;
			collision.gameObject.GetComponent<DialogTrigger>().hintView(false);
		}
		if (collision.gameObject.tag == "Battle"){
			enter_battle = null;
			collision.gameObject.GetComponent<DialogTrigger>().hintView(false);
		}
	}
	
IEnumerator NoRotation(){
		yield return new WaitForSeconds(0.2f);
		save_from_rot = true;
	}
}
