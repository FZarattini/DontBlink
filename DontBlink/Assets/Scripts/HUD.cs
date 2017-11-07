using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour {
    Player player;
    public Text prefab;
    private Text cd;
    
 	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cd = Instantiate(prefab,new Vector2(0,0) , Quaternion.identity);
        
    }
	
	// Update is called once per frame
	void Update () {
        cd.text = "coolDown: " + player.coolDown.ToString();
	}
}
