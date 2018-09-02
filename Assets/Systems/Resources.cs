using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{

  public static Resources main = null;
  public GameObject _bullet;
  public static GameObject bullet
  {
    get
    {
      if (main != null) return main._bullet;
      else return null;
    }
  }
    public GameObject _smallBullet;
  public static GameObject smallBullet
  {
    get
    {
      if (main != null) return main._smallBullet;
      else return null;
    }
  }
  public GameObject _flash;
  public static GameObject flash
  {
    get
    {
      if (main != null) return main._flash;
      else return null;
    }
  }
  public GameObject _bubble;
  public static GameObject bubble
  {
    get
    {
      if (main != null) return main._bubble;
      else return null;
    }
  }
  public GameObject _cross;
  public static GameObject cross
  {
    get
    {
      if (main != null) return main._cross;
      else return null;
    }
  }

  void Start()
  {
    if (main == null) main = this;
    else Destroy(this);
  }

}
