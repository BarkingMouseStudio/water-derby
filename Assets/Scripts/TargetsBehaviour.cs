using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetsBehaviour : MonoBehaviour {

  public Transform row0;
  public Transform row1;
  public Transform row2;

  public int rows = 3;
  public float targetSpeed = 2;
  public float minRate = 0;
  public float maxRate = 0.5f;
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
    row0Position = new Vector3(row0.position.x - row0.renderer.bounds.extents.x, row0.position.y + row0.renderer.bounds.extents.y + 0.3f, -0.86f);
    row1Position = new Vector3(row1.position.x - row1.renderer.bounds.extents.x, row1.position.y + row1.renderer.bounds.extents.y + 0.3f, -0.46f);
    row2Position = new Vector3(row2.position.x - row2.renderer.bounds.extents.x, row2.position.y + row2.renderer.bounds.extents.y + 0.3f, -0.08f);
    
    GameObject targetObject;
    TargetBehaviour target;
    
    for (int i = targetCapacity - 1; i >= 0; i--) {
      targetObject = Instantiate(targetPrefab, Vector3.zero, orientation) as GameObject;
      targetObject.transform.parent = transform;
      
      target = targetObject.GetComponent<TargetBehaviour>();  
      target.number = GetUnusedNumber();
      target.transform.rotation = Quaternion.Euler(0, 90, 0);

      targets.Enqueue(target);
    }    
  }
	
  public void Enable() {
    if (!spawningTargets && targets.Count > 0) {
      StartCoroutine(SpawnTargets());
    }
  }
  
  public void GameOver() {
    hitString = "";
    
    TargetBehaviour[] allTargets = GetComponentsInChildren<TargetBehaviour>();
    TargetBehaviour target;
    for (int i = allTargets.Length - 1; i >= 0; i--) {
      target = allTargets[i];
      if (target.isHit == true) {
        hitString += target.number + ",";
      }
      target.hitTime = 0;
      target.isHit = false;
      target.Disable();
    }
    Application.ExternalCall("gameOver", hitString);
    
  }
  
  public void Update() {
    TargetBehaviour target;
    for (int i = 0; i < activeTargets.Count; i++) {
      target = activeTargets[i];
      target.transform.Translate(direction * Time.deltaTime);
      if (Mathf.Abs(target.transform.localPosition.x) > maxDistance) {
        target.Disable();
        activeTargets.Remove(target);
        targets.Enqueue(target);
      }
    }

    if (!spawningTargets && targets.Count > 0) {
      StartCoroutine(SpawnTargets());
    }
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
  
  private Vector3 GetInitialPosition(int row){
    switch (row) {
      case 1:
        initialPosition = row1Position; 
        break;
      case 2: 
        initialPosition = row2Position;
        break;
      default:
        initialPosition = row0Position;
        break;
    }
    
    return initialPosition;
  }
  
  private IEnumerator SpawnTargets() {
    spawningTargets = true;
    TargetBehaviour target;
    while (targets.Count > 0) {
      yield return new WaitForSeconds(Random.Range(0.75f, 2f));

      target = targets.Dequeue();
      target.row = Mathf.FloorToInt(Random.Range(0,3));
      target.transform.position = transform.TransformPoint(GetInitialPosition(target.row));
      target.Enable();
      
      activeTargets.Add(target);
      
    }
    spawningTargets = false;
  }

}
