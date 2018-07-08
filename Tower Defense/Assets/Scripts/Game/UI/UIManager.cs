using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	public GameObject addTowerWindow;
	public GameObject towerToInfoWindow;
	public GameObject winGameWindow;
	public GameObject loseGameWindow;
	public GameObject blackBackground;
	public GameObject enemyHealthBarPrefab;
	public GameObject centerWindow;
	public GameObject damageCanvas;
	public GameObject towerToRemoveInfoWindow;


	public Transform enemyHealthBars;

	public Text txtGold;
	public Text txtWave;
	public Text txtEscapedEnemies;
	public Text txtScore;
	public Text txtTimeNextWave;
	public Text txtDifficulty;

	public float timeNextWave;
	public int actualWave = -1;

	void Awake () {
		Instance = this;
	}

	void Start () {
		
		txtDifficulty.text = "Difficulty: " + GameManager.Instance.GetDifficulty();

		timeNextWave = (int)WaveManager.Instance.enemyWaves[GameManager.Instance.waveNumber].startSpawnTimeInSeconds;

	}


	void Update () {

		UpdateTopBar();

	}

	void UpdateTopBar () {
		
		txtGold.text = GameManager.Instance.gold.ToString();
		
		txtWave.text = "Wave " + GameManager.Instance.waveNumber + " / " + WaveManager.Instance.enemyWaves.Count;
		
		txtEscapedEnemies.text = "Escaped Enemies " + GameManager.Instance.escapedEnemies + " / " + GameManager.Instance.maxAllowedEscapedEnemies;

		if(actualWave != GameManager.Instance.waveNumber ){
			
			actualWave++;

		}else if(GameManager.Instance.waveNumber == WaveManager.Instance.enemyWaves.Count){
			
			txtTimeNextWave.text = " ";	
			
		}else{

			timeNextWave = WaveManager.Instance.elapsedTime;
			txtTimeNextWave.text = "Next Wave in : "+(int)timeNextWave+ " s " + " / "+ WaveManager.Instance.enemyWaves[GameManager.Instance.waveNumber].startSpawnTimeInSeconds + " s";

		}
		

	}

	void SetScore () {

		txtScore.text = "Score:"  + GameManager.Instance.score;
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

	public void ShowRemoveTowerInfoWindow (Tower tower) {
		towerToRemoveInfoWindow.GetComponent<SellTowerWindow>().tower = tower;
		towerToRemoveInfoWindow.SetActive(true);

		UtilityMethods.MoveUiElementToWorldPosition(towerToRemoveInfoWindow.GetComponent<RectTransform>(), tower.transform.position);
	}

	public void ShowWinScreen () {
		blackBackground.SetActive(true);
		winGameWindow.SetActive(true);
		SetScore();
	}

	public void ShowLoseScreen () {
		blackBackground.SetActive(true);
		loseGameWindow.SetActive(true);
	}

	public void CreateHealthBarForEnemy (Enemy enemy) {

	GameObject healthBar = Instantiate(enemyHealthBarPrefab);
	healthBar.transform.SetParent(enemyHealthBars, false);
	healthBar.GetComponent<EnemyHealthBar>().enemy = enemy;

	}

	public void ShowCenterWindow (string text) {

		centerWindow.transform.Find("Text Wave").GetComponent<Text>().text = text;
		StartCoroutine(EnableAndDisableCenterWindow());

	}
	
	IEnumerator EnableAndDisableCenterWindow () {

		for(int i = 0; i < 3; i++){
			yield return new WaitForSeconds(.5f);
			centerWindow.SetActive(true);

			yield return new WaitForSeconds(.5f);
			centerWindow.SetActive(false);

		}

	}

	public void ShowDamage () {
		StartCoroutine(DoDamageAnimation());
	}

	IEnumerator DoDamageAnimation () {

		for(int i = 0; i < 3; i++){
			yield return new WaitForSeconds(.1f);
			damageCanvas.SetActive(true);

			yield return new WaitForSeconds(.1f);
			damageCanvas.SetActive(false);
		}

		
	}




}
