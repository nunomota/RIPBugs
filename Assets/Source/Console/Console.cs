using UnityEngine;
using System.Collections;

public class Console {

	private static int maxMessages = 20;	//maximum number of lines the console will keep stored
	private string[] msgList;				//list of stored messages
	private int indexOfLast;				//index of last string inserted (needed before reaching 'maxMessages')
	private Vector2 dimensions;				//dimensions of the console window

	//class' constructor
	public Console(Vector2 dimensions) {
		this.dimensions = dimensions;
		msgList = new string[maxMessages];
		indexOfLast = 0;
	}

	//appends text at the end of current line
	public void write(string msg) {

	}

	//adds a new line to the console
	public void writeLine(string msg) {

	}
}
