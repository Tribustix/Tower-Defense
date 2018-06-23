using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



[System.Serializable] public struct TowerCost {
	public TowerType TowerType;
	public int cost;
}


public class TowerManager : MonoBehaviour {

	public static TowerManager Instance;

	public GameObject stoneTowerPrefab;
	public GameObject fireTowerPrefab;
	public GameObject iceTowerPrefab;

	public List<TowerCost> TowerCosts = new List <TowerCost>();

	void Awake () {
		Instance = this;	
	}	

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void CreateNewTower (GameObject slotToFill, TowerType towerType) {
		
		switch(towerType){

			case TowerType.Stone:
			Instantiate(stoneTowerPrefab, slotToFill.transform.position, Quaternion.identity);
			slotToFill.gameObject.SetActive(true);
			break;

			case TowerType.Fire:
			Instantiate(fireTowerPrefab, slotToFill.transform.position, Quaternion.identity);
			slotToFill.gameObject.SetActive(true);
			break;

			case TowerType.Ice:
			Instantiate(iceTowerPrefab, slotToFill.transform.position, Quaternion.identity);
			slotToFill.gameObject.SetActive(true);
			break;
		}
	}


	//LINQ utility method to get the price of a tower.
	public int GetTowerPrice(TowerType towerType){
		return (from TowerCost in TowerCosts where TowerCost.TowerType == towerType select TowerCost.cost).FirstOrDefault();
	}
}
