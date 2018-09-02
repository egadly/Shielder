using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spriter : FrameLimiter {

  public Vector3 localPosition;

  protected SpriteRenderer _renderer;
  public const int pixelPerUnit = 16;

  void Start() {
    _renderer = GetComponent<SpriteRenderer>();
    SpriteStart();
  }

  protected override void UpdatePlus() {
    transform.localPosition = localPosition * 0.5f + transform.localPosition * 0.5f;
    SpriteUpdate();
  }

  protected virtual void SpriteStart() {}
  protected virtual void SpriteUpdate() {}


}
