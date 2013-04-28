using UnityEngine;
using System.Collections;

public class WaterGunBehavior : MonoBehaviour {

  private Vector3 prevMouse = Vector3.zero;
  private Vector3 center = Vector3.zero;
  private float centerSpeed = 1;
  private float gravity = 0.9f;
  
  private Camera camera;
  private Vector3 lookAt;
  
  SpringJoint springJoint;
  
  public void Awake () {
    springJoint = GetComponent("Spring Joint") as SpringJoint;  
    camera = Camera.main;
  }

  public void Update () {
    //if (Input.GetMouseButtonDown(0)) {
    
      lookAt = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

      transform.LookAt(lookAt);

      
   // }
  }
}
