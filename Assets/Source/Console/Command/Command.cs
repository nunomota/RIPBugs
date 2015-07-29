using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Command {

	private string command;				//command requested by user
	private Queue<string> options;		//list of options specified for command

	//class' constructor
	public Command(string line) {
		this.options = new Queue<string>();
		parseCommand(line);
	}

	//method used to parse the user input
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

	//get the command name
	public string getName() {
		return this.command;
	}

	//get specified options
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
