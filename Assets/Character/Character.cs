using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : FrameLimiter
{

  public float health = 100;
  public float maxHealth = 100;
  public float stamina = 100;

  protected Timer _hitstunTimer = new Timer(0);
  protected bool _isInvincible = false;

  public CharacterSprite _sprite;

  public Vector3 _velocity = Vector3.zero;

  public float moveSpeed = 1f;
  protected Vector3 _moveDirection = new Vector3();

  protected Direction _faceDirection = Direction.Right;
  public Direction faceDirection
  {
    get
    {
      return _faceDirection;
    }
  }

  protected bool _isMoving = false;
  public bool isMoving
  {
    get
    {
      return _isMoving;
    }
  }

  void Start()
  {
    _sprite = transform.Find("sprite").GetComponent<CharacterSprite>();
    health = maxHealth;
    CharacterStart();
  }

  protected override void UpdatePlus()
  {
    CharacterUpdate();

    GetComponent<Rigidbody2D>().MovePosition(transform.position + _velocity * frameTime);
    _velocity = _velocity * 0.9f;

    if (health <= 0) Die();
  }

  public void Force(Vector3 vector)
  {
    _velocity = vector;
  }
  public void Force(Vector3 direction, float magnitude)
  {
    _velocity = direction.normalized * magnitude;
  }
  public virtual void Damage(Hurtbox hurt, Hitbox hit)
  {

    if (hit.gameObject.tag != gameObject.tag && !_isInvincible)
    {
      health -= hit.damage;
      if (hit.direction == Vector3.zero)
      {
        Force(hurt.transform.position - hit.transform.position, hit.force);
      }
      else Force(hit.direction, hit.force);

      _sprite.FlashAdd(Color.white);
      _sprite.Shake(10, 0.125f);
      Particle.SpawnCross(transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0), Color.red);
      Particle.SpawnCross(transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0), Color.blue);
      Particle.SpawnCross(transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0), Color.green);
      SoundEffects.PlayHit();
      DamagePlus();

      hit.OnHit();
    }

  }

  public virtual void Die()
  {
    for (int i = 0; i < 10; i++)
    {
      float variability = 2.0f;
      Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), Color.red);
      Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), Color.blue);
      Particle.SpawnBubble(transform.position + new Vector3(Random.Range(-1 * variability, variability), Random.Range(-1 * variability, variability), 0), Color.green);
    }
    SoundEffects.PlayDeath();
    World.Message( gameObject.name );
    Destroy(gameObject);
  }

  protected virtual void CharacterStart() { }
  protected virtual void CharacterUpdate() { }
  protected virtual void DamagePlus() { }

}
