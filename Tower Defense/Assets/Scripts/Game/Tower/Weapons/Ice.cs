using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : FollowingProjectile {

	protected override void OnHitEnemy () {
		enemyToFollow.Freeze();
		Destroy(gameObject);
	}
}
