using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uUISliderColors : MonoBehaviour {

	public Image theSpriteHolder;
	
	public Color[] colors = new Color[] { Color.red, Color.yellow, Color.green };
	
	Slider mSlider;
	UIBasicSprite mSprite;

	void Awake(){
		mSlider = this.gameObject.GetComponent<Slider>();
		theSpriteHolder = mSlider.fillRect.gameObject.GetComponent<Image>();
	}
	void Start ()
	{
		mSlider = GetComponent<Slider>();
		mSprite = GetComponent<UIBasicSprite>();
		Update();
	}
	
	void Update ()
	{
		if (theSpriteHolder == null || colors.Length == 0) return;
		
		float val = (mSlider != null) ? mSlider.value : mSprite.fillAmount;
		val *= (colors.Length - 1);
		int startIndex = Mathf.FloorToInt(val);
		
		Color c = colors[0];
		
		if (startIndex >= 0)
		{
			if (startIndex + 1 < colors.Length)
			{
				float factor = (val - startIndex);
				c = Color.Lerp(colors[startIndex], colors[startIndex + 1], factor);
			}
			else if (startIndex < colors.Length)
			{
				c = colors[startIndex];
			}
			else c = colors[colors.Length - 1];
		}
		
		c.a = theSpriteHolder.color.a;
		theSpriteHolder.color = c;
	}
}
