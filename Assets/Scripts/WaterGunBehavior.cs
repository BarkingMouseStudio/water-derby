using UnityEngine;
using System.Collections;

public class WaterGunBehavior : MonoBehaviour {


  private const float touchScale = 200;
  private const float angularDrag = 0.9f;

  private Vector3 lookAt;
  private Vector3 prevLookAt = Vector3.zero;
  private Vector3 deltaLookAt = Vector3.zero;
  private Vector3 worldPosition;
  private Vector3 torque = Vector3.zero;
  
  private new Camera camera;
    
  public void Awake () {
    camera = Camera.main;
    worldPosition = transform.position;
  }

  public void Update () {
    
    lookAt = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
    deltaLookAt = lookAt - prevLookAt;
    prevLookAt = lookAt;
    torque += new Vector3(deltaLookAt.y / Screen.height,
      deltaLookAt.x / Screen.width, 0) * touchScale;
  	
    if (torque.magnitude > Mathf.Epsilon) {
      transform.RotateAround(worldPosition, Camera.main.transform.up, torque.y);
      transform.RotateAround(worldPosition, Camera.main.transform.right, torque.x);
      torque *= angularDrag;
    }
  		
    transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 2f);
      
  }
}
