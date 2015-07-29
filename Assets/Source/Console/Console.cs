using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console {

	/*
	TODO change implementations from Array to Linked List
	 */
	private static int maxMessages = 20;	//maximum number of lines the console will keep stored
	private Message[] msgList;				//list of stored messages
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
	private MessageType consoleMessage;

	//Texturing
	private GUISkin defaultSkin;
	private GUIStyle windowStyle, textAreaStyle;

	//class' constructor
	public Console(int width = 320, int height = 200, Vector2 position = default(Vector2)) {
		this.consoleMessage = new MessageType("[Console]");
		this.msgList = new Message[maxMessages];
		this.indexOfLast = 0;
		this.width = width;
		this.height = height;
		this.position = position;
		updateRect();

		this.isVisible = false;

		this.scrollPosition = new Vector2(0.0f, maxMessages*25.0f);
		this.userCommand = "";
		commands = new Queue<Command>();

		setupCustomStyles();
		writeLine("Initialized successfully!", consoleMessage, 0);
	}

	private void setupCustomStyles() {
		windowStyle = new GUIStyle();
		windowStyle.normal.background = Resources.Load ("Console/Background") as Texture2D;
		textAreaStyle = new GUIStyle();
		textAreaStyle.normal.textColor = Color.white;
		textAreaStyle.stretchWidth = false;
		textAreaStyle.stretchHeight = false;
		textAreaStyle.wordWrap = true;
		textAreaStyle.richText = true;
		textAreaStyle.font = Resources.Load ("Console/OpenSans") as Font;
		textAreaStyle.fontSize = 12;
		textAreaStyle.normal.background = Resources.Load ("Console/Background") as Texture2D;
	}

	//main console update
	public void update() {

	}

	//used to draw console on screen
	public void OnGUI() {
		if (this.isVisible) {
			GUILayout.BeginArea(this.rect, "Console", windowStyle);
				GUILayout.BeginScrollView(
					scrollPosition,
					false,
					true,
					GUI.skin.horizontalScrollbar,
					GUI.skin.verticalScrollbar
					);
					GUILayout.TextArea(messageArrayAsString(), textAreaStyle);
				GUILayout.EndScrollView();
				GUILayout.BeginHorizontal();
					userCommand = GUILayout.TextField(userCommand, GUILayout.Width(this.width - sendButtonWidth - GUI.skin.textField.border.right
			                                                           											- GUI.skin.button.border.left
			                                                           											- GUI.skin.window.border.right
			                                                           											- GUI.skin.window.border.left));
					if (GUILayout.Button("Send", GUILayout.Width(sendButtonWidth)) && userCommand.Length > 0) {
						writeLine("Queueing command '" + userCommand + "'", consoleMessage, 1);
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
			fullString += msgList[i].getText() + ((i == msgList.Length-1 || i == indexOfLast-1) ? "" : "\n");
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
		writeLine("Console was toggled!", consoleMessage, 0);
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

	//adds a new line to the console
	//priority sets a priority for the message
	public void writeLine(string msg, MessageType messageType = default(MessageType), int priority = 0) {
		if (indexOfLast == maxMessages-1) {
			for (int i = 0; i < msgList.Length-1; i++) {
				msgList[i] = msgList[i+1];
			}
			msgList[indexOfLast] = new Message(msg, messageType, priority);
		} else {
			msgList[indexOfLast] = new Message(msg, messageType, priority);
			indexOfLast++;
		}
	}
}
