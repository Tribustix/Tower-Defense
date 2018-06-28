using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

	public Enemy enemy;
	public float lifePercent;

	private GameObject fill;

	void Start () {
		GetComponent<Slider>().maxValue = enemy.maxHealth;
		fill = GetFillGameObject();
	}
	
	void Update () {
		
		if(enemy){
			GetComponent<Slider>().value = enemy.health;
			
			UtilityMethods.MoveUiElementToWorldPosition(GetComponent<RectTransform>(), enemy.transform.position + new Vector3 (0, 0, 1));

			lifePercent = (enemy.health / enemy.maxHealth) * 100;
			ChangeBar();
			
		}else{
			Destroy(gameObject);
		}

	}

	void ChangeBar () {

		if(lifePercent >= 50){
			fill.GetComponent<Image>().color = new Color32(32, 231, 42, 255);
		}else if(lifePercent > 25 && lifePercent < 50){
			fill.GetComponent<Image>().color = new Color32(251, 255, 0, 255);
		}else if(lifePercent <= 25){
			fill.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
		}


	}

	GameObject GetFillGameObject  () {

		GameObject fillArea = this.transform.Find("Fill Area").gameObject;

		if(fillArea){

			GameObject fill = fillArea.transform.Find("Fill").gameObject;
			return fill; 

		}else{

			return null;

		}

	}
}
