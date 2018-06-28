using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	public GameObject addTowerWindow;
	public GameObject towerToInfoWindow;

	public Text txtGold;
	public Text txtWave;
	public Text txtEscapedEnemies;

	void Awake () {
		Instance = this;
	}

	void Start () {
		
	}


	void Update () {

		UpdateTopBar();

	}

	void UpdateTopBar () {
		
		txtGold.text = GameManager.Instance.gold.ToString();
		
		txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
		
		txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " + GameManager.Instance.maxAllowedEscapedEnemies;

	}

	public void ShowAddTowerWindow (GameObject towerSlot) {

		addTowerWindow.SetActive(true);
		addTowerWindow.GetComponent<AddTowerWindow>().towerSlotToAddTowerTo = towerSlot;

		UtilityMethods.MoveUiElementToWorldPosition(addTowerWindow.GetComponent<RectTransform>(), towerSlot.transform.position);
	}

	public void ShowTowerInfoWindow (Tower tower) {
		towerToInfoWindow.GetComponent<TowerInfoWindow>().tower = tower;
		towerToInfoWindow.SetActive(true);

		UtilityMethods.MoveUiElementToWorldPosition(towerToInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
	}



}
