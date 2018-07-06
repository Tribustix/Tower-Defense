using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : FollowingProjectile {

	public float damage;
	
	protected override void OnHitEnemy () {
		enemyToFollow.Freeze();
		enemyToFollow.TakeDamage(damage);
		Destroy(gameObject);
	}
}
