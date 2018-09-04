using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : Character
{

  public static Shielder main = null;
  protected Timer _invincibleTimer = new Timer(0);
  protected Timer _flashTimer = new Timer(10);
  protected Color _flashMultiplyColor = Color.black;
  protected Color _flashAddColor = Color.black;
  protected GameObject _shield;
  protected bool _useShield;


  public Sprite[] idle;
  public Sprite[] run;

  protected override void CharacterStart()
  {
    if (main == null) main = this;
    _shield = transform.Find("shield").gameObject;
  }


  // Update is called once per frame
  protected override void CharacterUpdate()
  {

    _isInvincible = false;
    _flashMultiplyColor = Color.white;
    _flashAddColor = Color.black;
    float currentMoveSpeed = moveSpeed;

    _moveDirection = Vector3.zero;
    if (Input.GetKey(KeyCode.W)) _moveDirection.y += 1f;
    if (Input.GetKey(KeyCode.A))
    {
      _moveDirection.x -= 1f;
      _faceDirection = Direction.Left;
    }
    if (Input.GetKey(KeyCode.S)) _moveDirection.y -= 1f;
    if (Input.GetKey(KeyCode.D))
    {
      _moveDirection.x += 1f;
      _faceDirection = Direction.Right;
    }
    _moveDirection.Normalize();

    if (!_hitstunTimer)
    {
      _hitstunTimer.Increment();
      _moveDirection = Vector3.zero;
      _useShield = false;
    }
    if (!_invincibleTimer)
    {
      _invincibleTimer.Increment();
      stamina = 100f * ( (float)_invincibleTimer.time/_invincibleTimer.goal );
      _flashMultiplyColor = new Color(0f, 1f, 1f, 0);
      _isInvincible = true;
      _useShield = false;
    }
    else
    {
      if ( Input.GetKey(KeyCode.Mouse0)|| Input.GetKey(KeyCode.Space) )
      {
        if ( !_useShield ) {
          if ( stamina >= 100 ) _useShield = true;
        }
        else { 
          if ( stamina <= 0 ) _useShield = false;
          stamina -= 2f;
        }
      }
      else
      {
        _useShield = false;
      }
    }

    if ( _useShield ) _isInvincible = true;

    if ( _useShield ) {
      currentMoveSpeed = 0.75f;
      _faceDirection = _shield.GetComponent<Shield>().faceDirection;
    } else {
      if ( stamina < 100 ) stamina = stamina + 2;
      

    }
    _shield.SetActive( _useShield );

    _isMoving = _moveDirection != Vector3.zero;
    if (isMoving)
    {
      _velocity = _moveDirection * currentMoveSpeed;
      _sprite.ChangeAnimation(run);
    }
    else
    {
      _sprite.ChangeAnimation(idle);
    }
    if (_flashTimer)
    {
      _sprite.FlashMultiply(_flashMultiplyColor);
      _sprite.FlashAdd(_flashAddColor);
      _flashTimer.Set(30);
    }
    else _flashTimer.Increment();

  }

  protected override void DamagePlus()
  {
    _hitstunTimer.Set(10);
    _flashTimer.Set(30);
    _invincibleTimer.Set(30);
  }
}
