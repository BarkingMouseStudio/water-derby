using UnityEngine;
using System.Collections;

public class SuccessBehavior : MonoBehaviour {

  private TextMesh textMesh;

  void SetText(string val) {
    if (textMesh) {
      textMesh.text = val;
    }
  }

  void Awake() {
    textMesh = GetComponentInChildren<TextMesh>();
  }

	void Start() {
    SetText("100!");
	}
}
