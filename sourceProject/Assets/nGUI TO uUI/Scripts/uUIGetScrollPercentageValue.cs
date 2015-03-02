// All Rights Reserved, Muhammad A.Moniem (mamoniem) 2015  http://www.mamoniem.com 
// v1.0
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uUIGetScrollPercentageValue : MonoBehaviour {
	
	public Scrollbar scrollBarObject;
	private Text textObject;

	// Use this for initialization
	void Awake () {
		textObject = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		textObject.text = ((int)(scrollBarObject.value*100.0)).ToString()+"%";
	}
}
