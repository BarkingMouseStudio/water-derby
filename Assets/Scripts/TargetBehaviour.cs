using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

  public int row;
  public int number;
  
  public float hitTime = 0;
  
  public bool isHit = false;
  
  private Transform text;

  private TextMesh textMesh;

  public void Awake () {
    textMesh = GetComponentInChildren<TextMesh>();
    text = transform.Find("Number");    
    text.transform.renderer.enabled = false;
    
    transform.renderer.enabled = false;
    transform.collider.enabled = false;
    transform.position = new Vector3(200,200,200);
  }
  
  public void ResetHitTime () {
    hitTime = 0;
    isHit = false;
  }
  
  public void Update () {
    if (isHit == false && hitTime >= 1) {
      isHit = true;
      StartCoroutine(RotateDown());
    }
  
  }
  
  public void Enable () {
    text.transform.renderer.enabled = true;
    transform.renderer.enabled = true;
    transform.collider.enabled = true;
    textMesh.text = number.ToString();
  }
  
  public void Disable () {
    text.renderer.enabled = false;
    transform.renderer.enabled = false;
    transform.collider.enabled = false;
  }
  private IEnumerator RotateDown () {
    text.renderer.enabled = false;
    
    float time = 0;
    while (Quaternion.Angle(transform.localRotation, Quaternion.identity) > Mathf.Epsilon) {
        time += Time.deltaTime;
        Quaternion target = Quaternion.Euler(90, 90, 0);        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, time * 0.02f);
        yield return null;
    }
  
  }
}
