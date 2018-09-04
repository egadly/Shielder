using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : Character, Reflectable
{

  public bool _isAggressive = false;
  protected Timer _idleTimer = new Timer(0);
  protected Timer _chargeTimer = new Timer(0);
  protected Timer _rushTimer = new Timer(0);
  protected Timer _flashTimer = new Timer(0);
  protected Timer _soundTimer = new Timer(0);
  protected int _aggressionLength = 60;
  protected Color _flashAddColor;
  protected Color _flashMultiplyColor;
  protected bool _wasDeflected = false;

  protected Hitbox[] _hitboxes;

  protected Timer _particleTimer = new Timer(0);


  public Sprite[] idle;
  public Sprite[] charge;
  public Sprite[] rush;

  protected override void CharacterStart()
  {
    _hitboxes = GetComponentsInChildren<Hitbox>();
  }

  protected override void CharacterUpdate()
  {


    if (!_idleTimer)
    {
      _sprite.ChangeAnimation(idle);

      if (_idleTimer.time % 120 == 0)
      {
        _moveDirection.x = Random.Range(-1f, 1f);
        _moveDirection.y = Random.Range(-1f, 1f);
      }
      moveSpeed = 2f;

      stamina = 100 * (((float)_idleTimer.time / (float)_idleTimer.goal));

      _idleTimer.Increment();
    }
    else
    {
      if (!_chargeTimer)
      {
        if (_chargeTimer.time == 0)
        {
          if (_isAggressive) SoundEffects.PlaySkull();
          _sprite.FlashMultiply(Color.black);
        }
        _sprite.ChangeAnimation(charge);

        _chargeTimer.Increment();
      }
      else
      {
        if (!_rushTimer)
        {
          _sprite.ChangeAnimation(rush);
          _sprite.Shake(1, 0.125f);

          if (_rushTimer.time == 0)
          {
            if (Shielder.main != null) _moveDirection = Shielder.main.transform.position - transform.position;
            SoundEffects.PlaySkull();
            SoundEffects.PlayRush();
            _soundTimer.Set(30);
          }
          if (!_wasDeflected && (_rushTimer.time == 45 || _rushTimer.time == 90)) { if (Shielder.main != null) _moveDirection = Vector3.RotateTowards(_moveDirection, Shielder.main.transform.position - transform.position, Mathf.PI / 8f, 1f); }
          moveSpeed = 15f;

          foreach (Hitbox hitbox in _hitboxes)
          {
            hitbox.force = 30f;
          }

          if ( _soundTimer ) {
            _soundTimer.Set(30);
            SoundEffects.PlayRush();
          } else _soundTimer.Increment();

          if (_flashTimer)
          {
            _flashTimer.Set(10);
            _sprite.FlashAdd(_flashAddColor);
            _sprite.FlashMultiply(_flashMultiplyColor);
          }
          else _flashTimer.Increment();

          stamina = 100 * (1f - ((float)_rushTimer.time / _rushTimer.goal));
          _rushTimer.Increment();
        }
        else
        {

          _idleTimer.Set(_aggressionLength);
          _chargeTimer.Set(25);
          if (_isAggressive) _rushTimer.Set(_aggressionLength * 2);
          else _rushTimer.Set(0);
          _flashTimer.Set(10);
          foreach (Hitbox hitbox in _hitboxes)
          {
            hitbox.gameObject.tag = "Enemy";
            hitbox.force = 10f;
            hitbox.direction = Vector3.zero;
          }
          _flashAddColor = Color.red;
          _flashMultiplyColor = new Color(1f, 0.5f, 0.5f, 1f);

        }
      }
    }

    if (Shielder.main != null)
    {
      _isAggressive = Vector3.Distance(Shielder.main.transform.position, transform.position) < 20f;
    }
    else _isAggressive = false;

    if (_particleTimer)
    {
      for (int i = 0; i < 3; i++) Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Color.green);
      _particleTimer.Set(10);
    }
    else _particleTimer.Increment();

    _moveDirection.Normalize();
    if (_moveDirection.x >= 0) _faceDirection = Direction.Right;
    else _faceDirection = Direction.Left;
    _velocity = _moveDirection * moveSpeed;

  }

  public void Reflect()
  {
    health -= 34;
    _moveDirection = -1f * _moveDirection;
    if (_flashAddColor == Color.red)
    {
      _flashAddColor = Color.blue;
      _flashMultiplyColor = new Color(0.5f, 0.5f, 1.0f, 1f);
    }
    else
    {
      _flashAddColor = Color.red;
      _flashMultiplyColor = new Color(1f, 0.5f, 0.5f, 1f);
    }
    Particle.SpawnFlash(transform.position, _flashAddColor);
    _wasDeflected = true;
  }
  public void Deflect(Vector3 direction)
  {
    health -= 10;
    _moveDirection = direction.normalized;
    if (_flashAddColor == Color.red)
    {
      _flashAddColor = Color.blue;
      _flashMultiplyColor = new Color(0.5f, 0.5f, 1.0f, 1f);
    }
    else
    {
      _flashAddColor = Color.red;
      _flashMultiplyColor = new Color(1f, 0.5f, 0.5f, 1f);
    }
    Particle.SpawnFlash(transform.position, _flashAddColor);
    _wasDeflected = true;
  }

}
