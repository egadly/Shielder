using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FrameLimiter, Reflectable
{

  protected Timer _lifeTimer;
  public Vector3 _velocity = Vector3.zero;
  protected Hitbox[] _hitboxes;
  public Color color = Color.red;
  protected BulletSprite _sprite;
  protected Light _light;

  void Start()
  {
    _lifeTimer.Set(60 * 5);
    _sprite = GetComponentInChildren<BulletSprite>();
    _light = GetComponentInChildren<Light>();
    _hitboxes = GetComponentsInChildren<Hitbox>();
  }

  protected override void UpdatePlus()
  {

    if (_lifeTimer)
    {
      Destroy(gameObject);
      for (int i = 0; i < 5; i++)
      {
        float variability = 1.0f;
        Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), color);
      }
      SoundEffects.PlayShield(0.5f);
    }
    else _lifeTimer.Increment();

    foreach (Hitbox hitbox in _hitboxes)
    {
      if (hitbox.didHit)
      {
        Destroy(gameObject);
        for (int i = 0; i < 5; i++)
        {
          float variability = 1.0f;
          Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), color);
        }
        SoundEffects.PlayShield(0.5f);
      }
      hitbox.direction = _velocity.normalized;
    }
    ColorLight();
    transform.position += _velocity * frameTime;

  }

  public void Reflect()
  {
    _velocity = _velocity * -1f;
    if (color == Color.red) color = Color.blue;
    else if (color == Color.blue) color = Color.red;
    Particle.SpawnFlash(transform.position, color);
  }
  public void Deflect(Vector3 direction)
  {
    _velocity = direction * _velocity.magnitude;
    if (color == Color.red) color = Color.blue;
    else if (color == Color.blue) color = Color.red;
    Particle.SpawnFlash(transform.position, color);
  }

  public virtual void ColorLight() {
    _light.color = color;
  }

  public static GameObject SpawnBullet(MonoBehaviour parent, Vector3 position, Vector3 direction, float speed, Color color)
  {
    if (Resources.main != null)
    {
      GameObject temp = Instantiate(Resources.bullet, position, Quaternion.identity);
      temp.transform.Find("hitbox").tag = parent.tag;
      Bullet bullet = temp.GetComponent<Bullet>();
      bullet._velocity = direction.normalized * speed;
      bullet.color = color;
      return temp;
    }
    else return null;
  }

  public static GameObject SpawnSmallBullet(MonoBehaviour parent, Vector3 position, Vector3 direction, float speed, Color color)
  {
    if (Resources.main != null)
    {
      GameObject temp = Instantiate(Resources.smallBullet, position, Quaternion.identity);
      temp.transform.Find("hitbox").tag = parent.tag;
      Bullet bullet = temp.GetComponent<Bullet>();
      bullet._velocity = direction.normalized * speed;
      bullet.color = color;
      return temp;
    }
    else return null;
  }

}
