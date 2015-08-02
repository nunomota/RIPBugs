using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console {
	
	private static int maxMessages = 20;	//maximum number of lines the console will keep stored
	private MessageContainer msgContainer;	//container of stored messages
	private int width, height;				//dimensions of the console window
	private Vector2 position;				//current position of the console
	private Rect rect;						//Rect that defines position and size of console

	private bool isVisible;					//determines whether terminal is visible or not

	private Vector2 scrollPosition;
	private string userCommand;
	private Queue<Command> commands;		//queue of received commands

	//other variables irrelevant to logics
	private MessageType consoleMessage;

	//Texturing
	private GUISkin defaultSkin;
	private GUIStyle windowStyle, textAreaStyle, scrollBarStyle;

	//class' constructor
	public Console(int width = 320, int height = 200, Vector2 position = default(Vector2)) {
		this.consoleMessage = new MessageType("[Console]");
		this.msgContainer = new MessageContainer(maxMessages);
		this.width = width;
		this.height = height;
		this.position = position;
		updateRect();

		this.isVisible = false;

		this.scrollPosition = new Vector2(0.0f, maxMessages*25.0f);
		this.userCommand = "";
		commands = new Queue<Command>();

		setupCustomStyles();
		Input.eatKeyPressOnTextFieldFocus = false;
		writeLine("Initialized successfully!", consoleMessage, 0);
	}

	private void setupCustomStyles() {
		int consolefontSize = 12;
		Font consoleFont = Resources.Load ("Console/OpenSans") as Font;

		windowStyle = new GUIStyle();
		windowStyle.alignment = TextAnchor.UpperCenter;
		windowStyle.normal.textColor = Color.white;
		windowStyle.normal.background = Resources.Load ("Console/Background") as Texture2D;
		windowStyle.normal.background.alphaIsTransparency = true;
		windowStyle.font = consoleFont;
		windowStyle.fontSize = consolefontSize;

		textAreaStyle = new GUIStyle();
		textAreaStyle.normal.textColor = Color.white;
		//textAreaStyle.border.top = topTitleHeight;
		//textAreaStyle.padding.top = topTitleHeight;
		textAreaStyle.padding.left = 5;
		textAreaStyle.stretchWidth = false;
		textAreaStyle.stretchHeight = false;
		textAreaStyle.wordWrap = true;
		textAreaStyle.richText = true;
		textAreaStyle.font = consoleFont;
		textAreaStyle.fontSize = consolefontSize;
		//textAreaStyle.overflow.top = 0;
		//textAreaStyle.normal.background = Resources.Load ("Console/Background") as Texture2D;
		//textAreaStyle.normal.background.alphaIsTransparency = true;

		scrollBarStyle = new GUIStyle();
		scrollBarStyle.normal.background = Resources.Load("Console/ScrollBar/Background") as Texture2D;
	}

	//main console update
	public void update() {
		handleInput();
	}

	//used to draw console on screen
	public void OnGUI() {
		if (this.isVisible) {
			GUILayout.BeginArea(this.rect, "Console", GUI.skin.window);
				/* 
				 * Ideally the scrollBar should always keep up with the bottom messages but that seems to be impossible using GUILayout.BeginScrollView(). Why?
				 * 		> We can use two different modes
				 * 		1) Manual
				 * 			+ really simple to implement
				 * 			- the console will not follow the new messages, so it's the users' job to keep scrolling
				 * 		2) Automatic
				 * 			+ we would pass a randomly large number as an argument for 'scrollPosition' (which had to be greater or equal to the maximum scroll value)
				 * 			+ if greater, the ScrollView would automatically adjust that value to its actual maximum
				 * 			- that iplementation would be really 'dirty'
				 * 			- We can never know how big of a scroll value there can be, even if we know the maximum messages we can store
				 * 			(any message can have from 1 to a bunch of lines; e.x. if I wanted to print a stack trace)
				 * 			- IF we assume a big value and it is not enought to reach the maximum value of the scroll, the user might get his scrolling locked somewhere between old messages
				 * 
				 * Solutions:
				 * 		1) don't use wordWrap, then we know each message really has 1 line (with any amount of characters) and we can know each line's height with GUIStyle.lineHeight
				 * 			+ this allows us to calculate the maximum scroll value, using each line's height and the number of lines
				 * 			- the user experience would be worst, as lines that get out of the screen too the sides get harder to read (even with an horizontal scroll)
				 * 		2) develop a custom TextArea
				 * 			+ this new custom implementation would give access to: current scroll value, max scroll value, number of lines (NOT number of messages), line height
				 * 			+ also, we could now limit the number of lines and not the number of messages
				 * 			- would be a bit of an overkill for this purpose
				 * 		3) Limit the number of lines (not messages) in the default TextArea
				 * 			> The only reason this is not being done now is that we would have to know 2 things
				 * 			1) the width of each character (which would only be easy for monospaced fonts)
				 * 			2) the width of the TextArea (so we can know where the line will be interrupted with wordWrap, to calculate the number of lines a certain message will take)
				 * 		4) Maybe something really simple I did not think of...
			 	*/
				scrollPosition = GUILayout.BeginScrollView(
					scrollPosition,
					false,
					true,
					GUI.skin.horizontalScrollbar,
					GUI.skin.verticalScrollbar
					);
					GUILayout.TextArea(msgContainer.ToString(), textAreaStyle);
				GUILayout.EndScrollView();
				GUILayout.BeginHorizontal();
					GUI.SetNextControlName("commandTextField");
					userCommand = GUILayout.TextField(userCommand, GUILayout.Width(this.width - GUI.skin.textField.border.right
			                                                           						  - GUI.skin.window.border.right
			                                                           						  - GUI.skin.window.border.left));
				GUILayout.EndHorizontal();
			GUILayout.EndArea();
			GUI.FocusControl("commandTextField");

		}
	}

	//method used to handle various user inputs
	private void handleInput() {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			queueCommand(new Command(userCommand));
		}
	}

	private void queueCommand(Command newCommand) {
		if (userCommand.Length > 0) {
			writeLine("Queueing command '" + userCommand + "'", consoleMessage, 1);
			commands.Enqueue(newCommand);
			userCommand = "";
		}
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
		msgContainer.addMessage(new Message(msg, messageType, priority));
	}
}
