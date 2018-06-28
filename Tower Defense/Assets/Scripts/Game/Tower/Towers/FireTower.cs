using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower {

	public GameObject fireParticlesPrefab;

	protected override void AttackEnemy () {
		base.AttackEnemy();

		GameObject particles = (GameObject) Instantiate(fireParticlesPrefab, transform.position + new Vector3(0, .5f), fireParticlesPrefab.transform.rotation);

		particles.transform.localScale *= aggroRadius / 10f;
		
		foreach(Enemy enemy in EnemyManager.Instance.GetEnemiesInRange(transform.position, aggroRadius)){
			enemy.TakeDamage(attackPower);
		}
	}
	
}
