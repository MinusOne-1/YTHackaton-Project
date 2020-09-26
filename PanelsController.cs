using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelsController : MonoBehaviour
{
	public GameObject pause_screen, win_screen, lose_screen;
	
		public GameObject[] dialogs;
		public  GameObject inventary;
		public Player player;
		public Player2d player1;
		public Image[] lifes;
		public Text[] texts;
		public Sprite life, viod_life;
		
	// Start is called before the first frame update
    void Start(){
		player.enabled = true;
        DeactiveAllDialog();
    }
		
	public void OpenLosePanel(){
		Time.timeScale = 0f;
		try
		{player.enabled = false;}
		catch
		{player1.enabled = false;}
		lose_screen.SetActive(true);
	}	
	
	public void OpenWinPanel(){
		Time.timeScale = 0f;
		try
			{player.enabled = false;}
		catch
			{player1.enabled = false;}
		win_screen.SetActive(true);
	}	
		
	public void Restart() 
	{
		Time.timeScale = 1f;
		try
			{player.enabled = true;}
		catch
			{player1.enabled = true;}
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void Win(){
		Time.timeScale = 0f;
		try
			{player.enabled = false;}
		catch
			{player1.enabled = false;}
		pause_screen.SetActive(true);
	}
    
    // Update is called once per frame
    void Update(){
		try{
			if (inventary.activeSelf){
				
				for(int i = 0; i < 6;i++){
					print(i);
					//print(inventary.transform.Find("items").Find(i.ToString()).gameObject.GetComponent<Image>());
				inventary.transform.Find("items").Find(i.ToString()).GetComponent<Image>().sprite =  Resources.Load<Sprite>(player.inventary[i].Split(' ')[0]);
				inventary.transform.Find("items").Find(i.ToString()).Find("Text").gameObject.GetComponent<Text>().text = player.inventary[i] + i.ToString();
				}
		}
		}
		catch{print("Нет инвентаря :(");}
		for (int i = 0; i < lifes.Length; i++)
		{
			try{
			if(player.getCurrHP() > i) 
				lifes[i].sprite = life;
			else
			lifes[i].sprite = viod_life;
		}
		catch{
			if(player1.getCurrHP() > i) 
				lifes[i].sprite = life;
			else
			lifes[i].sprite = viod_life;}
		}
    
	}
	
	 public void SetSoundVolum(Slider slider){
		transform.Find("Sound").gameObject.GetComponent<SoundMenu>().audio.volume = slider.value;
	}
	
	public void SetTextFromTextButton(Text text){
		print(2);
		inventary.transform.Find("InputField").gameObject.GetComponent<InputField>().text = text.text;
		print(1 + " " + inventary.transform.Find("InputField").gameObject.GetComponent<InputField>().text);
	}
			
			
	void DeactiveAllDialog(){
		 for(int i=0; i < dialogs.Length; i++){
			dialogs[i].SetActive(false);
		}
	}
	
	
	public void pauseOn(){
		Time.timeScale = 0f;
		try
			{player.enabled = false;}
		catch
			{player1.enabled = false;}
		pause_screen.SetActive(true);
	}
	
	public void OpenCurrentScene(int index){
		Time.timeScale = 1f;
		SceneManager.LoadScene(index);
	}
	

	public void OpenCurrentSceneAdditive(int index){
		Time.timeScale = 0f;
		SceneManager.LoadScene (index, LoadSceneMode.Additive);
	}
	
	
	
	public void pauseOff(){
		Time.timeScale = 1f;
		try
			{player.enabled = true;}
		catch
			{player1.enabled = true;}
		pause_screen.SetActive(false);		
	}
}
