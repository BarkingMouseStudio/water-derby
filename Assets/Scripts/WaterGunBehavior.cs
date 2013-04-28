using UnityEngine;
using System.Collections;

public class WaterGunBehavior : MonoBehaviour {

  private Vector3 prevMouse = Vector3.zero;
  private Vector3 center = Vector3.zero;
  private float centerSpeed = 1;
  private float gravity = 0.9f;
  
  SpringJoint springJoint;
  
  public void Awake () {
    springJoint = GetComponent("Spring Joint") as SpringJoint;  
  
  }

  public void Update () {
    if (Input.GetMouseButtonDown(0)) {
    
    
      var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	  // springJoint.transform.position = ray.GetPoint(distance);
      
    }
  }
}
