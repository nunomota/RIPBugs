﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console {

	/*
	TODO change implementations from Array to Linked List
	 */
	private static int maxMessages = 20;	//maximum number of lines the console will keep stored
	private string[] msgList;				//list of stored messages
	private int indexOfLast;				//index of last string inserted (needed before reaching 'maxMessages')
	private int width, height;				//dimensions of the console window
	private Vector2 position;				//current position of the console
	private Rect rect;						//Rect that defines position and size of console

	private bool isVisible;					//determines whether terminal is visible or not

	private Vector2 scrollPosition;
	private string userCommand;
	private Queue<Command> commands;		//queue of received commands

	//other variables irrelevant to logics
	private int sendButtonWidth = 50;
	private string lineInitStr = ">> ";

	//class' constructor
	public Console(int width = 320, int height = 200, Vector2 position = default(Vector2)) {
		this.msgList = new string[maxMessages];
		this.indexOfLast = 0;
		this.width = width;
		this.height = height;
		this.position = position;
		updateRect();

		this.isVisible = false;

		this.scrollPosition = new Vector2(0.0f, maxMessages*25.0f);
		this.userCommand = "";
		commands = new Queue<Command>();
		writeLine("Initialized successfully!");
	}

	//main console update
	public void update() {

	}

	//used to draw console on screen
	public void OnGUI() {
		if (this.isVisible) {
			GUILayout.BeginArea(this.rect, "Console", GUI.skin.window);
				GUILayout.BeginScrollView(
					scrollPosition,
					false,
					true,
					GUI.skin.horizontalScrollbar,
					GUI.skin.verticalScrollbar
					);
					GUILayout.TextArea(messageArrayAsString());
				GUILayout.EndScrollView();
				GUILayout.BeginHorizontal();
					userCommand = GUILayout.TextField(userCommand, GUILayout.Width(this.width - sendButtonWidth - GUI.skin.textField.border.right
			                                                           											- GUI.skin.button.border.left
			                                                           											- GUI.skin.window.border.right
			                                                           											- GUI.skin.window.border.left));
					if (GUILayout.Button("Send", GUILayout.Width(sendButtonWidth)) && userCommand.Length > 0) {
						writeLine("Queueing command '" + userCommand + "'");
						commands.Enqueue(new Command(userCommand));
						userCommand = "";
					}
				GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
	}

	//turns the array of messages into a single string
	private string messageArrayAsString() {
		string fullString = "";
		for (int i = 0; i < msgList.Length && i < indexOfLast && indexOfLast > 0; i++) {
			fullString += lineInitStr + msgList[i] + ((i == msgList.Length-1 || i == indexOfLast-1) ? "" : "\n");
		}
		return fullString;
	}

	//method used to update the rect of the console
	private void updateRect() {
		this.rect = new Rect(this.position.x, this.position.y, width, height);
	}

	//sets a new position for the console
	public void setPosition(Vector2 position) {
		this.position = position;
		updateRect();
	}

	//used to turn this console on/off
	public void toggle() {
		this.isVisible = !this.isVisible;
		writeLine("Console was toggled!");
	}

	//used to know if terminal e showing or not
	public bool isActive() {
		return this.isVisible;
	}

	//gets next command in queue to be executed
	public Command getNextCommand() {
		if (commands.Count > 0) {
			return commands.Dequeue();
		}
		return null;
	}

	//appends text at the end of current line
	public void write(string msg) {
		msgList[indexOfLast] += msg;
	}

	//adds a new line to the console
	public void writeLine(string msg) {
		if (indexOfLast == maxMessages-1) {
			for (int i = 0; i < msgList.Length-1; i++) {
				msgList[i] = msgList[i+1];
			}
			msgList[indexOfLast] = msg;
		} else {
			msgList[indexOfLast] = msg;
			indexOfLast++;
		}
	}
}
