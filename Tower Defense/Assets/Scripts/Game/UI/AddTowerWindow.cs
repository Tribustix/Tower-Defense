using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddTowerWindow : MonoBehaviour {

	public GameObject towerSlotToAddTowerTo;

	public void AddTower(string towerTypeAsString){
		TowerType type = (TowerType)Enum.Parse(typeof(TowerType), towerTypeAsString, true);

		if(TowerManager.Instance.GetTowerPrice(type) <= GameManager.Instance.gold){
			GameManager.Instance.gold -= TowerManager.Instance.GetTowerPrice(type);
			TowerManager.Instance.CreateNewTower(towerSlotToAddTowerTo, type);
			gameObject.SetActive(false);
		}
	}



	
}
