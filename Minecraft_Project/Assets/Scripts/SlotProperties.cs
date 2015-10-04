using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlotProperties : MonoBehaviour {

	public int itemID;
	public bool isTaken = false;
	Image image;
	RectTransform rectTransform;
	public Sprite invisSprite;
	public Sprite objectSprite;
	public Color mouseOverColor;
	Color myColor;


	// Use this for initialization
	void Start () {
		myColor = gameObject.GetComponent<Image> ().color;
		image = transform.GetChild(0).GetComponent<Image>();
		rectTransform = transform.GetChild(0).GetComponent<RectTransform> ();
		rectTransform.anchorMax = new Vector2(.92f , .92f);
		rectTransform.anchorMin = new Vector2(.08f , .08f);
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
		image.sprite = invisSprite;
		transform.GetChild (0).gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isTaken && image.sprite.name != invisSprite.name)
		{
			image.sprite = invisSprite;
		}
	}

	public void MouseOver()
	{
		gameObject.GetComponent<Image> ().color = mouseOverColor;
	}

	public void MouseExit()
	{
		gameObject.GetComponent<Image> ().color = myColor;
	}

	public void UpdateItemSprite(int itemID)
	{
		// set my image sprite based off the item id and a Dictionary and then get that items sprite
	}

}
