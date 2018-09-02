using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizzy : Mage
{
  protected override void Fire()
  {
    if (Shielder.main != null)
    {
      SoundEffects.PlayFire();
      Particle.SpawnFlash(transform.position, Color.red);
      Vector3 direction = (Shielder.main.transform.position - transform.position).normalized;
      float angle = Mathf.Atan2(direction.y, direction.x);
      Bullet.SpawnSmallBullet(this, transform.position + Utilities.AngleToVector(angle), Utilities.AngleToVector(angle), 5, Color.red);
    }
    if (_isAggressive) _fireTimer.Set(30);
    else _fireTimer.Set( 9000 );
  }

}