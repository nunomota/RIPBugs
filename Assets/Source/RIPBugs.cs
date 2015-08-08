using UnityEngine;
using System.Collections;

public static class RIPBugs {

	private static bool isActive = false;									//toggles debugger on/off
	public static Console console = new Console(1280, 720);					//main console used by debugger
	private static CommandHandler commandHandler = new CommandHandler();	//handler that executes and interprets commands

	public static void enable() {
		isActive = true;
	}

	public static void update() {
		if (isActive) {
			if (togglePressed(new KeyCode[] {KeyCode.LeftControl, KeyCode.LeftShift})) {
				console.toggle();
			}
			if (console.isActive()) {
				console.update();
				executeCommand(console.getNextCommand());
			}
		}
	}

	public static void OnGUI() {
		console.OnGUI();
	}

	//checks if key combination is being pressed or not
	private static bool togglePressed(KeyCode[] keys) {

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

	private static void executeCommand(Command command) {
		if (command != null) {
			console.writeLine(string.Format("Executing command '{0}' with {1} option(s)", command.getName(), command.getOptions().Length));
			if (commandHandler.execute(command) < 0) {
				console.writeLine(string.Format("Command '{0}' could not be executed...", command.getName()), priority: 2);
			} else {
				console.writeLine(string.Format("Command '{0}' finished execution", command.getName()), priority: 0);
			}
		}
	}
}
