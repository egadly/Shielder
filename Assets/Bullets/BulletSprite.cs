using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSprite : Spriter {

  protected Bullet _bullet;

  protected Color _color = Color.black;

  protected override void SpriteStart() {
    _bullet = GetComponentInParent<Bullet>();
  }

  protected override void SpriteUpdate() {
    _color = _bullet.color;
    _renderer.sortingOrder = (int)(transform.position.y * -16f);
    _renderer.material.SetColor( "_Add", _color );
  }

}
