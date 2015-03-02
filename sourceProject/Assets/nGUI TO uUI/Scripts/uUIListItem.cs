// All Rights Reserved, Muhammad A.Moniem (mamoniem) 2015  http://www.mamoniem.com 
// v1.0
using UnityEngine;
using System.Collections;

public class uUIListItem : MonoBehaviour {

	public GameObject thePopupController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SelectThisItem(){
		thePopupController.gameObject.SendMessage("SetSelection", this.name);
	}
}
