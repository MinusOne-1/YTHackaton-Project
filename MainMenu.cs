using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	
	 void FixedUpdate(){
		 if(Input.GetKeyDown(KeyCode.Escape)) 
				CloseApplication();
	 }
	 

	public void OpenCurrentSceneAdditive(int index){
		Time.timeScale = 0f;
		SceneManager.LoadScene (index, LoadSceneMode.Additive);
	}
	
	
	
	public void CloseApplication(){
		Application.Quit();
	}
	public void ResumeGame()
{
    SceneManager.UnloadScene (1);
}
	
	public void OpenCurrentScene(int index){
		
		SceneManager.LoadScene(index);
	}
}
