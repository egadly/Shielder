using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : FrameLimiter
{
  protected Vector3 _localPosition = Vector3.zero;
  protected Vector3 _deflectBoxPosition = Vector3.zero;
  protected Vector3 _deflectBoxScale = new Vector3(0.75f, 0.6f, 0);
  protected float _offset = 0.3f;
  protected Vector3 _direction = new Vector3();
  public Vector3 direction
  {
    get
    {
      return _direction;
    }
  }
  public Direction faceDirection
  {
    get
    {
      if (_direction.x >= 0) return Direction.Right;
      else return Direction.Left;
    }
  }
  protected Deflectbox _deflectbox;
  protected Reflectbox _reflectbox;
  protected Timer _reflectTimer;

  void Start()
  {
    _deflectbox = GetComponentInChildren<Deflectbox>();
    _reflectbox = GetComponentInChildren<Reflectbox>();
  }

  // Update is called once per frame
  protected override void UpdatePlus()
  {

    _localPosition = direction;
    _localPosition.x *= 1 / 4f;
    _localPosition.y *= 1 / 8f;
    transform.localPosition = _localPosition;

    _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    _direction.z = transform.position.z;
    _direction -= transform.position;
    _direction.Normalize();

    _deflectbox.direction = _direction;


    if (Mathf.Abs(_direction.x) > 0.8f)
    {
      _offset = 0;
      _deflectBoxScale.y = 1f;
    }
    else
    {
      if (_direction.y >= 0) _offset = 0.3f;
      else _offset = -0.3f;
      _deflectBoxScale.y = 0.6f;
    }
    _deflectBoxPosition.y = _offset;
    _deflectbox.transform.localPosition = _deflectBoxPosition;
    _deflectbox.transform.localScale = _deflectBoxScale;
    _reflectbox.transform.localPosition = _deflectBoxPosition;
    _reflectbox.transform.localScale = _deflectBoxScale;

    if (!_reflectTimer)
    {
      _reflectbox.gameObject.SetActive(true);
      _deflectbox.gameObject.SetActive(false);
      _reflectTimer.Increment();
    }
    else {
      _reflectbox.gameObject.SetActive(false);
      _deflectbox.gameObject.SetActive(true);
    }

  }

  void OnEnable()
  {
    _reflectTimer.Set(2);
    SoundEffects.PlayShield();
  }

  /*void OnDisable() {
    SoundEffects.PlayUnshield();
  }*/
}
