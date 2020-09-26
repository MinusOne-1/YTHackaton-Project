using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMissingPlayer : MonoBehaviour
{
    // Start is called before the first frame update
	
    void Start()
    {
        Player player = GameObject.FindGameObjectsWithTag("PlayersReturner")[0].GetComponent<PlayerSave>().player;
		player.enabled = true;
		print(player);
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{ 
			try{gameObject.transform.GetChild(i).gameObject.GetComponent<PanelsController>().player = player; }catch{}
			try{gameObject.transform.GetChild(i).gameObject.GetComponent<FlipEnviroment>().player = player;}catch{}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
