using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetsBehaviour : MonoBehaviour {

  public Transform row0;
  public Transform row1;
  public Transform row2;

  public int rows = 3;
  public float targetSpeed = 2;
  public float minRate = 2;
  public float maxRate = 4;
  public float maxDistance = 5;

  public Vector3 direction = new Vector3(1, 0, 0);
  public Vector3 initialPosition = new Vector3(0, 0, 0);

  public Quaternion orientation = Quaternion.Euler(0, 0, 0);

  private Queue<TargetBehaviour> targets;
  private List<TargetBehaviour> activeTargets;
  
  private int targetCapacity = 80;
  private bool spawningTargets = false;
  private string hitString;
  
  private Vector3 row0Position;
  private Vector3 row1Position;
  private Vector3 row2Position;

  private GameObject targetPrefab;
  
  private HashSet<int> usedNumbers = new HashSet<int>();

  public void Awake() {
    targets = new Queue<TargetBehaviour>(targetCapacity);
    activeTargets = new List<TargetBehaviour>(targetCapacity);	
    targetPrefab = Resources.Load("Prefabs/Target") as GameObject;	
    row0Position = row0.position;
    row1Position = row1.position;
    row2Position = row2.position;
    
    Debug.Log(row0.collider.bounds.size);
  }
  
  public void Start() {
    GameObject targetObject;
    TargetBehaviour target;
    
    for (int i = targetCapacity - 1; i >= 0; i--) {
      targetObject = Instantiate(targetPrefab, Vector3.zero, orientation) as GameObject;
      targetObject.transform.parent = transform;
      
      target = targetObject.GetComponent<TargetBehaviour>();      
      target.renderer.enabled = false;
      target.number = GetUnusedNumber();

      if (i > 5) {
        hitString += target.number + ",";
      }        
      targets.Enqueue(target);
    }
    
    StartCoroutine(SpawnTargets());
    
    Application.ExternalCall("gameOver", hitString);
  }

  public void Update() {
    TargetBehaviour target;
    for (int i = 0; i < activeTargets.Count; i++) {
      target = activeTargets[i];
      target.transform.Translate(direction * Time.deltaTime);
      if (Mathf.Abs(target.transform.localPosition.x) > maxDistance) {
        target.renderer.enabled = false;
        activeTargets.Remove(target);
        targets.Enqueue(target);
      }
    }

    if (!spawningTargets && targets.Count > 0) {
      StartCoroutine(SpawnTargets());
    }
  }
  
  public void Results(string str) {
    Debug.Log("Results: " + str);
  
  }
  
  private int GetUnusedNumber() {
    int number;
    
    // ending range is exclusive so we use 81 and not 80
    number = Mathf.FloorToInt(Random.Range(1,81));

    if (usedNumbers.Contains(number)) {
      return GetUnusedNumber();
    
    } else {
      usedNumbers.Add(number);  
      return number;
    }
    
  }
  
  private IEnumerator SpawnTargets() {
    spawningTargets = true;
    TargetBehaviour target;
    while (targets.Count > 0) {
      yield return new WaitForSeconds(Random.Range(minRate, maxRate));

      target = targets.Dequeue();
      target.row = Mathf.FloorToInt(Random.Range(0,3));
      target.transform.position = transform.TransformPoint(initialPosition);
      target.renderer.enabled = true;
      activeTargets.Add(target);
      
    }
    spawningTargets = false;
  }

}
