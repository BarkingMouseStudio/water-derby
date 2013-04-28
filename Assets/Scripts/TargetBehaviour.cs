using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

  public int row;
  public int number;
  
  public float hitTime = 0;
    
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
}
