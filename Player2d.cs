using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2d : MonoBehaviour

{
    Rigidbody2D rb;
    public float speed = 10f;
    public float jumpheigth = 10f;
	public Transform groundCheck;
	bool is_grounded; 
	Animator anim;
	bool deth = false;
	bool win = false;
		bool jumping = false;
		bool hitting = false;
	GameObject enter_dialod = null;
	public PanelsController main;
	
	int currHP;
	int maxHP = 3;
	bool is_hit = false;
	bool is_hit_plus = false;
	
	public bool save_for_hitting = false;
	public bool shifting = false;
	
	Collider2D collider;
	public CapsuleCollider2D collider_idle;
	public BoxCollider2D collider_shiting;
	
	
    // Start is called before the first frame update
    void Start(){ 
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		currHP = maxHP;
		collider = collider_idle;
    }
	
	
	public int getMax(){
		return maxHP;
	}
	public int getCurrHP(){
		return currHP;
	}
    //Обновление физических компонентов
    void FixedUpdate(){
		//изменение координаты по нажатию на кнопки
		ChangerOfAnims();
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
      
	    if (Input.GetKeyDown(KeyCode.Space) && is_grounded){ // прыжок
		jumping = true;
			StartCoroutine(Jump());
        }
		  else if (Input.GetKeyDown(KeyCode.E) && is_grounded){ // удар
			hitting = true;
			StartCoroutine(HittingEnemy());
        }
		
		if (Input.GetKeyDown(KeyCode.LeftShift)){ 
           ChangeShiftState(!shifting);
       }
		
    }
	
	
	/*void Shot(){
		if (number_of_bullet > 0 && ! shooting) 
			{shooting = true;
			anim.SetInteger("State", 10);
		StartCoroutine(Shotting());}
	}
	
	IEnumerator Shotting(){
			yield return new WaitForSeconds(0.33f);
			GameObject bul = Instantiate(bullet, trunk.transform.position, transform.rotation);
			yield return new WaitForSeconds(0.15f);
			anim.SetInteger("State", 1);
			shooting = false;
			number_of_bullet -= 1;
	}
*/
    // обновление графических компонентов
    void Update()
    {
		
		CheckGroung();
    }
	
	void ChangeShiftState(bool state){
		if (!deth){
				   shifting = state;
				   if (state){
					   collider_shiting.enabled = true;
					   collider = collider_shiting;
						collider_idle.enabled = false;
				   }
				   else{
					   collider_idle.enabled = true;
					   collider = collider_idle;
						collider_shiting.enabled = false;
				   }
			}}
	
	void ChangerOfAnims(){
		if (hitting){
			anim.SetInteger("State", 2);
			ChangeShiftState(false);
		}
		else if(jumping){

			anim.SetInteger("State", 4);
			ChangeShiftState(false);
		}
		 else if (shifting){
			anim.SetInteger("State", 7); //shifting
			if (Input.GetAxis("Horizontal") != 0 ){
				anim.SetInteger("State", 8); //shifting_step
			}
		}
		else if (Input.GetAxis("Horizontal") == 0 && is_grounded){
				anim.SetInteger("State", 0);
			}
		else if(Input.GetAxis("Horizontal") != 0 && is_grounded){
			anim.SetInteger("State", 1);
		}

		if (!is_grounded) {
			anim.SetInteger("State", 3);
			ChangeShiftState(false);
		}
		Flip();
	}

    void Flip()  { 		//поворот картинки по нажатию на кнопки
        if (Input.GetAxis("Horizontal") > 0)
            {
             transform.eulerAngles = new Vector3(0, 180, 0);
            }
        else if (Input.GetAxis("Horizontal") < 0)
        {
           this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

	void CheckGroung(){
		Collider2D[]  colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
		is_grounded = colliders.Length > 1;
		
	}
	
	public void RecountHP(int delta_hp){
		if (delta_hp < 0 && !save_for_hitting){
			currHP += delta_hp;
			StopCoroutine(OnHealthPlus());
			StopCoroutine(OnHit());
			is_hit = true;
			StartCoroutine(OnHit());
		}
		else if (delta_hp > 0){
			currHP += delta_hp;
			StopCoroutine(OnHit());
			StopCoroutine(OnHealthPlus());
			is_hit_plus = true;
			StartCoroutine(OnHealthPlus());
		}
		print(currHP);
		if (currHP <= 0){
			deth = true;
			collider_idle.enabled = false;
			collider_shiting.enabled = false;
			Invoke("Lose", 2f);
		}
	}
	
	
	
	
	IEnumerator NoHit(){
		transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
		save_for_hitting = true;
		yield return new WaitForSeconds(10f);
		save_for_hitting = false;
		transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
	}
	IEnumerator HittingEnemy(){
		yield return new WaitForSeconds(0.20f);
            //удар
			Collider2D[]  colliders = Physics2D.OverlapCircleAll(transform.GetChild(0).transform.position , 0.2f);
		if(colliders.Length > 0){
			try{
				print(colliders[0].gameObject.GetComponent<Enemy2d>());
				colliders[0].gameObject.GetComponent<Enemy2d>().recountHP(-2);}
			catch{print("не враг");}
		}
		yield return new WaitForSeconds(0.40f);
			hitting = false;
	}
	IEnumerator Jump(){
		yield return new WaitForSeconds(0.35f);
            rb.AddForce(transform.up * jumpheigth, ForceMode2D.Impulse);
			is_grounded = false;
			jumping = false;
	}
	IEnumerator OnHealthPlus(){
		if (is_hit_plus)
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r - 0.09f, 1f, GetComponent<SpriteRenderer>().color.b - 0.09f);
		else
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r + 0.09f, 1f, GetComponent<SpriteRenderer>().color.b + 0.09f);

		if (GetComponent<SpriteRenderer>().color.g >= 1f){
				StopCoroutine(OnHealthPlus());
				GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
			}
		if (GetComponent<SpriteRenderer>().color.g <= 0f)
			is_hit_plus = false;
		
	yield return new WaitForSeconds(0.02f);
		StartCoroutine(OnHealthPlus());
	}
	IEnumerator OnHit() {
		if (is_hit)
			GetComponent<SpriteRenderer>().color = new Color(1f,GetComponent<SpriteRenderer>().color.g - 0.09f, GetComponent<SpriteRenderer>().color.b - 0.09f);
		else
			GetComponent<SpriteRenderer>().color = new Color(1f,GetComponent<SpriteRenderer>().color.g + 0.09f, GetComponent<SpriteRenderer>().color.b + 0.09f);

		if (GetComponent<SpriteRenderer>().color.g >= 1f){
				StopCoroutine(OnHit());
				GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
			}
		if (GetComponent<SpriteRenderer>().color.g <= 0f)
			is_hit = false;
		
	yield return new WaitForSeconds(0.02f);
		StartCoroutine(OnHit());
	
	}
	void Lose() {
		if(deth){
				main.GetComponent<PanelsController>().OpenLosePanel();
		}
		else if(win){
			main.GetComponent<PanelsController>().OpenWinPanel();
		}
		}
	}
