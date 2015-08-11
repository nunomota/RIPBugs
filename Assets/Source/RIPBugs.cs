using UnityEngine;
using System.Collections;

/// <summary>
/// Main class of the whole package.
/// </summary>
public static class RIPBugs {

	private static bool isActive = false;									//toggles debugger on/off
	public static Console console = new Console(width: 1280, height: 720);	//main console used by debugger
	private static CommandHandler commandHandler = new CommandHandler();	//handler that executes and interprets commands

	/// <summary>
	/// Enable all the services associated with this class (like Console and real-time Vectors).
	/// </summary>
	public static void enable() {
		isActive = true;
	}

	/// <summary>
	/// Method to be called from the Update method inside the Main script of the user's Unity project.
	/// </summary>
	public static void update() {
		if (isActive) {
			if (checkKeyPressed(keys: new KeyCode[] {KeyCode.LeftControl, KeyCode.LeftShift})) {
				console.toggle();
			}
			if (console.isActive()) {
				console.update();
				executeCommand(command: console.getNextCommand());
			}
		}
	}

	/// <summary>
	/// Method to be called from the OnGUI method inside the Main script of the user's Unity project.
	/// </summary>
	public static void OnGUI() {
		console.OnGUI();
	}
	
	/// <summary>
	/// Checks if key combination is being pressed or not.
	/// </summary>
	/// <returns><c>true</c>, if key combination was just pressed, <c>false</c> otherwise.</returns>
	/// <param name="keys">Array of KeyCodes to check.</param>
	private static bool checkKeyPressed(KeyCode[] keys) {

		//make sure that prolongued key press 
		//does not trigger many events
		bool newKeyPress = false;

		for (int i = 0; i < keys.Length; i++) {
			if (Input.GetKeyDown(key: keys[i])) {
				newKeyPress = true;
			} else if (Input.GetKey(key: keys[i])) {
				continue;
			} else {
				return false;
			}
		}
		return newKeyPress;
	}

	/// <summary>
	/// Executes the Command <see cref="Command"/>.
	/// </summary>
	/// <param name="command">Target <see cref="Command"/> to be executed.</param>
	private static void executeCommand(Command command) {
		if (command != null) {
			console.writeLine(msg: string.Format("Executing command '{0}' with {1} option(s)", command.getName(), command.getOptions().Length));
			if (commandHandler.execute(command: command) < 0) {
				console.writeLine(msg: string.Format("Command '{0}' could not be executed...", command.getName()), priority: 2);
			} else {
				console.writeLine(msg: string.Format("Command '{0}' finished execution", command.getName()), priority: 0);
			}
		}
	}
}
