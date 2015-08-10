using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Command issue in the <see cref="Console"/>.
/// </summary>
public class Command {

	private string command;				//command requested by user
	private Queue<string> options;		//list of options specified for command

	/// <summary>
	/// Initializes a new instance of the <see cref="Command"/> class.
	/// </summary>
	/// <param name="line"><see cref="Command"/> as a string.</param>
	public Command(string line) {
		this.options = new Queue<string>();
		parseCommand(line);
	}
	
	/// <summary>
	/// Parses the command.
	/// </summary>
	/// <param name="line">User input string.</param>
	private void parseCommand(string line) {
		string[] splitLine = line.Split(new char[]{' '});
		command = splitLine[0];
		/*
		TODO not assume the string comes well formated (prevent double spaces, etc...)
		 */
		for (int i = 1; i < splitLine.Length; i++) {
			if (splitLine[i].Length != 0) {
				options.Enqueue(splitLine[i]);
			}
		}
	}

	/// <summary>
	/// Gets the name of the <see cref="Command"/>.
	/// </summary>
	public string getName() {
		return this.command;
	}
	
	/// <summary>
	/// Gets the options specified by the user.
	/// </summary>
	/// <returns>Array of options.</returns>
	public string[] getOptions() {
		string[] array = new string[options.Count];
		IEnumerable opts = options;
		int index = 0;
		foreach(string option in opts) {
			array[index++] = option;
		}
		return array;
	}
}
