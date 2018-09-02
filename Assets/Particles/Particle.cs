using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Spriter
{

  public int animFrameRate = 10;
  public int animFrameTime;
  protected Timer _frameTimer = new Timer(0);


  protected Timer _shakeTimer = new Timer(0);
  protected float _shakeStrength = 0.5f;

  public Color color;

  public Sprite[] _currentAnimation;
  protected int _currentIndex = 0;



  protected override void SpriteStart()
  {
    animFrameTime = (int)frameRate / animFrameRate;
    _frameTimer.Set(animFrameTime);
  }

  protected override void SpriteUpdate()
  {

    ParticleUpdate();

    //Animation 
    if (_frameTimer)
    {
      if (_currentAnimation != null)
      {
        _currentIndex = (_currentIndex + 1);
        if (_currentIndex >= _currentAnimation.Length) Destroy(gameObject);
        else _renderer.sprite = _currentAnimation[_currentIndex];
        _frameTimer.Set(animFrameTime);
      }
    }
    else _frameTimer.Increment();

    _renderer.color = color;

  }

  protected virtual void ParticleUpdate() { }

  public static GameObject SpawnFlash(Vector3 position, Color color)
  {
    if (Resources.main != null)
    {
      GameObject temp = Instantiate(Resources.flash, position, Quaternion.identity);
      temp.GetComponent<Particle>().color = color;
      temp.GetComponent<Particle>().localPosition = position;
      return temp;
    }
    else return null;
  }

  public static GameObject SpawnBubble(Vector3 position, Color color)
  {
    if (Resources.main != null)
    {
      GameObject temp = Instantiate(Resources.bubble, position, Quaternion.identity);
      temp.GetComponent<Particle>().color = color;
      temp.GetComponent<Particle>().localPosition = position;
      return temp;
    }
    else return null;
  }
  public static GameObject SpawnCross(Vector3 position, Color color)
  {
    if (Resources.main != null)
    {
      GameObject temp = Instantiate(Resources.cross, position, Quaternion.identity);
      temp.GetComponent<Particle>().color = color;
      temp.GetComponent<Particle>().localPosition = position;
      return temp;
    }
    else return null;
  }

}
