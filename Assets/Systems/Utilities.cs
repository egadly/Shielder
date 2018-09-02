using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction: short { Up, Left, Down, Right };

public static class Utilities {
  public static float LimitPrecision( float input, float precision ) {
    return ((int)(input * precision))/precision;
  }

  public static Vector3 AngleToVector( float radians ) {
    return new Vector3( Mathf.Cos( radians ), Mathf.Sin( radians ) );
  }

}