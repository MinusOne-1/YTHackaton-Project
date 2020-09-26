using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2d : MonoBehaviour
{
    // Start is called before the first frame update
	
	
	public GameObject drop=null;
	bool deth = false;
	public int currHP;
	bool is_hit = false;
    void Start(){
        
    }
	
	public void recountHP(int delta_hp){
		if (delta_hp < 0){
			currHP += delta_hp;
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 3f);
			StopCoroutine(OnHit());
			is_hit = true;
			StartCoroutine(OnHit());
		}
		if (currHP <= 0){
			StartCoroutine(Death());
			
		}
	}
	
	public IEnumerator Death(){
		if (drop != null){
			Instantiate(drop, transform.position, transform.rotation);
		}
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(5f);
		
		Destroy(gameObject);
		
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
    // Update is called once per frame
    
	private void OnCollisionEnter2D(Collision2D collision)
	{	
		if (collision.gameObject.tag == "Player"){
			collision.gameObject.GetComponent<Player2d>().RecountHP(-1);
		 collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
      
		}	}
	
	void Update()
    {
        
    }
}
