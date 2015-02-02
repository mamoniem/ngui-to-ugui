using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConverterMenu : MonoBehaviour {
	/*
	[MenuItem ("nGUI TO uGUI/Convert/Selected")]
	static void OnConvertSelected () {
		GameObject theSelectedObject;
		GameObject theNewObject;
		GameObject thEventSystem;

		if (Selection.activeGameObject != null){
			theSelectedObject = Selection.activeGameObject;
			Debug.Log (theSelectedObject.name);

			theNewObject = (GameObject)Instantiate (theSelectedObject);
			theNewObject.name = theSelectedObject.name;
			theSelectedObject.SetActive(false);

			thEventSystem = (GameObject)Instantiate ((AssetDatabase.LoadAssetAtPath ("Assets/nGUI TO uGUI/Prefabs/EventSystem.prefab", typeof(GameObject))));
			thEventSystem.name = "EventSystem";


		}else{
			Debug.LogError ("<Color=red>NOTHING SELECTED</Color>, <Color=yellow>Please select something to convert</Color>");
		}
	}
	*/

	/*
	[MenuItem ("nGUI TO uGUI/Atlas Convert/Selected")]
	static void OnConvertWidget () {
		GameObject theSelectedWidget;
		GameObject theNewWidget;
		GameObject thEventSystem;
		Texture2D theTextureAtlas;

		if (Selection.activeGameObject != null){
			if(!Directory.Exists("Assets/CONVERSION_DATA")){
				AssetDatabase.CreateFolder ("Assets", "CONVERSION_DATA");
			}else{

			}
			theSelectedWidget = Selection.activeGameObject;
			theNewWidget = (GameObject)Instantiate (theSelectedWidget);
			theNewWidget.name = theSelectedWidget.name;
			theSelectedWidget.SetActive(false);

			if (theNewWidget.GetComponent<UISprite>()){
				UISprite theUISprite;
				Image theReplacedUISprite;
				Sprite theNewResourceSprite;

				theUISprite = theNewWidget.GetComponent<UISprite>();
				theReplacedUISprite = theNewWidget.gameObject.AddComponent<Image>();

				theReplacedUISprite.color = theUISprite.color;

				float theUISpriteWidth = theUISprite.atlas.GetSprite(theUISprite.spriteName).width;
				float theUISpriteHeight = theUISprite.atlas.GetSprite(theUISprite.spriteName).height;
				float theUISpriteX = theUISprite.atlas.GetSprite(theUISprite.spriteName).x;
				float theUISpriteY = theUISprite.atlas.GetSprite(theUISprite.spriteName).y;

				Rect theRectFromAtlas = new Rect (theUISpriteX, theUISpriteY, theUISpriteWidth, theUISpriteHeight);

				Debug.Log (theRectFromAtlas);
				DestroyImmediate (theNewWidget.GetComponent<UISprite>());

				theNewResourceSprite = new Sprite();
				theNewResourceSprite = 	Sprite.Create((Texture2D)theUISprite.atlas.texture, theRectFromAtlas, new Vector2(0.0f,1.0f), 100.0f);
				//theReplacedUISprite.sprite = theNewResourceSprite;
				AssetDatabase.CopyAsset (AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(theUISprite.atlas.name)[0]), "Assets/CONVERSION_DATA/"+theUISprite.atlas.name+".png");
				AssetDatabase.Refresh();
				Debug.Log(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(theUISprite.atlas.name)[0]) + "\n" + "Assets/CONVERSION_DATA/"+theUISprite.atlas.name+".png");
				///

				string path = "Assets/CONVERSION_DATA/"+theUISprite.atlas.name+".png";
				TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
				importer.textureType = TextureImporterType.Sprite;
				importer.mipmapEnabled = false;
				importer.spriteImportMode = SpriteImportMode.Multiple;

				List <UISpriteData> theNGUISpritesList = theUISprite.atlas.spriteList;
				SpriteMetaData[] theSheet = new SpriteMetaData[theNGUISpritesList.Count];

				for (int c=0; c<theNGUISpritesList.Count; c++){
					float theY = theUISprite.atlas.texture.height - (theNGUISpritesList[c].y + theNGUISpritesList[c].height);
					theSheet[c].name = theNGUISpritesList[c].name;
					theSheet[c].pivot = new Vector2(theNGUISpritesList[c].paddingLeft, theNGUISpritesList[c].paddingBottom);
					theSheet[c].rect = new Rect (theNGUISpritesList[c].x, theY, theNGUISpritesList[c].width, theNGUISpritesList[c].height);
					theSheet[c].border = new Vector4(theNGUISpritesList[c].borderLeft, theNGUISpritesList[c].borderBottom, theNGUISpritesList[c].borderRight, theNGUISpritesList[c].borderTop);
					theSheet[c].alignment = 0;
					Debug.Log (theSheet[c].name + "       " + theSheet[c].pivot);
				}
				importer.spritesheet = theSheet;
				AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

				//theReplacedUISprite.sprite = (Sprite)AssetDatabase.LoadAssetAtPath ("Assets/CONVERSION_DATA/"+theUISprite.spriteName+".png", typeof(Sprite));


			}
		}else{
			Debug.LogError ("<Color=red>NOTHING SELECTED</Color>, <Color=yellow>Please select something to convert</Color>");
		}
	}
	*/
	[MenuItem ("nGUI TO uGUI/Atlas Convert/Selected")]
	static void OnConvertAtlasSelected () {	
		if (Selection.activeGameObject != null){
			foreach(GameObject selectedObject in Selection.gameObjects){
				if (selectedObject.GetComponent<UIAtlas>()){
					UIAtlas tempNguiAtlas;
					tempNguiAtlas = selectedObject.GetComponent<UIAtlas>();
					if (File.Exists("Assets/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
						Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"CONVERSION_DATA\" </color>Directory");
					}else{
						ConvertAtlas(tempNguiAtlas);
					}
				}
			}
		}else{
			Debug.LogError ("<Color=red>NO ATLASES SELECTED</Color>, <Color=yellow>Please select something to convert</Color>");
		}
	}

	[MenuItem ("nGUI TO uGUI/Atlas Convert/Current Scene")]
	static void OnConvertAtlasesInScene () {
		UISprite[] FoundAtlasesList;
		FoundAtlasesList = GameObject.FindObjectsOfType<UISprite>();
		for (int c=0; c<FoundAtlasesList.Length; c++){
			UIAtlas tempNguiAtlas;
			tempNguiAtlas = FoundAtlasesList[c].atlas;
			if (File.Exists("Assets/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
				Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"CONVERSION_DATA\" </color>Directory");
			}else{
				ConvertAtlas(tempNguiAtlas);
			}
		}
	}

	[MenuItem ("nGUI TO uGUI/Atlas Convert/Related To Selected")]
	static void OnConvertAtlasesFromSelected () {
		if (Selection.activeGameObject != null){
			foreach(GameObject selectedObject in Selection.gameObjects){
				if (selectedObject.GetComponent<UISprite>()){
					UIAtlas tempNguiAtlas;
					tempNguiAtlas = selectedObject.GetComponent<UISprite>().atlas;
					if (File.Exists("Assets/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
						Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"CONVERSION_DATA\" </color>Directory");
					}else{
						ConvertAtlas(tempNguiAtlas);
					}
				}
			}
		}
	}

	/*
	[MenuItem ("nGUI TO uGUI/Atlas Convert/All over the project")]
	static void OnConvertAtlasesAlloverTheProject () {
		AssetDatabase.FindAssets
	}
	*/

	static void ConvertAtlas(UIAtlas theAtlas){
		if(!Directory.Exists("Assets/CONVERSION_DATA")){
			AssetDatabase.CreateFolder ("Assets", "CONVERSION_DATA");
		}else{
			
		}
		AssetDatabase.CopyAsset (AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(theAtlas.name)[0]), "Assets/CONVERSION_DATA/"+theAtlas.name+".png");
		AssetDatabase.Refresh();
		//Debug.Log(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(theAtlas.name)[0]) + "\n" + "Assets/CONVERSION_DATA/"+theAtlas.name+".png");
		
		string conversionPath = "Assets/CONVERSION_DATA/"+theAtlas.name+".png";
		TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(conversionPath);
		importer.textureType = TextureImporterType.Sprite;
		importer.mipmapEnabled = false;
		importer.spriteImportMode = SpriteImportMode.Multiple;
		
		List <UISpriteData> theNGUISpritesList = theAtlas.spriteList;
		SpriteMetaData[] theSheet = new SpriteMetaData[theNGUISpritesList.Count];
		
		for (int c=0; c<theNGUISpritesList.Count; c++){
			float theY = theAtlas.texture.height - (theNGUISpritesList[c].y + theNGUISpritesList[c].height);
			theSheet[c].name = theNGUISpritesList[c].name;
			theSheet[c].pivot = new Vector2(theNGUISpritesList[c].paddingLeft, theNGUISpritesList[c].paddingBottom);
			theSheet[c].rect = new Rect (theNGUISpritesList[c].x, theY, theNGUISpritesList[c].width, theNGUISpritesList[c].height);
			theSheet[c].border = new Vector4(theNGUISpritesList[c].borderLeft, theNGUISpritesList[c].borderBottom, theNGUISpritesList[c].borderRight, theNGUISpritesList[c].borderTop);
			theSheet[c].alignment = 0;
			Debug.Log (theSheet[c].name + "       " + theSheet[c].pivot);
		}
		importer.spritesheet = theSheet;
		AssetDatabase.ImportAsset(conversionPath, ImportAssetOptions.ForceUpdate);
	}



	[MenuItem ("nGUI TO uGUI/Wedgit Convert/Selected")]
	static void OnConvertWedgitSelected () {
		if (Selection.activeGameObject != null){
			foreach(GameObject selectedObject in Selection.gameObjects){
				if (selectedObject.GetComponent<UIButton>() && selectedObject.GetComponent<UISprite>()){
					OnConvertUIButton (selectedObject);
				}
				if (selectedObject.GetComponent<UILabel>()){
					OnConvertUILabel (selectedObject, false);
				}

			}
		}else{
			Debug.LogError ("<Color=red>NO NGUI-Wedgits SELECTED</Color>, <Color=yellow>Please select at least one wedgit to convert</Color>");
		}
	}


	static void OnConvertUILabel(GameObject theHolderObject, bool subConvert){
		GameObject tempObject;
		Text tempText;
		if (subConvert == false){
			tempObject = (GameObject) Instantiate (theHolderObject.gameObject, theHolderObject.transform.position, theHolderObject.transform.rotation);
			tempObject.layer = LayerMask.NameToLayer ("UI");
			if (GameObject.FindObjectOfType<Canvas>()){
				tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
			}else{
				Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
				DestroyImmediate (tempObject.gameObject);
				return;
			}
			tempText = tempObject.AddComponent<Text>();
			tempObject.name = theHolderObject.name;
			//to adjust the text issue
			if (tempObject.GetComponent <UILabel>().overflowMethod == UILabel.Overflow.ResizeHeight){
				tempObject.GetComponent<RectTransform>().pivot = new Vector2(tempObject.GetComponent<RectTransform>().pivot.x, 1.0f);
			}
			tempObject.transform.position = theHolderObject.transform.position;
			tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UILabel>().localSize;
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}else{
			tempObject = theHolderObject;
			tempText = tempObject.gameObject.AddComponent<Text>();
		}

		UILabel originalText = tempObject.GetComponent <UILabel>();
		//tempText = originalText.gameObject.AddComponent<Text>();
		tempText.text = originalText.text;
		tempText.color = originalText.color;
		tempText.gameObject.GetComponent<RectTransform>().sizeDelta = originalText.localSize;
		tempText.font = (Font)AssetDatabase.LoadAssetAtPath("Assets/CONVERSION_DATA/FONTS/"+"FONT.ttf", typeof(Font));
		tempText.fontSize = originalText.fontSize;
		if (originalText.spacingY != 0){
			tempText.lineSpacing = originalText.spacingY;
		}
		
		if (originalText.alignment == NGUIText.Alignment.Automatic){
			tempText.alignment = TextAnchor.MiddleCenter;
		}else if (originalText.alignment == NGUIText.Alignment.Center){
			tempText.alignment = TextAnchor.MiddleCenter;
		}else if (originalText.alignment == NGUIText.Alignment.Justified){
			tempText.alignment = TextAnchor.MiddleLeft;
		}else if (originalText.alignment == NGUIText.Alignment.Left){
			tempText.alignment = TextAnchor.UpperLeft;
		}else if (originalText.alignment == NGUIText.Alignment.Right){
			tempText.alignment = TextAnchor.UpperRight;
		}


		
		if (tempObject.GetComponent<Collider>()){
			DestroyImmediate(tempObject.GetComponent<Collider>());
		}
		DestroyImmediate (originalText);
	}

	static void OnConvertUIButton(GameObject selectedObject){
		GameObject tempObject;
		UIAtlas tempNguiAtlas;
		tempNguiAtlas = selectedObject.GetComponent<UISprite>().atlas;
		if (File.Exists("Assets/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
			Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"CONVERSION_DATA\" </color>Directory");
		}else{
			ConvertAtlas(tempNguiAtlas);
		}
		
		tempObject = (GameObject) Instantiate (selectedObject.gameObject, selectedObject.transform.position, selectedObject.transform.rotation);
		tempObject.layer = LayerMask.NameToLayer ("UI");
		if (GameObject.FindObjectOfType<Canvas>()){
			tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
		}else{
			Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
			DestroyImmediate (tempObject.gameObject);
			return;
		}
		tempObject.name = selectedObject.name;
		tempObject.transform.position = selectedObject.transform.position;
		
		//to easliy control the old and the new sprites and buttons
		Image addedImage;
		Button addedButton;
		UISprite originalSprite;
		UIButton originalButton;
		
		//define the objects of the previous variables
		addedImage = tempObject.AddComponent<Image>();
		addedButton = tempObject.AddComponent<Button>();
		originalSprite = selectedObject.GetComponent<UISprite>();
		originalButton = selectedObject.GetComponent<UIButton>();
		
		//adjust the rect transform to fit the original one's size
		tempObject.GetComponent<RectTransform>().sizeDelta = originalSprite.localSize;
		tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
		Sprite[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/CONVERSION_DATA/" + originalSprite.atlas.name + ".png").OfType<Sprite>().ToArray();
		SpriteState tempState = addedButton.spriteState;
		for (int c=0; c<sprites.Length; c++){
			if (sprites[c].name == originalSprite.spriteName){
				addedImage.sprite = sprites[c];
			}
			//Apply the sprite swap option, just in case the user have it.
			// Used several If statement, just in case a user using the same sprite to define more than one state
			if (sprites[c].name == originalButton.hoverSprite){
				tempState.highlightedSprite = sprites[c];
			}
			if (sprites[c].name == originalButton.pressedSprite){
				tempState.pressedSprite = sprites[c];
			}
			if (sprites[c].name == originalButton.disabledSprite){
				tempState.disabledSprite = sprites[c];
			}
			addedButton.spriteState = tempState;
		}

		//the actions code
		//UnityAction tempActionHolder = (UnityAction)originalButton.onClick[0];
		//addedButton.onClick.AddListener (() => originalButton.onClick[0].target.SendMessage(originalButton.onClick[0].methodName));
		//addedButton.onClick.AddListener (() => originalButton.onClick[0].target.gameObject.SendMessage( originalButton.onClick[0].methodName ));
		//addedButton.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
		//addedButton.onClick.Equals((object)originalButton.onClick[0]);
		//addedButton.onClick.AddListener (delegate() { CheckChildUILabels(addedButton.gameObject); });
		//addedButton.onClick.AddListener  (delegate {Debug.Log("Button " + this.gameObject.name + " has been clicked!");});
		Debug.Log(originalButton.onClick[0].GetType());
		//Debug.Log((originalButton.onClick[0].ToString())+"."+originalButton.onClick[0].methodName.ToString());

		
		// set the image sprite color
		addedImage.color = Color.white;
		
		//set the button colors and the fade duration
		ColorBlock tempColor = addedButton.colors;
		tempColor.normalColor = originalSprite.color;
		tempColor.highlightedColor = originalButton.hover;
		tempColor.pressedColor = originalButton.pressed;
		tempColor.disabledColor = originalButton.disabledColor;
		tempColor.fadeDuration = originalButton.duration;
		addedButton.colors = tempColor;
		
		//if the button is using some sprites, then switch the transitons into the swap type. otherwise, keep it with the color tint!
		if (originalButton.hoverSprite != "" &&
		    originalButton.pressedSprite != "" &&
		    originalButton.disabledSprite != ""){
			//addedButton.transition = Selectable.Transition.SpriteSwap;
			addedButton.transition = Selectable.Transition.ColorTint;
		}else{
			addedButton.transition = Selectable.Transition.ColorTint;
		}

		//set the type of the sprite (with a button it will be usually sliced)
		if (originalSprite.type == UIBasicSprite.Type.Simple){
			addedImage.type = Image.Type.Simple;
		}else if (originalSprite.type == UIBasicSprite.Type.Sliced){
			addedImage.type = Image.Type.Sliced;
		}else if (originalSprite.type == UIBasicSprite.Type.Tiled){
			addedImage.type = Image.Type.Tiled;
		}else if (originalSprite.type == UIBasicSprite.Type.Filled){
			addedImage.type = Image.Type.Filled;
		}
		
		//remcoe the 2 nGUI components from the newly created duplicate
		DestroyImmediate (tempObject.GetComponent<UISprite>());
		DestroyImmediate (tempObject.GetComponent<UIButton>());
		if (tempObject.GetComponent<Collider>()){
			DestroyImmediate (tempObject.GetComponent<Collider>());
		}
		
		//do the calls to change the childerns
		//CheckChildUILabels(tempObject);

		UILabel[] textOnChilds = tempObject.GetComponentsInChildren <UILabel>();
		for (int v=0; v<textOnChilds.Length; v++){
			OnConvertUILabel(textOnChilds[v].gameObject, true);
		}
	}

	//check any UILabel in a child and change it
	static void CheckChildUILabels(GameObject theParentObject){

		/*
		UILabel[] textOnChilds = theParentObject.GetComponentsInChildren <UILabel>();
		for (int v=0; v<textOnChilds.Length; v++){
			Text tempText = textOnChilds[v].gameObject.AddComponent<Text>();
			tempText.text = textOnChilds[v].text;
			tempText.color = textOnChilds[v].color;
			tempText.gameObject.GetComponent<RectTransform>().sizeDelta = textOnChilds[v].localSize;
			tempText.font = (Font)AssetDatabase.LoadAssetAtPath("Assets/CONVERSION_DATA/FONTS/"+"FONT.ttf", typeof(Font));
			tempText.fontSize = textOnChilds[v].fontSize;
			tempText.lineSpacing = textOnChilds[v].spacingY;
			
			if (textOnChilds[v].alignment == NGUIText.Alignment.Automatic){
				tempText.alignment = TextAnchor.MiddleCenter;
			}else if (textOnChilds[v].alignment == NGUIText.Alignment.Center){
				tempText.alignment = TextAnchor.MiddleCenter;
			}else if (textOnChilds[v].alignment == NGUIText.Alignment.Justified){
				tempText.alignment = TextAnchor.MiddleLeft;
			}else if (textOnChilds[v].alignment == NGUIText.Alignment.Left){
				tempText.alignment = TextAnchor.MiddleLeft;
			}else if (textOnChilds[v].alignment == NGUIText.Alignment.Right){
				tempText.alignment = TextAnchor.MiddleRight;
			}
			
			DestroyImmediate (textOnChilds[v]);
			
		}*/
	}

}

// when everything done, the canvas needs to be UnParented, and have the scale of 1, and finally moved to Zero to be viewed by the camera.
