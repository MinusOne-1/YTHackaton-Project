using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
		public GameObject dialog_panel;
		public GameObject hint;
		public int scene;
	
	public void hintView(bool view){
		hint.SetActive(view);
	}
}
