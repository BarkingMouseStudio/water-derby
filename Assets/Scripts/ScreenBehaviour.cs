using UnityEngine;
using System.Collections;

public class ScreenBehaviour : MonoBehaviour {
  public GameObject start;
  public GameObject countdown;
  public GameObject success;
  
  public void Restart() {
    start.SetActive(false);
    success.SetActive(false);
    countdown.SetActive(false);
  }
  	
  public void startScreenStart(){
    start.SetActive(true);
  }	
  
  public void startScreenEnd() {
    start.SetActive(false);
  }
  
  public void successScreenStart() {
    success.SetActive(true);
  }
  
  public void successScreenEnd() {
    success.SetActive(false);
  }	
}
