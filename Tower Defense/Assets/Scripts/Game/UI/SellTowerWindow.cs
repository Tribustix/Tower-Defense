using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellTowerWindow : MonoBehaviour {

	public Tower tower;

	public Text txtInfo;

	public Text txtSellPrice;

	private int sellPrice;
	private GameObject btnSell;

	void Awake () {

		btnSell = txtSellPrice.transform.parent.gameObject;
		
	}

	void OnEnable () {

		RemoveInfo();

	}


	void RemoveInfo () {

		
		sellPrice = GetSellPrice();

		txtInfo.text = tower.type + " Tower Lv " + tower.towerLevel;

		txtSellPrice.text = "Remove\n" + sellPrice + " Gold";

		
		btnSell.SetActive(true);
	}

	public void RemoveTower () {

			GameManager.Instance.gold += GetSellPrice();
			tower.DestroyTower();
			gameObject.SetActive(false);

	}

	public int GetSellPrice () {

		int basePrice = TowerManager.Instance.GetTowerPrice(tower.type);
		sellPrice = 0;

		for(int i = 1; i <= tower.towerLevel; i++){

			if(i == 1){
				sellPrice = sellPrice + (basePrice / 2);
			}else{
				int levelPrice = Mathf.CeilToInt(basePrice * 1.5f * (i - 1));
				sellPrice = sellPrice + (levelPrice / 2);
			}
			
			
		}

		return sellPrice;

	}
}
