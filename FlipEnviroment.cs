using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnviroment : MonoBehaviour
{
    // Start is called before the first frame update
		public SpriteRenderer[] sprites;
		public SpriteRenderer curr_sprite;
		public Player player;
		
    void Start()
    {
        curr_sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i<8 ; i++)
			sprites[i].enabled = false;
		
		sprites[(int)player.angal_pow].enabled = true;
    }
}
