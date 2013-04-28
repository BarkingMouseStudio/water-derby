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
	        isActiveGame = true;
	        screens.startScreenEnd();
	        targets.Enable();
	        
	        watergun.enabled = true;
	        time = 0;

	        break;
	      case "Play Again":
            isActiveGame = true;
	        screens.startScreenEnd();
	        targets.Enable();
	        
	        watergun.enabled = true;
	        time = 0;


          break;
        }
      }
	}
  }
}
