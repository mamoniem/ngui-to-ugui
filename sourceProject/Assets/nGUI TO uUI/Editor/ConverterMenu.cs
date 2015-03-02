// All Rights Reserved, Muhammad A.Moniem (mamoniem) 2015  http://www.mamoniem.com 
// v1.0
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

	#region Convert Atlas Selected
	[MenuItem ("nGUI TO uGUI/Atlas Convert/Selected")]
	static void OnConvertAtlasSelected () {	
		if (Selection.activeGameObject != null){
			foreach(GameObject selectedObject in Selection.gameObjects){
				if (selectedObject.GetComponent<UIAtlas>()){
					UIAtlas tempNguiAtlas;
					tempNguiAtlas = selectedObject.GetComponent<UIAtlas>();
					if (File.Exists("Assets/nGUI TO uUI/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
						Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"nGUI TO uUI/CONVERSION_DATA\" </color>Directory");
					}else{
						ConvertAtlas(tempNguiAtlas);
					}
				}
			}
		}else{
			Debug.LogError ("<Color=red>NO ATLASES SELECTED</Color>, <Color=yellow>Please select something to convert</Color>");
		}
	}
	#endregion

	#region Convert Atlases In Scene
	[MenuItem ("nGUI TO uGUI/Atlas Convert/Current Scene")]
	static void OnConvertAtlasesInScene () {
		UISprite[] FoundAtlasesList;
		FoundAtlasesList = GameObject.FindObjectsOfType<UISprite>();
		for (int c=0; c<FoundAtlasesList.Length; c++){
			UIAtlas tempNguiAtlas;
			tempNguiAtlas = FoundAtlasesList[c].atlas;
			if (File.Exists("Assets/nGUI TO uUI/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
				Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"nGUI TO uUI/CONVERSION_DATA\" </color>Directory");
			}else{
				ConvertAtlas(tempNguiAtlas);
			}
		}
	}
	#endregion

	#region Convert Atlas From Selected
	[MenuItem ("nGUI TO uGUI/Atlas Convert/Related To Selected")]
	static void OnConvertAtlasesFromSelected () {
		if (Selection.activeGameObject != null){
			foreach(GameObject selectedObject in Selection.gameObjects){
				if (selectedObject.GetComponent<UISprite>()){
					UIAtlas tempNguiAtlas;
					tempNguiAtlas = selectedObject.GetComponent<UISprite>().atlas;
					if (File.Exists("Assets/nGUI TO uUI/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
						Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"nGUI TO uUI/CONVERSION_DATA\" </color>Directory");
					}else{
						ConvertAtlas(tempNguiAtlas);
					}
				}
			}
		}
	}
	#endregion

	#region PROCEDURALS Convert Atlas
	static void ConvertAtlas(UIAtlas theAtlas){
		if(!Directory.Exists("Assets/nGUI TO uUI/CONVERSION_DATA")){
			AssetDatabase.CreateFolder ("Assets", "nGUI TO uUI/CONVERSION_DATA");
		}else{
			
		}
		AssetDatabase.CopyAsset (AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(theAtlas.name)[0]), "Assets/nGUI TO uUI/CONVERSION_DATA/"+theAtlas.name+".png");
		AssetDatabase.Refresh();
		//Debug.Log(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(theAtlas.name)[0]) + "\n" + "Assets/nGUI TO uUI/CONVERSION_DATA/"+theAtlas.name+".png");
		
		string conversionPath = "Assets/nGUI TO uUI/CONVERSION_DATA/"+theAtlas.name+".png";
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
	#endregion

	#region Convert Widget Selected
	[MenuItem ("nGUI TO uGUI/Wedgit Convert/Selected")]
	static void OnConvertWedgitSelected () {
		GameObject inProgressObject;
		if (Selection.activeGameObject != null){
			foreach(GameObject selectedObject in Selection.gameObjects){

				inProgressObject = (GameObject) Instantiate (selectedObject, selectedObject.transform.position, selectedObject.transform.rotation);

				if (selectedObject.GetComponent<UIWidget>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUIWidget (inProgressObject, false);
				}

				if (selectedObject.GetComponent<UISprite>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUISprite (inProgressObject, false);
				}

				if (selectedObject.GetComponent<UILabel>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUILabel (inProgressObject, false);
				}

				if (selectedObject.GetComponent<UIToggle>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUIToggle (inProgressObject, false);
				}

				if (selectedObject.GetComponent<UIInput>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUIInput (inProgressObject, false);
				}

				if (selectedObject.GetComponent<UIScrollBar>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUIScrollBar (inProgressObject, false);
				}

				if (selectedObject.GetComponent<UISlider>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUISlider (inProgressObject, false);
				}


				if (selectedObject.GetComponent<UIButton>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUIButton (inProgressObject, false);
				}

#if PopupLists
				if (selectedObject.GetComponent<UIPopupList>()){
					inProgressObject.name = selectedObject.name;
					OnConvertUIButton (inProgressObject, false);
				}
#endif

				UIWidget[] UIWidgetsOnChilderens = inProgressObject.GetComponentsInChildren<UIWidget>();
				UISprite[] UISpritesOnChilderens = inProgressObject.GetComponentsInChildren<UISprite>();
				UILabel[] UILablesOnChilderens = inProgressObject.GetComponentsInChildren<UILabel>();
				UIButton[] UIButtonsOnChilderens = inProgressObject.GetComponentsInChildren<UIButton>();
				UIToggle[] UITogglesOnChilderens = inProgressObject.GetComponentsInChildren<UIToggle>();
				UIInput[] UIInputsOnChilderens = inProgressObject.GetComponentsInChildren<UIInput>();
				UIScrollBar[] UIScrollBarsOnChilderens = inProgressObject.GetComponentsInChildren<UIScrollBar>();
				UISlider[] UISlidersOnChilderens = inProgressObject.GetComponentsInChildren<UISlider>();
#if PopupLists
				UIPopupList[] UIPopuplistsOnChilderens = inProgressObject.GetComponentsInChildren<UIPopupList>();
#endif

				for (int a=0; a<UIWidgetsOnChilderens.Length; a++){
					if (!UIWidgetsOnChilderens[a].gameObject.GetComponent<RectTransform>()){
						OnConvertUIWidget (UIWidgetsOnChilderens[a].gameObject, true);
					}
				}

				for (int b=0; b<UISpritesOnChilderens.Length; b++){
					OnConvertUISprite (UISpritesOnChilderens[b].gameObject, true);
				}

				for (int c=0; c<UILablesOnChilderens.Length; c++){
					OnConvertUILabel (UILablesOnChilderens[c].gameObject, true);
				}

				for (int d=0; d<UIButtonsOnChilderens.Length; d++){
					OnConvertUIButton (UIButtonsOnChilderens[d].gameObject, true);
				}

				for (int e=0; e<UITogglesOnChilderens.Length; e++){
					OnConvertUIToggle (UITogglesOnChilderens[e].gameObject, true);
				}

				for (int f=0; f<UIInputsOnChilderens.Length; f++){
					OnConvertUIInput (UIInputsOnChilderens[f].gameObject, true);
				}
				for (int g=0; g<UIScrollBarsOnChilderens.Length; g++){
					OnConvertUIScrollBar (UIScrollBarsOnChilderens[g].gameObject, true);
				}
				for (int h=0; h<UISlidersOnChilderens.Length; h++){
					OnConvertUISlider (UISlidersOnChilderens[h].gameObject, true);
				}
#if PopupLists
				for (int i=0; i<UIPopuplistsOnChilderens.Length; i++){
					OnConvertUIPopuplist (UIPopuplistsOnChilderens[i].gameObject, true);
				}
#endif
				OnAdjustSliders(inProgressObject);
				OnCleanConvertedItem(GameObject.FindObjectOfType<Canvas>().gameObject);
			}
		}else{
			Debug.LogError ("<Color=red>NO NGUI-Wedgits SELECTED</Color>, <Color=yellow>Please select at least one wedgit to convert</Color>");
		}
	}
	#endregion

	#region UIWidgets Converter
	static void OnConvertUIWidget(GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;

		tempObject = selectedObject;

		tempObject.layer = LayerMask.NameToLayer ("UI");
		if (!isSubConvert){
			if (GameObject.FindObjectOfType<Canvas>()){
				tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
			}else{
				Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
				DestroyImmediate (tempObject.gameObject);
				return;
			}
		}

		tempObject.name = selectedObject.name;
		tempObject.transform.position = selectedObject.transform.position;

		RectTransform addedRectT;

		addedRectT = tempObject.AddComponent<RectTransform>();

		tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UIWidget>().pivotOffset;
		tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UIWidget>().localSize;
		tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}
	#endregion

	#region UISprites Converter
	static void OnConvertUISprite(GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;

		UIAtlas tempNguiAtlas;
		tempNguiAtlas = selectedObject.GetComponent<UISprite>().atlas;
		if (File.Exists("Assets/nGUI TO uUI/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
			Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"nGUI TO uUI/CONVERSION_DATA\" </color>Directory");
		}else{
			ConvertAtlas(tempNguiAtlas);
		}

		tempObject = selectedObject;
		tempObject.layer = LayerMask.NameToLayer ("UI");
		if (!isSubConvert){
			if (GameObject.FindObjectOfType<Canvas>()){
				tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
			}else{
				Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
				DestroyImmediate (tempObject.gameObject);
				return;
			}
		}
		tempObject.name = selectedObject.name;
		tempObject.transform.position = selectedObject.transform.position;
		
		//to easliy control the old and the new sprites and buttons
		Image addedImage;
		UISprite originalSprite;

		//define the objects of the previous variables
		if (tempObject.GetComponent<Image>()){
			addedImage = tempObject.GetComponent<Image>();
		}else{
			addedImage = tempObject.AddComponent<Image>();
		}
		originalSprite = selectedObject.GetComponent<UISprite>();

		//adjust the rect transform to fit the original one's size
		tempObject.GetComponent<RectTransform>().pivot = originalSprite.pivotOffset;
		tempObject.GetComponent<RectTransform>().sizeDelta = originalSprite.localSize;
		tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
		Sprite[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/nGUI TO uUI/CONVERSION_DATA/" + originalSprite.atlas.name + ".png").OfType<Sprite>().ToArray();
		for (int c=0; c<sprites.Length; c++){
			if (sprites[c].name == originalSprite.spriteName){
				addedImage.sprite = sprites[c];
			}

		}
		
		// set the image sprite color
		if (addedImage.gameObject.GetComponent<UIButton>()){
			addedImage.color = Color.white;
		}else{
			addedImage.color = originalSprite.color;
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


		//check if the parent was converted into a slider
		/*if (tempObject.transform.GetComponentInParent<Slider>() && !tempObject.gameObject.GetComponent<Button>()){
			Debug.Log("THE NAME :: "+ tempObject.name);
			tempObject.transform.GetComponentInParent<Slider>().fillRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, 0);
			tempObject.transform.GetComponentInParent<Slider>().fillRect.gameObject.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, 0);

		}*/
	}
	#endregion

	#region UILabels Converter
	static void OnConvertUILabel(GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;
		Text tempText;

		tempObject = selectedObject;
		tempObject.layer = LayerMask.NameToLayer ("UI");
		if (!isSubConvert){
			if (GameObject.FindObjectOfType<Canvas>()){
				tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
			}else{
				Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
				DestroyImmediate (tempObject.gameObject);
				return;
			}
		}
		tempText = tempObject.AddComponent<Text>();
		tempObject.name = selectedObject.name;
		//to adjust the text issue
		if (tempObject.GetComponent <UILabel>().overflowMethod == UILabel.Overflow.ResizeHeight){
			tempObject.GetComponent<RectTransform>().pivot = new Vector2(tempObject.GetComponent<RectTransform>().pivot.x, 1.0f);
		}
		//tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UILabel>().pivotOffset;
		tempObject.transform.position = selectedObject.transform.position;
		tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UILabel>().localSize;
		tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);


		UILabel originalText = tempObject.GetComponent <UILabel>();
		if (tempText != null){
			//tempText = originalText.gameObject.AddComponent<Text>();
			tempText.text = originalText.text;
			tempText.color = originalText.color;
			tempText.gameObject.GetComponent<RectTransform>().sizeDelta = originalText.localSize;
			tempText.font = (Font)AssetDatabase.LoadAssetAtPath("Assets/nGUI TO uUI/CONVERSION_DATA/FONTS/"+originalText.bitmapFont.name+".ttf", typeof(Font));
			tempText.fontSize = originalText.fontSize-2;
			if (originalText.spacingY != 0){
				tempText.lineSpacing = 1 /*originalText.spacingY*/;
			}
			
			if (originalText.alignment == NGUIText.Alignment.Automatic){
				if (originalText.gameObject.transform.parent.gameObject.GetComponent<UIButton>() || originalText.gameObject.transform.parent.gameObject.GetComponent<Button>()){
					tempText.alignment = TextAnchor.MiddleCenter;
				}else{
					tempText.alignment = TextAnchor.UpperLeft;
				}
			}else if (originalText.alignment == NGUIText.Alignment.Center){
				tempText.alignment = TextAnchor.MiddleCenter;
			}else if (originalText.alignment == NGUIText.Alignment.Justified){
				tempText.alignment = TextAnchor.MiddleLeft;
			}else if (originalText.alignment == NGUIText.Alignment.Left){
				tempText.alignment = TextAnchor.UpperLeft;
			}else if (originalText.alignment == NGUIText.Alignment.Right){
				tempText.alignment = TextAnchor.UpperRight;
			}


		}

		if (originalText.gameObject.GetComponent<TypewriterEffect>()){
			uUITypewriterEffect tempWriterEffect = 
			originalText.gameObject.AddComponent<uUITypewriterEffect>();
			DestroyImmediate(originalText.gameObject.GetComponent<TypewriterEffect>());
		}

	}
	#endregion

	#region UIButtons Converter
	static void OnConvertUIButton(GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;
		tempObject = selectedObject;

		/*
		UIAtlas tempNguiAtlas;
		tempNguiAtlas = selectedObject.GetComponent<UISprite>().atlas;
		if (File.Exists("Assets/nGUI TO uUI/CONVERSION_DATA/"+tempNguiAtlas.name+".png")){
			Debug.Log ("The Atlas <color=yellow>" + tempNguiAtlas.name + " </color>was Already Converted, Check the<color=yellow> \"nGUI TO uUI/CONVERSION_DATA\" </color>Directory");
		}else{
			ConvertAtlas(tempNguiAtlas);
		}
		*/
		if (tempObject.GetComponent<Scrollbar>() || tempObject.GetComponent<Slider>()){

		}else{
			tempObject.layer = LayerMask.NameToLayer ("UI");
			if (!isSubConvert){
				if (GameObject.FindObjectOfType<Canvas>()){
					tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
				}else{
					Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
					DestroyImmediate (tempObject.gameObject);
					return;
				}
			}
			tempObject.transform.position = selectedObject.transform.position;
			
			//to easliy control the old and the new sprites and buttons
			Button addedButton;
			UIButton originalButton;
			
			//define the objects of the previous variables
			if (tempObject.GetComponent<Button>()){
				addedButton = tempObject.GetComponent<Button>();
			}else{
				addedButton = tempObject.AddComponent<Button>();
			}
			originalButton = selectedObject.GetComponent<UIButton>();
			
			//adjust the rect transform to fit the original one's size..If it have no sprite, then it must had a widget
			if (tempObject.GetComponent<RectTransform>()){

			}else{
				tempObject.AddComponent<RectTransform>();
			}

			if (originalButton.GetComponent<UISprite>()){
				tempObject.GetComponent<RectTransform>().sizeDelta = originalButton.GetComponent<UISprite>().localSize;
				tempObject.GetComponent<RectTransform>().pivot = originalButton.GetComponent<UISprite>().pivotOffset;
			}else if (originalButton.GetComponent<UIWidget>()){
				tempObject.GetComponent<RectTransform>().sizeDelta = originalButton.GetComponent<UIWidget>().localSize;
				tempObject.GetComponent<RectTransform>().pivot = originalButton.GetComponent<UIWidget>().pivotOffset;
			}else{
				tempObject.GetComponent<RectTransform>().sizeDelta = originalButton.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().localSize;
				tempObject.GetComponent<RectTransform>().pivot = originalButton.GetComponent<UIButton>().tweenTarget.GetComponent<UISprite>().pivotOffset;
			}
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

			//if the object ahve no UISprites, then a sub object must have!
			Sprite[] sprites;
			if (originalButton.GetComponent<UISprite>()){
				sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/nGUI TO uUI/CONVERSION_DATA/" + originalButton.GetComponent<UISprite>().atlas.name + ".png").OfType<Sprite>().ToArray();
			}else{
				sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/nGUI TO uUI/CONVERSION_DATA/" + originalButton.gameObject.GetComponentInChildren<UISprite>().atlas.name + ".png").OfType<Sprite>().ToArray();
			}

			if (tempObject.gameObject.GetComponent<UIToggle>()){

			}else{
				SpriteState tempState = addedButton.spriteState;
				for (int c=0; c<sprites.Length; c++){
					//Apply the sprite swap option, just in case the user have it. // Used several If statement, just in case a user using the same sprite to define more than one state
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
			}
			
			//set the button colors and the fade duration
			if (originalButton.GetComponent<UISprite>()){
				ColorBlock tempColor = addedButton.colors;
				tempColor.normalColor = originalButton.GetComponent<UISprite>().color;
				tempColor.highlightedColor = originalButton.hover;
				tempColor.pressedColor = originalButton.pressed;
				tempColor.disabledColor = originalButton.disabledColor;
				tempColor.fadeDuration = originalButton.duration;
				addedButton.colors = tempColor;
			}

			if (tempObject.gameObject.GetComponent<UIToggle>()){

			}else{
				//if the button is using some sprites, then switch the transitons into the swap type. otherwise, keep it with the color tint!
				if (originalButton.hoverSprite != "" &&
				    originalButton.pressedSprite != "" &&
				    originalButton.disabledSprite != ""){
					//addedButton.transition = Selectable.Transition.SpriteSwap;
					addedButton.transition = Selectable.Transition.ColorTint;
				}else{
					addedButton.transition = Selectable.Transition.ColorTint;
				}
			}

			//check if the parent was converted into a scrollbar
			if (tempObject.transform.GetComponentInParent<Scrollbar>()){
				tempObject.transform.GetComponentInParent<Scrollbar>().handleRect = tempObject.GetComponent<RectTransform>();
				tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(tempObject.GetComponent<UISprite>().rightAnchor.absolute*2
				                                                                 ,tempObject.GetComponent<UISprite>().topAnchor.absolute*2);
			}

			//check if the parent was converted into a slider
			if (tempObject.transform.GetComponentInParent<Slider>()){
				tempObject.transform.GetComponentInParent<Slider>().handleRect = tempObject.GetComponent<RectTransform>();
				if (tempObject.transform.GetComponentInParent<Slider>().direction == Slider.Direction.LeftToRight ||  tempObject.transform.GetComponentInParent<Slider>().direction == Slider.Direction.RightToLeft){
					tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(tempObject.GetComponent<UISprite>().localSize.x)
					                                                                 ,Mathf.Abs(tempObject.GetComponent<UISprite>().topAnchor.absolute*2));
				}else{
					tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(tempObject.GetComponent<UISprite>().leftAnchor.absolute*2),
					                                                                 Mathf.Abs(tempObject.GetComponent<UISprite>().localSize.y));
				}
			}

		}
	}
	#endregion

	#region UIToggles Converter
	static void OnConvertUIToggle (GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;

		tempObject = selectedObject;

		if (tempObject.GetComponent<uUIToggle>()){
			return;
		}else{
			tempObject.layer = LayerMask.NameToLayer ("UI");
			if (!isSubConvert){
				if (GameObject.FindObjectOfType<Canvas>()){
					tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
				}else{
					Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
					DestroyImmediate (tempObject.gameObject);
					return;
				}
			}
			
			tempObject.name = selectedObject.name;
			tempObject.transform.position = selectedObject.transform.position;
			
			Toggle addedToggle;
			uUIToggle addedToggleController;
			
			addedToggle = tempObject.AddComponent<Toggle>();
			addedToggleController = tempObject.AddComponent<uUIToggle>();

			tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UIWidget>().pivotOffset;
			tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UIWidget>().localSize;
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);


			//addedToggle

			addedToggleController.Group = tempObject.GetComponent<UIToggle>().group;
			addedToggleController.StateOfNone = tempObject.GetComponent<UIToggle>().optionCanBeNone;
			addedToggleController.startingState = tempObject.GetComponent<UIToggle>().startsActive;

			UISprite[] childImages;
			childImages = tempObject.GetComponentsInChildren<UISprite>(); //not using <Image>() because the child have not been converted yet
			for (int x=0; x< childImages.Length; x++){
				if (childImages[x].spriteName == tempObject.GetComponent<UIToggle>().activeSprite.gameObject.GetComponent<UISprite>().spriteName){
					Sprite[] sprites;
					sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/nGUI TO uUI/CONVERSION_DATA/" + childImages[x].atlas.name + ".png").OfType<Sprite>().ToArray();
					for (int c=0; c<sprites.Length; c++){
						if (sprites[c].name == childImages[x].spriteName){
							addedToggleController.m_Sprite = sprites[c];
						}
					}
				}
			}
			addedToggleController.m_Animation = tempObject.GetComponent<UIToggle>().activeAnimation;
		}
	}
	#endregion

	#region UIInput Converter
	static void OnConvertUIInput (GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;
		InputField newInputField;
		tempObject = selectedObject;

		if (tempObject.GetComponent<InputField>()){

		}else{
			tempObject.layer = LayerMask.NameToLayer ("UI");
			
			if (!isSubConvert){
				if (GameObject.FindObjectOfType<Canvas>()){
					tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
				}else{
					Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
					DestroyImmediate (tempObject.gameObject);
					return;
				}
			}
			
			newInputField = tempObject.AddComponent<InputField>();
			//tempObject.name = selectedObject.name;
			
			//tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UILabel>().pivotOffset;
			tempObject.transform.position = selectedObject.transform.position;
			tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UIWidget>().localSize;
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
			
			ColorBlock tempColor = newInputField.colors;
			tempColor.normalColor = tempObject.GetComponent<UIInput>().activeTextColor;
			tempColor.pressedColor = tempObject.GetComponent<UIInput>().caretColor;
			tempColor.highlightedColor = tempObject.GetComponent<UIInput>().selectionColor;
			//mising the disabled/inactive
			newInputField.colors = tempColor;
			
			newInputField.text = tempObject.GetComponent<UIInput>().value;
			newInputField.characterLimit = tempObject.GetComponent<UIInput>().characterLimit;
			newInputField.textComponent = newInputField.gameObject.GetComponentInChildren<Text>();
			
			if (tempObject.GetComponent<UIInput>().inputType == UIInput.InputType.Standard){
				newInputField.contentType = InputField.ContentType.Standard;
			}else if (tempObject.GetComponent<UIInput>().inputType == UIInput.InputType.AutoCorrect){
				newInputField.contentType = InputField.ContentType.Autocorrected;
			}else if (tempObject.GetComponent<UIInput>().inputType == UIInput.InputType.Password){
				newInputField.contentType = InputField.ContentType.Password;
			}else if (tempObject.GetComponent<UIInput>().validation == UIInput.Validation.Integer){
				newInputField.contentType = InputField.ContentType.IntegerNumber;
			}else if (tempObject.GetComponent<UIInput>().validation == UIInput.Validation.Float){
				newInputField.contentType = InputField.ContentType.DecimalNumber;
			}else if (tempObject.GetComponent<UIInput>().validation == UIInput.Validation.Alphanumeric){
				newInputField.contentType = InputField.ContentType.Alphanumeric;
			}else if (tempObject.GetComponent<UIInput>().validation == UIInput.Validation.Username){
				newInputField.contentType = InputField.ContentType.EmailAddress;
			}else if (tempObject.GetComponent<UIInput>().validation == UIInput.Validation.Name){
				newInputField.contentType = InputField.ContentType.Name;	
			}else if (tempObject.GetComponent<UIInput>().validation == UIInput.Validation.None){
				newInputField.contentType = InputField.ContentType.Custom;
			}
			
			Debug.Log ("UIInput have been done !!!!");
			//newInputField.colors
		}
	}
	#endregion

	#region UIScrollBar Converter
	static void OnConvertUIScrollBar(GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;
		Scrollbar newScrollbar;
		tempObject = selectedObject;
		
		if (tempObject.GetComponent<Scrollbar>()){
			
		}else{
			tempObject.layer = LayerMask.NameToLayer ("UI");
			
			if (!isSubConvert){
				if (GameObject.FindObjectOfType<Canvas>()){
					tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
				}else{
					Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
					DestroyImmediate (tempObject.gameObject);
					return;
				}
			}
			
			newScrollbar = tempObject.AddComponent<Scrollbar>();
			//tempObject.name = selectedObject.name;

			if (tempObject.GetComponent<UIButton>()){
				DestroyImmediate (tempObject.GetComponent<UIButton>());
			}
			//tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UILabel>().pivotOffset;
			tempObject.transform.position = selectedObject.transform.position;
			tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UIWidget>().localSize;
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

			UIScrollBar oldScrollbar = selectedObject.GetComponent<UIScrollBar>();

			/* // replaced with an assignment on the end of the buttons conversion
			newScrollbar.handleRect = newScrollbar.gameObject.transform.FindChild(oldScrollbar.foregroundWidget.name).gameObject.GetComponent<RectTransform>();
			*/

			newScrollbar.numberOfSteps = oldScrollbar.numberOfSteps;
			newScrollbar.value = oldScrollbar.value;
			newScrollbar.size = oldScrollbar.barSize;
			if(oldScrollbar.fillDirection == UIProgressBar.FillDirection.BottomToTop){
				newScrollbar.direction = Scrollbar.Direction.BottomToTop;
			}else if(oldScrollbar.fillDirection == UIProgressBar.FillDirection.LeftToRight){
				newScrollbar.direction = Scrollbar.Direction.LeftToRight;
			}else if(oldScrollbar.fillDirection == UIProgressBar.FillDirection.RightToLeft){
				newScrollbar.direction = Scrollbar.Direction.RightToLeft;
			}else if(oldScrollbar.fillDirection == UIProgressBar.FillDirection.TopToBottom){
				newScrollbar.direction = Scrollbar.Direction.TopToBottom;
			}

			for (int x=0; x<selectedObject.GetComponent<UIScrollBar>().onChange.Capacity; x++){
				if (selectedObject.GetComponent<UIScrollBar>().onChange[x].methodName == "SetCurrentPercent"){
					//Debug.Log ("<Color=blue> HERE </Color>");
					selectedObject.GetComponentInChildren<UILabel>().gameObject.AddComponent<uUIGetScrollPercentageValue>();
					selectedObject.GetComponentInChildren<uUIGetScrollPercentageValue>().scrollBarObject = selectedObject.GetComponent<Scrollbar>();
				}
			}

		}
	}
	#endregion

	#region UISlider Converter
	static void OnConvertUISlider (GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;
		Slider newSlider;
		tempObject = selectedObject;
		
		if (tempObject.GetComponent<Slider>()){
			
		}else{
			tempObject.layer = LayerMask.NameToLayer ("UI");
			
			if (!isSubConvert){
				if (GameObject.FindObjectOfType<Canvas>()){
					tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
				}else{
					Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
					DestroyImmediate (tempObject.gameObject);
					return;
				}
			}
			
			newSlider = tempObject.AddComponent<Slider>();
			//tempObject.name = selectedObject.name;
			
			if (tempObject.GetComponent<UIButton>()){
				DestroyImmediate (tempObject.GetComponent<UIButton>());
			}
			//tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UILabel>().pivotOffset;
			tempObject.transform.position = selectedObject.transform.position;
			tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UIWidget>().localSize;
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
			
			UISlider oldSlider = selectedObject.GetComponent<UISlider>();

			//witht the fact of the ngui limitations of 0:1
			if (newSlider){
				newSlider.minValue = 0;
				newSlider.maxValue = 1;
				newSlider.value = oldSlider.value;

				if(oldSlider.fillDirection == UIProgressBar.FillDirection.BottomToTop){
					newSlider.direction = Slider.Direction.BottomToTop;
				}else if(oldSlider.fillDirection == UIProgressBar.FillDirection.LeftToRight){
					newSlider.direction = Slider.Direction.LeftToRight;
				}else if(oldSlider.fillDirection == UIProgressBar.FillDirection.RightToLeft){
					newSlider.direction = Slider.Direction.RightToLeft;
				}else if(oldSlider.fillDirection == UIProgressBar.FillDirection.TopToBottom){
					newSlider.direction = Slider.Direction.TopToBottom;
				}

				for (int x=0; x<tempObject.GetComponent<UISlider>().onChange.Capacity; x++){
					if (tempObject.GetComponent<UISlider>().onChange[x].methodName == "SetCurrentPercent"){
						//Debug.Log ("<Color=blue> HERE </Color>");
						tempObject.GetComponentInChildren<UILabel>().gameObject.AddComponent<uUIGetSliderPercentageValue>();
						tempObject.GetComponentInChildren<uUIGetSliderPercentageValue>().sliderObject = tempObject.GetComponent<Slider>();
					}
				}
				
				GameObject theForgroundObject;
				theForgroundObject = oldSlider.foregroundWidget.gameObject;
				theForgroundObject.AddComponent<RectTransform>();
				theForgroundObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
				theForgroundObject.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
				newSlider.fillRect = theForgroundObject.GetComponent<RectTransform>();
				
				
				GameObject theThumb;
				theThumb = oldSlider.thumb.gameObject;
				float theTempPosition = oldSlider.thumb.gameObject.transform.position.x;
				theThumb.gameObject.AddComponent<RectTransform>();
				Vector3 tempPos = theThumb.gameObject.GetComponent<RectTransform>().localPosition;
				tempPos.x *= 0;
				tempPos.y *= 0;
				theThumb.gameObject.GetComponent<RectTransform>().localPosition = tempPos;
				newSlider.handleRect = theThumb.gameObject.GetComponent<RectTransform>();

				if (newSlider.gameObject.GetComponent<UISliderColors>()){
					UISliderColors oldSliderColors = newSlider.gameObject.GetComponent<UISliderColors>();
					uUISliderColors newSliderColors =  newSlider.gameObject.AddComponent<uUISliderColors>();
				}
			}
		}
	}
	#endregion

	#region UIPopuplist Converter
	static void OnConvertUIPopuplist(GameObject selectedObject, bool isSubConvert){
		GameObject tempObject;
		uUIPopupList newPopuplist;
		tempObject = selectedObject;
		
		if (tempObject.GetComponent<uUIPopupList>()){
			
		}else{
			tempObject.layer = LayerMask.NameToLayer ("UI");
			
			if (!isSubConvert){
				if (GameObject.FindObjectOfType<Canvas>()){
					tempObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
				}else{
					Debug.LogError ("<Color=red>The is no CANVAS in the scene</Color>, <Color=yellow>Please Add a canvas and adjust it</Color>");
					DestroyImmediate (tempObject.gameObject);
					return;
				}
			}
			
			newPopuplist = tempObject.AddComponent<uUIPopupList>();

			//tempObject.GetComponent<RectTransform>().pivot = tempObject.GetComponent<UILabel>().pivotOffset;
			tempObject.transform.position = selectedObject.transform.position;
			tempObject.GetComponent<RectTransform>().sizeDelta = tempObject.GetComponent<UIWidget>().localSize;
			tempObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
			
			UIPopupList oldPopuplist = selectedObject.GetComponent<UIPopupList>();

			if (newPopuplist){
				newPopuplist.theItemsList = oldPopuplist.items;
				newPopuplist.theItemSample = oldPopuplist.GetComponentInChildren<UILabel>().gameObject;
				newPopuplist.selection = newPopuplist.theItemsList[0];
				newPopuplist.canChangeTitle = true;

				newPopuplist.theItemSample.gameObject.AddComponent<uUIListItem>();
				newPopuplist.theItemSample.gameObject.AddComponent<Button>();


			}
		}
	}
	#endregion
	
	#region AdjustSliders Components
	static void OnAdjustSliders (GameObject selectedObject){
		if (selectedObject.GetComponent<Slider>()){
			Vector3 tempPos = selectedObject.GetComponent<Slider>().fillRect.gameObject.GetComponent<RectTransform>().localPosition;
			tempPos.x *= 0;
			tempPos.y *= 0;
			selectedObject.GetComponent<Slider>().fillRect.gameObject.GetComponent<RectTransform>().localPosition = tempPos;
			selectedObject.GetComponent<Slider>().fillRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

			if (selectedObject.GetComponent<Slider>().handleRect.gameObject.GetComponent<UISprite>()){
				if (selectedObject.GetComponent<Slider>().direction == Slider.Direction.LeftToRight || selectedObject.GetComponent<Slider>().direction == Slider.Direction.RightToLeft){
					selectedObject.GetComponent<Slider>().handleRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(selectedObject.GetComponent<Slider>().handleRect.sizeDelta.x
					                                                                                                                  ,-(selectedObject.GetComponent<Slider>().handleRect.gameObject.GetComponent<UISprite>().bottomAnchor.absolute*2));
				}else{
					selectedObject.GetComponent<Slider>().handleRect.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(-(selectedObject.GetComponent<Slider>().handleRect.gameObject.GetComponent<UISprite>().leftAnchor.absolute*2),
					                                                                                                                  selectedObject.GetComponent<Slider>().handleRect.sizeDelta.y);
				}
			}

			if (selectedObject.GetComponent<Slider>().fillRect.gameObject.GetComponent<UISprite>()){
				selectedObject.GetComponent<Slider>().fillRect.gameObject.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, 0);
			}
		}
	}
	#endregion

	#region Cleaner
	static void OnCleanConvertedItem (GameObject selectedObject){

		UIWidget[] UIWidgetsOnChilderens = selectedObject.GetComponentsInChildren<UIWidget>();
		UISprite[] UISpritesOnChilderens = selectedObject.GetComponentsInChildren<UISprite>();
		UILabel[] UILablesOnChilderens = selectedObject.GetComponentsInChildren<UILabel>();
		UIButton[] UIButtonsOnChilderens = selectedObject.GetComponentsInChildren<UIButton>();
		UIToggle[] UITogglesOnChilderens = selectedObject.GetComponentsInChildren<UIToggle>();
		UIInput[] UIInputsOnChilderens = selectedObject.GetComponentsInChildren<UIInput>();
		UIScrollBar[] UIScrollBarsOnChilderens = selectedObject.GetComponentsInChildren<UIScrollBar>();
		UISlider[] UISlidersOnChilderens = selectedObject.GetComponentsInChildren<UISlider>();
#if PopupLists
		UIPopupList[] UIPopuplistsOnChilderens = selectedObject.GetComponentsInChildren<UIPopupList>();
#endif

		Collider[] CollidersOnChilderens = selectedObject.GetComponentsInChildren<Collider>();

		for (int a=0; a<UIWidgetsOnChilderens.Length; a++){
			if (UIWidgetsOnChilderens[a]){
				DestroyImmediate (UIWidgetsOnChilderens[a]);
			}
		}
		
		for (int b=0; b<UISpritesOnChilderens.Length; b++){
			if (UISpritesOnChilderens[b]){
				DestroyImmediate (UISpritesOnChilderens[b]);
			}
		}
		
		for (int c=0; c<UILablesOnChilderens.Length; c++){
			if (UILablesOnChilderens[c]){
				DestroyImmediate (UILablesOnChilderens[c]);
			}
		}
		
		for (int d=0; d<UIButtonsOnChilderens.Length; d++){
			if (UIButtonsOnChilderens[d]){
				DestroyImmediate (UIButtonsOnChilderens[d]);
			}
		}
		
		for (int e=0; e<UITogglesOnChilderens.Length; e++){
			if(UITogglesOnChilderens[e]){
				DestroyImmediate (UITogglesOnChilderens[e]);
			}
		}

		for (int f=0; f<UIInputsOnChilderens.Length; f++){
			if (UIInputsOnChilderens[f]){
				DestroyImmediate (UIInputsOnChilderens[f]);
			}
		}

		for (int g=0; g<UIScrollBarsOnChilderens.Length; g++){
			if (UIScrollBarsOnChilderens[g]){
				DestroyImmediate (UIScrollBarsOnChilderens[g]);
			}
		}

		for (int h=0; h<UISlidersOnChilderens.Length; h++){
			if (UISlidersOnChilderens[h]){
				if (UISlidersOnChilderens[h].GetComponent<UISliderColors>()){
					DestroyImmediate (UISlidersOnChilderens[h].gameObject.GetComponent<UISliderColors>());
				}
				DestroyImmediate (UISlidersOnChilderens[h]);
			}
		}
#if PopupLists
		for (int h=0; h<UIPopuplistsOnChilderens.Length; h++){
			if (UIPopuplistsOnChilderens[h]){
				DestroyImmediate (UIPopuplistsOnChilderens[h]);
			}
		}
#endif
		for (int z=0; z<CollidersOnChilderens.Length; z++){
			if (CollidersOnChilderens[z]){
				DestroyImmediate (CollidersOnChilderens[z]);
			}
		}


		GameObject[] allTrash;
		allTrash = GameObject.FindObjectsOfType<GameObject>();
		for (int Z=0; Z<allTrash.Length; Z++){
			if (allTrash[Z].gameObject.name.Contains("NGUI Snapshot") && allTrash[Z].gameObject.transform.GetComponentInParent<RectTransform>()){
				DestroyImmediate (allTrash[Z].gameObject);
			}
		}
		Debug.Log ("<Color=blue> Cleaned all the <Color=Red>NGUISnapshot</Color> Objects in the scene Hierarchy</Color>");


	}
	#endregion

}

// May be when everything done, the canvas needs to be UnParented, and have the scale of 1, and finally moved to Zero to be viewed by the camera.
