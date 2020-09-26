using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWithNapr : MonoBehaviour
{
	
	Animator anim;
	public SpriteRenderer img;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		img = GetComponent<SpriteRenderer>();
		anim.SetInteger("State", 3);
    }

    public void Update_sprite()
    {
		
		if(Input.GetAxis("Horizontal") == 1){
					img.flipX = true;
		}
		else if(Input.GetAxis("Horizontal") == -1){
			img.flipX = false;
		}
		anim.SetInteger("State", 3);
		if (Input.GetAxis("Vertical") == -1){
			anim.SetInteger("State", 0);
		}
		if (Input.GetAxis("Vertical") == 1){
			anim.SetInteger("State", 1);
		}
        if(Input.GetAxis("Horizontal") != 0){
			anim.SetInteger("State", 2);
			
		}
    }
}
