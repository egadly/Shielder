using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : Collision
{

  public float damage = 0;
  public float force = 0;
  public Vector3 direction = Vector3.zero;
  public Reflectable _body = null;
  protected bool _didHit = false;
  public bool didHit
  {
    get { return _didHit; }
  }
  private Timer _reflectTimer = new Timer(0);


  protected override void CollisionStart()
  {
    _color = Color.red;
    _didHit = false;
    _body = GetComponentInParent<Reflectable>();
  }

  protected override void CollisionUpdate()
  {
    CheckCollisions("Physical");
    if (_results[0] != null) _didHit = true;
    if (!_reflectTimer) _reflectTimer.Increment();
  }

  public bool OnHit()
  {
    _didHit = true;
    return true;
  }

  public void Reflect()
  {
    if (_body != null && _reflectTimer)
    {
      _body.Reflect();
      _reflectTimer.Set(10);
      SoundEffects.PlayReflect();

      for (int i = 0; i < 5; i++)
      {
        float variability = 1.0f;
        Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), Color.red);
        Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), Color.blue);
        Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), Color.green);
      }

      SwapSides();
    }
  }
  public void Deflect(Vector3 direction)
  {
    if (_body != null && _reflectTimer)
    {
      _body.Deflect(direction);
      _reflectTimer.Set(60);
      SoundEffects.PlayDeflect();
      SwapSides();
    }
  }

  public void SwapSides()
  {
    if (gameObject.tag == "Ally")
    {
      gameObject.tag = "Enemy";
    }
    else if (gameObject.tag == "Enemy")
    {
      gameObject.tag = "Ally";
    }
  }


}
