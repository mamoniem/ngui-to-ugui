using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]

public class uUIEmoticons : MonoBehaviour {

	public bool isSupported;
	Text mText;
	string TheOldText;
	string TheNewText;

	public Sprite[] sprites;

	void Awake (){
		mText = this.gameObject.GetComponent<Text>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isSupported){
			TheOldText = mText.text;

			//Material newMaterial = new Material (Shader.Find("UI/Default"));
			//newMaterial.mainTexture = sprites[0].texture;
			//TheNewText = TheOldText.Replace(":)", "<quad material=0 size=20 x=0.1 y=0.1 width=0.5 height=0.5 />");
			TheNewText = TheOldText.Replace(":)", "<quad material=1 size=20 x=0.1 y=0.1 width=0.5 height=0.5 />");

			mText.text = TheNewText;
		}
	}
}
