using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	private RIPBugs debugger;

	// Use this for initialization
	void Start () {
		debugger = new RIPBugs();
		debugger.enable();
	}
	
	// Update is called once per frame
	void Update () {
		debugger.update();
		checkInput();
	}

	private void checkInput() {
		if (Input.anyKeyDown) {
			Debug.Log ("some key was pressed!");
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			Debug.Log ("A was pressed");
		}
	}

	void OnGUI() {
		debugger.OnGUI();
	}
}
