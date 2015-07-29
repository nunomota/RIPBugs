using UnityEngine;
using System.Collections;

public class RIPBugs {

	private bool isActive;			//toggles debugger on/off
	private Console console;		//main console used by debugger

	public RIPBugs() {
		this.isActive = false;
		this.console = new Console();
	}

	public void enable() {
		this.isActive = true;
	}

	public void update() {
		if (this.isActive) {
			if (togglePressed(new KeyCode[] {KeyCode.LeftControl, KeyCode.LeftAlt, KeyCode.D})) {
				console.toggle();
			}
			if (console.isActive()) {
				console.update();
				executeCommand(console.getNextCommand());
			}
		}
	}

	public void OnGUI() {
		console.OnGUI();
	}

	//checks if key combination is being pressed or not
	private bool togglePressed(KeyCode[] keys) {

		//make sure that prolongued key press 
		//does not trigger many events
		bool newKeyPress = false;

		for (int i = 0; i < keys.Length; i++) {
			if (Input.GetKeyDown(keys[i])) {
				newKeyPress = true;
			} else if (Input.GetKey(keys[i])) {
				continue;
			} else {
				return false;
			}
		}
		return newKeyPress;
	}

	private void executeCommand(Command command) {
		if (command != null) {
			console.writeLine(string.Format("Executing command '{0}' with {1} option(s)", command.getName(), command.getOptions().Length));
		}
	}
}
