using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {
	
  public GameObject targetsObject;
  public GameObject particlesObject;
  public GameObject watergunObject;
  public GameObject screensObject;

  private TargetsBehaviour targets;
  private ParticleBehaviour particles;
  private WaterGunBehavior watergun;
  private ScreenBehaviour screens;
  
  private bool isActiveGame = false;
  private RaycastHit[] hits;
	
  private float gameDuration = 10;
  private float time;
  	
  // Use this for initialization
  void Start () {
    screens = screensObject.transform.GetComponent<ScreenBehaviour>();
    targets = targetsObject.transform.GetComponent<TargetsBehaviour>();
    watergun = watergunObject.transform.GetComponent<WaterGunBehavior>();
    particles = watergunObject.transform.GetComponentInChildren<ParticleBehaviour>();
    
    screens.startScreenStart();
  }
	
  // Update is called once per frame
  void Update () {
	if (Input.GetMouseButtonDown(0) && isActiveGame == false) {
	  RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      hits = Physics.RaycastAll(ray, Mathf.Infinity);
      for (int i = hits.Length - 1; i >= 0; i--) {
      hit = hits[i];

	    switch (hit.transform.name) {
	      case "StartGame":
	        targets.Enable();
	        isActiveGame = true;
	        screens.startScreenEnd();
	        targets.enabled = true;
	        particles.enabled = true;
	        watergun.enabled = true;
	        time = 0;

	        break;
	      case "Play Again":
	        targets.Enable();
            // targets.Restart();
            isActiveGame = true;
	        screens.successScreenEnd();
	        
	        targets.enabled = true;	        
	        watergun.enabled = true;
	        particles.enabled = true;
	        
	        time = 0;

           break;
        }
      }
	}
	if (isActiveGame) {
	  time = Time.deltaTime;
	  if (time > gameDuration) {
	    Debug.Log("GameOver");
	    isActiveGame = false;
	    screens.successScreenStart();
	    targets.enabled = false;
	    watergun.enabled = false;
	    particles.enabled = false;
	    
	    targets.GameOver();
	    
	  }
	  
	  
	}
  }
}
