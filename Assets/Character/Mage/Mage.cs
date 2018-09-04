using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
  public Sprite[] idle;
  public Sprite[] hide;
  public bool _isAggressive = false;
  protected Vector3 _desiredPosition;
  protected Timer _targetTimer = new Timer();
  protected int _aggressionLength = 60;

  protected Timer _fireTimer = new Timer(60);

  protected override void CharacterStart()
  {
    _targetTimer = new Timer(Random.Range(0, 240));
  }

  // Update is called once per frame
  protected override void CharacterUpdate()
  {

    _isInvincible = false;

    if (_targetTimer)
    {
      _desiredPosition.x = Random.Range(-2f, 2f);
      _desiredPosition.y = Random.Range(-2f, 2f);
      if (_isAggressive && Shielder.main != null)
      {
        _desiredPosition = Shielder.main.transform.position + _desiredPosition.normalized * Random.Range(2f, 5f);
        Fire();
      }
      else
      {
        _desiredPosition = transform.position + _desiredPosition;
      }
      _targetTimer.Set( Random.Range(_aggressionLength, (int) (_aggressionLength * 1.5f ) ) );
    }
    else
    {
      stamina = 100f * ( (float)_targetTimer.time/_targetTimer.goal );
      if ( _isAggressive && _targetTimer.time == (_targetTimer.goal - 60) ) SoundEffects.PlayMage();
      _targetTimer.Increment();

      if (_targetTimer.time < 120 && _isAggressive )
      {
        if (_fireTimer)
        {
          Fire();
        }
        else _fireTimer.Increment();
      }
    }


    _moveDirection = _desiredPosition - transform.position;
    if (_moveDirection.magnitude < 0.25f) _moveDirection = Vector3.zero;
    _moveDirection.Normalize();

    if (!_hitstunTimer)
    {
      _sprite.ChangeAnimation(hide);
      _moveDirection = Vector3.zero;
      _hitstunTimer.Increment();
    }

    _isMoving = _moveDirection != Vector3.zero;
    if (_isMoving)
    {
      _sprite.ChangeAnimation(idle);
      _velocity = moveSpeed * _moveDirection;
      if (_moveDirection.x > 0) _faceDirection = Direction.Right;
      if (_moveDirection.x < 0) _faceDirection = Direction.Left;
    }

    if (Shielder.main != null)
    {
      _isAggressive = Vector3.Distance(Shielder.main.transform.position, transform.position) < 15f;
    }
    else _isAggressive = false;

  }

  protected virtual void Fire()
  {
    if (Shielder.main != null)
    {
      SoundEffects.PlayFire();
      Particle.SpawnFlash(transform.position, Color.red);
      Vector3 direction = (Shielder.main.transform.position - transform.position).normalized;
      float angle = Mathf.Atan2(direction.y, direction.x);
      Bullet.SpawnBullet(this, transform.position + Utilities.AngleToVector(angle), Utilities.AngleToVector(angle), 3, Color.red);
      Bullet.SpawnBullet(this, transform.position + Utilities.AngleToVector(angle + Mathf.PI / 4.0f), Utilities.AngleToVector(angle + Mathf.PI / 4.0f), 3, Color.red);
      Bullet.SpawnBullet(this, transform.position + Utilities.AngleToVector(angle - Mathf.PI / 4.0f), Utilities.AngleToVector(angle - Mathf.PI / 4.0f), 3, Color.red);
    }
    _fireTimer.Set( 9000 );
  }

  protected override void DamagePlus()
  {
    _hitstunTimer.Set(60);
  }
}
