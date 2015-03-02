// All Rights Reserved, Muhammad A.Moniem (mamoniem) 2015  http://www.mamoniem.com 
// v1.0
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class uUIPopupList : MonoBehaviour {

	public string selection;
	public Text selectedName;

	public GameObject theList;
	public bool isAppear;
    public bool canChangeTitle;

    public GameObject theItemSample;
    public List<string> theItemsList;
    private RectTransform[] scrollListItemsObjects;

    public float distanceBetweenItems;

	void Awake(){
		OnUpdateList();
		isAppear = false;

	}

    void OnUpdateList(){
        if (theItemsList.Count != 0){
            scrollListItemsObjects = new RectTransform[theItemsList.Count];
            GameObject processingMenuItem;
            processingMenuItem = (GameObject) Instantiate (theItemSample.gameObject, theItemSample.transform.position, theItemSample.transform.rotation);
            processingMenuItem.GetComponentInChildren<Text>().text = theItemsList[0];
            processingMenuItem.gameObject.name = theItemsList[0];
            scrollListItemsObjects[0] = processingMenuItem.GetComponent<RectTransform>();
            scrollListItemsObjects[0].SetParent(theItemSample.transform.parent);
            for (int c=1; c<theItemsList.Count; c++){
                Vector3 theNewPositon = new Vector3 (scrollListItemsObjects[c-1].transform.position.x,
                                                     scrollListItemsObjects[c-1].transform.position.y - (scrollListItemsObjects[c-1].rect.height+distanceBetweenItems), 
                                                     scrollListItemsObjects[c-1].transform.position.z);
                processingMenuItem = (GameObject) Instantiate (theItemSample.gameObject, theNewPositon, theItemSample.transform.rotation);
                processingMenuItem.GetComponentInChildren<Text>().text = theItemsList[c];
                processingMenuItem.gameObject.name = theItemsList[c];
                scrollListItemsObjects[c] = processingMenuItem.GetComponent<RectTransform>();
                scrollListItemsObjects[c].SetParent(theItemSample.transform.parent);
            }
            theItemSample.SetActive(false);
            
			theList.GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(theList.GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().sizeDelta.x,
                                                                                                                    /* scrollListItemsObjects[scrollListItemsObjects.Length-1].position.y+scrollListItemsObjects[scrollListItemsObjects.Length-1].sizeDelta.y*/
                                                                                                                     scrollListItemsObjects[scrollListItemsObjects.Length-1].sizeDelta.y*scrollListItemsObjects.Length);
			Vector3 tempPos =  theList.GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().position;
			tempPos.y = theList.GetComponent<ScrollRect>().content.rect.position.y*2;
			theList.GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().position = tempPos;
        }
		SetSelection(theItemsList[0]);
    }

	
	public void ShowOrHideList(){
		if (isAppear){
			//theList.SetActive (false);
            theList.GetComponent<Animator>().SetBool("isSongList", false);
			isAppear = false;
		}else{
			//theList.SetActive (true);
            theList.GetComponent<Animator>().SetBool("isSongList", true);
			isAppear = true;
		}
	}
	

	public void SetSelection(string itemName){
        if (canChangeTitle){
            selectedName.text = /*"<b><size=25><color=black>Song Name - </color></size> <size=19> <color=grey>"+*/itemName/*+"</color></size></b>"*/;
        }
		selection = itemName;
		if (isAppear){
			//theList.SetActive (false);
            theList.GetComponent<Animator>().SetBool("isSongList", false);
			isAppear = false;
		}
	}
	
	public void SetListOfItems(List<string> theNewList){
		theItemsList = theNewList;
		OnUpdateList();
		
	}
}
