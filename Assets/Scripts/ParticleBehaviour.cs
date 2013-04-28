using UnityEngine;
using System.Collections;

public class ParticleBehaviour : MonoBehaviour {

	RaycastHit[] hits;
	RaycastHit hit;
	Ray ray;
	TargetBehaviour target;
	
	
	// Update is called once per frame
	void Update () {
	  ray = new Ray(transform.position, transform.forward);
      hits = Physics.RaycastAll(ray, Mathf.Infinity);
      for (int i = hits.Length - 1; i >= 0; i--) {
        hit = hits[i];
        Debug.Log(hit.transform.name);
        target = hit.transform.GetComponent<TargetBehaviour>();
        target.hitTime += 0.05f; 
      }
	}
}
