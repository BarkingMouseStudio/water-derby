using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetsBehaviour : MonoBehaviour {
  public int rows = 3;
  public float targetSpeed = 2;
  public float minRate = 2;
  public float maxRate = 4;
  public float maxDistance = 5;

  public Vector3 direction = new Vector3(1, 0, 0);
  public Vector3 initialPosition = new Vector3(0, 0, 0);

  public Quaternion orientation = Quaternion.Euler(0, 0, 0);

  private Queue<GameObject> targets;
  private List<GameObject> activeTargets;
  
  private int targetCapacity = 80;
  private bool spawningTargets = false;


  public void Awake() {
    targets = new Queue<GameObject>(targetCapacity);
    activeTargets = new List<GameObject>(targetCapacity);

    GameObject targetPrefab = Resources.Load("Prefabs/Target") as GameObject;
    GameObject target;
    for (int i = 0; i < targetCapacity; i++) {
      target = Instantiate(targetPrefab, Vector3.zero, orientation) as GameObject;
      target.transform.parent = transform;
      target.SetActive(false);
      
      targets.Enqueue(target);
      
    }
  }

  public void Start() {
    StartCoroutine(SpawnTargets());
  }

  public void Update() {
    GameObject target;
    for (int i = 0; i < activeTargets.Count; i++) {
      target = activeTargets[i];
      target.transform.Translate(direction * Time.deltaTime);
      if (Mathf.Abs(target.transform.localPosition.x) > maxDistance) {
        activeTargets.Remove(target);
        target.SetActive(false);
        targets.Enqueue(target);
      }
    }

    if (!spawningTargets && targets.Count > 0) {
      StartCoroutine(SpawnTargets());
    }
  }
  
  private IEnumerator SpawnTargets() {
    spawningTargets = true;
    while (targets.Count > 0) {
      yield return new WaitForSeconds(Random.Range(minRate, maxRate));

      GameObject target = targets.Dequeue();
      target.transform.position = transform.TransformPoint(initialPosition);
      target.SetActive(true);
      activeTargets.Add(target);
    }
    spawningTargets = false;
  }
  
}
