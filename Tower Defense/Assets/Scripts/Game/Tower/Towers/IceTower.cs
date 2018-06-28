using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower {

	public GameObject icePrefab;

	public override void Update () {
		base.Update();
		GetNonFrozenTarget();
	}

	void GetNonFrozenTarget () {
		foreach(Enemy enemy in GetEnemiesInAggroRange()){
			if(!enemy.frozen){
				targetEnemy = enemy;
				break;
			}
		}
	}

	protected override void AttackEnemy () {
		base.AttackEnemy();
		
		GameObject ice = (GameObject)Instantiate(icePrefab, towerPieceToAim.position, Quaternion.identity);

		ice.GetComponent<FollowingProjectile>().enemyToFollow = targetEnemy;
	} 

}
