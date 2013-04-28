using UnityEngine;
using System.Collections;

public class WaterGunBehavior : MonoBehaviour {


  private const float touchScale = 2000;
  private const float angularDrag = 0.01f;

  private Vector3 lookAt;
  private Vector3 prevLookAt = Vector3.zero;
  private Vector3 deltaLookAt = Vector3.zero;
  private Vector3 worldPosition;
  private Vector3 torque = Vector3.zero;
  
  private Quaternion startingRotation;
  
  private new Camera camera;
    
  public void Awake () {
    camera = Camera.main;
    worldPosition = transform.position;
    startingRotation = transform.rotation; 
  }

  public void Update () {
    
    lookAt = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
    deltaLookAt = lookAt - prevLookAt;
    prevLookAt = lookAt;
    torque += new Vector3(deltaLookAt.y / Screen.height,
      deltaLookAt.x / Screen.width, 0) * touchScale;
  	
    if (torque.magnitude > Mathf.Epsilon) {
      transform.RotateAround(worldPosition, Camera.main.transform.right, torque.x);
      transform.RotateAround(worldPosition, Camera.main.transform.up, torque.y);
      torque *= angularDrag;
    }
  		
    transform.rotation = Quaternion.Slerp(transform.localRotation, startingRotation, Time.deltaTime * 2f);      
  }
}
