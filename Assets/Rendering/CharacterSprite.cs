using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : Spriter {

  protected Character _char = null;
  
  public int animFrameRate = 6;
  public int animFrameTime;
  protected Timer _frameTimer = new Timer( 0 );


  protected Timer _shakeTimer = new Timer( 0 );
  protected float _shakeStrength = 0.5f;


  protected Color _originAddColor = Color.black;
  protected Color _currentAddColor = Color.black;
  protected Color _originMultiplyColor = Color.white;
  protected Color _currentMultiplyColor = Color.white;

  public Sprite[] _currentAnimation;
  protected int _currentIndex = 0;



  protected override void SpriteStart() {
    animFrameTime = (int)frameRate/animFrameRate;
    _frameTimer.Set( animFrameTime );
    _char = transform.parent.gameObject.GetComponent<Character>();
  }

  protected override void SpriteUpdate() {

    if ( _char != null ) {
      if ( _char.faceDirection == Direction.Right ) _renderer.flipX = false;
      else if ( _char.faceDirection == Direction.Left ) _renderer.flipX = true;
    }

    if ( !_shakeTimer ) {
      transform.localPosition = localPosition + new Vector3( Random.Range( _shakeStrength, -1* _shakeStrength ), Random.Range( _shakeStrength, -1* _shakeStrength ), 0 );
      _shakeTimer.Increment();
    }

    //Animation 
    if ( _frameTimer ) {
      if ( _currentAnimation != null ) {
        _currentIndex = ( _currentIndex + 1 ) % _currentAnimation.Length;
        _renderer.sprite = _currentAnimation[_currentIndex];
         _frameTimer.Set( animFrameTime );
      }
    } else _frameTimer.Increment();

    _renderer.material.SetColor( "_Add", _currentAddColor );
    _currentAddColor = _currentAddColor * 0.85f + _originAddColor * 0.15f;

    _renderer.color = _currentMultiplyColor;
    _currentMultiplyColor = _currentMultiplyColor * 0.85f + _originMultiplyColor * 0.15f;

    _renderer.sortingOrder = (int)(transform.position.y * -16f);

  }

  public void ChangeAnimation( Sprite[] animation ) {
    if ( _currentAnimation != animation ) {
      _currentAnimation = animation;
      _currentIndex = 0;
      _frameTimer.Set( animFrameTime );
      _renderer.sprite = _currentAnimation[_currentIndex];
    }
  }

  public void FlashAdd( Color color ) {
    _currentAddColor = color;
  }
  public void FlashMultiply( Color color ) {
    _currentMultiplyColor = color;
  }
  public void Shake( int time, float strength ) {
    _shakeTimer.Set( time );
    _shakeStrength = strength;
  }


}
