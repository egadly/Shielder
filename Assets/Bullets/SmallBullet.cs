using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBullet : Bullet {

  public override void ColorLight() {
    Color temp = color;
    temp.g = 1.0f;
    _light.color = temp;
  }

}
