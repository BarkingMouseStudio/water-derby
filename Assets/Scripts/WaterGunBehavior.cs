using UnityEngine;
using System.Collections;

public class WaterGunBehavior : MonoBehaviour {

  private Vector3 prevMouse = Vector3.zero;
  private Vector3 center = Vector3.zero;
  private float centerSpeed = 1;
  private float gravity = 0.9;

	void Update () {
    if (Input.GetMouseButtonDown(0)) {
      // Add the mouse position to center
      var mouseViewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);

      center = mouseViewport - prevMouse;

      // Move gun center towards zero
      center = Vector3.Lerp(center, Vector3.zero, Time.deltaTime * centerSpeed);
      center.z = Camera.main.farClipPlane;

      var aim = Camera.main.ViewportToWorldPoint(center);
      var target = Quaternion.LookRotation(aim);
      transform.localRotation = target;

      prevMouse = mouseViewport;
    }

    center *= gravity;
	}
}
