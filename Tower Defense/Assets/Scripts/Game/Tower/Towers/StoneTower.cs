using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTower : Tower {

	public GameObject stonePrefab;

	protected override void AttackEnemy (TowerType type) {
		
		base.AttackEnemy(type);

		GameObject stone = (GameObject) Instantiate(stonePrefab, towerPieceToAim.position, Quaternion.identity);
		stone.GetComponent<Stone>().enemyToFollow = targetEnemy;
		stone.GetComponent<Stone>().damage = attackPower;
	}


}
