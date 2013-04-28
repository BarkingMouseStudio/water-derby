using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

  public int row;
  public int number;
  public bool isHit = false;

  private TextMesh textMesh;

  void Awake() {
    textMesh = GetComponentInChildren<TextMesh>();
  }

  void Start() {
    textMesh.text = number.ToString();
  }
}
