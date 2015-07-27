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
	}

	void OnGUI() {
		debugger.OnGUI();
	}
}
