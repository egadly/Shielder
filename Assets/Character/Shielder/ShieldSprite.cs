using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSprite : Spriter {

  protected Shield _shield;
  public Vector3 direction = Vector3.zero;

  protected override void SpriteStart() {
    _shield = transform.parent.GetComponentInChildren<Shield>();
  }
	protected override void SpriteUpdate() {
    _renderer.sortingOrder = (int)(transform.position.y * -16f);
    if ( _shield.transform.localPosition.y <= 0 ) _renderer.sortingOrder += 100;
    else _renderer.sortingOrder -= 100;
    _renderer.flipX = _shield.faceDirection == Direction.Left;
  }
}
