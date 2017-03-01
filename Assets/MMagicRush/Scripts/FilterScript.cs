using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FilterScript : MonoBehaviour {

	public List<Transform> childrens;

	public GameObject[] CardPrefabs;

	void OnEnable() {
		//UpdateChildrens ();
		UpdatePlayerCardList();
	}

	// Use this for initialization
	void Start () {
	
	}
		
	// Update is called once per frame
	void Update () {

	}

	public void FilterEfect(string t){
		foreach (Transform c in childrens)
		{
			if(c.GetComponent<CardScript>().efeito != t){
				c.gameObject.SetActive(false);
			} 
			if(c.GetComponent<CardScript>().efeito == t){
				c.gameObject.SetActive(true);
			}
			if(c.GetComponent<CardScript>().efeito == "Todos"){
				c.gameObject.SetActive(true);
			}
		}
	}

	public void FilterCharacter(string t){
		foreach (Transform c in childrens)
		{
			if(c.GetComponent<CardScript>().personagem != t){
				c.gameObject.SetActive(false);
			} 
			if(c.GetComponent<CardScript>().personagem == t){
				c.gameObject.SetActive(true);
			}
			if(c.GetComponent<CardScript>().personagem == "Todos"){
				c.gameObject.SetActive(true);
			}
		}
	}

	public void UpdatePlayerCardList(){
		int[] x = PlayerPrefsX.GetIntArray ("PlayerCardsIDs");
		Debug.Log (x);
		foreach (GameObject card in CardPrefabs) {
			if( ArrayUtility.Contains(x,card.GetComponent<CardScript>().CardID) ){
			GameObject g = Instantiate (card, this.transform.position, Quaternion.identity);
			g.transform.localScale = new Vector3 (1, 1, 1);
			g.transform.SetParent (this.transform,false);
				childrens.Add (g.transform);
			}
		}
	}

//	public void UpdateChildrens(){
//		Transform[] filter = this.gameObject.GetComponentsInChildren<Transform> ();
//		foreach (Transform f in filter)
//		{
//			if(f.name == "Card"){
//			f.transform.gameObject.SetActive(true);
//			childrens.Add(f);
//			}  
//		}
//	}

}
