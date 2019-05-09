using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    Player player;
    Text hpText;

	// Use this for initialization
	void Start () {
        hpText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        hpText.text = player.GetHP().ToString();
    }
}
