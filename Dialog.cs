using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
	public Text text;
	public string[] replicas;
	public string[] pl_replicas;
	public string default_text;
	public GameObject self;
	public GameObject[] butons;
	int indxs = 0;
	int rep_indx = 0;
	public bool twist_in_closed = false;
	
	
    void Start(){
		for(int i=0; i < butons.Length; i++){
			butons[i].SetActive(false);
		}
		butons[0].SetActive(true);
		text.text = default_text;
		if (twist_in_closed){
				butons[1].SetActive(true);	
		}
    }
	// Update is called once per frame
    void Update(){
        
    }
	
	public void ChageButton(int new_indxs){
			butons[indxs].SetActive(false);
			butons[new_indxs].SetActive(true);
		indxs = new_indxs;
	}
	
	public void NextRep(){
		rep_indx += 1;
		if(rep_indx >= replicas.Length && !twist_in_closed){
			CloseDialog();
		}
		else if(rep_indx == replicas.Length - 1 && twist_in_closed){
			twistInClosed();
		}
		text.text = replicas[rep_indx];
	}
	
	public void twistInClosed(){
		butons[0].SetActive(false);
	}
	
	public void MakeDinamicIfINDX(GameObject obj){
		if (rep_indx == 3)
		{		obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		
		}}
	
	public void ChangeTextForButton(GameObject button){
		button.GetComponentInChildren<Text>().text = pl_replicas[rep_indx];
	}
	
	public void CloseDialog(){
		for(int i=0; i < butons.Length; i++){
			butons[i].SetActive(false);
		}
		butons[0].SetActive(true);
		rep_indx = 0;
		indxs = 0;
		text.text = default_text;
		self.SetActive(false);
    }
}
