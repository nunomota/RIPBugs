using UnityEngine;
using System.Collections;

public class Console {

	private static int maxMessages = 20;	//maximum number of lines the console will keep stored
	private string[] msgList;				//list of stored messages
	private int indexOfLast;				//index of last string inserted (needed before reaching 'maxMessages')
	private int width, height;				//dimensions of the console window
	private Vector2 position;				//current position of the console
	private Rect rect;

	private bool isVisible;

	//class' constructor
	public Console(int width = 320, int height = 200, Vector2 position = default(Vector2)) {
		this.msgList = new string[maxMessages];
		this.indexOfLast = 0;
		this.width = width;
		this.height = height;
		this.position = position;
		updateRect();

		this.isVisible = true;
	}

	//main console update
	public void update() {

	}

	//used to draw console on screen
	public void OnGUI() {
		if (this.isVisible) {
			GUILayout.BeginArea(this.rect, "Console", GUI.skin.window);
				GUILayout.Box(Resources.Load ("Console/Background") as Texture);
				GUI.enabled = true;
			GUILayout.EndArea();
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
		Debug.Log ("Console was toggled!");
	}

	//appends text at the end of current line
	public void write(string msg) {

	}

	//adds a new line to the console
	public void writeLine(string msg) {

	}
}
