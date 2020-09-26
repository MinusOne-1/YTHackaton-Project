using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMenu : MonoBehaviour
{

	public AudioSource audio;
	public AudioClip idle;
	public AudioClip fight;
	public string curr = "Settings";
	//public AudioClip fight_down;
    // Start is called before the first frame update
    void Start()
    {
        audio.clip = idle;
		audio.volume = 0.5f;
		audio.loop = true;
		audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
		if(SceneManager.GetActiveScene().name!=curr){
			if(SceneManager.GetActiveScene().name.Contains("Firrhing")){
				 audio.clip = fight;
				 audio.loop = true;
			}
			if(!SceneManager.GetActiveScene().name.Contains("Firrhing") &&
								curr.Contains("Firrhing")){
				audio.clip = idle;
				audio.loop = true;
			}
			curr = SceneManager.GetActiveScene().name;
			
		}
	
        
    }
}
