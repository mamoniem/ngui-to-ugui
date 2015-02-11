using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uUISliderColors : MonoBehaviour {

	public Image theSprite;
	
	public Color[] colors = new Color[] { Color.red, Color.yellow, Color.green };
	
	Slider mSlider;
	UIBasicSprite mSprite;

	void Awake(){
		theSprite = mSlider.fillRect.gameObject.GetComponent<Image>();
	}
	void Start ()
	{
		mSlider = GetComponent<Slider>();
		mSprite = GetComponent<UIBasicSprite>();
		Update();
	}
	
	void Update ()
	{
		if (theSprite == null || colors.Length == 0) return;
		
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
		
		c.a = theSprite.color.a;
		theSprite.color = c;
	}
}
