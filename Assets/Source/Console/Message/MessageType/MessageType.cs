using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageType {

	private List<string> textColors;
	private bool isVisible;
	private string tag;

	public MessageType (string tag) {
		this.textColors = new List<string>();
		this.textColors.Add("white");
		this.textColors.Add("yellow");
		this.textColors.Add ("red");
		this.isVisible = false;

		this.tag = tag;
		MessageFilter.register(this);
	}

	//method used by console to color text in RTF
	public string colorText(string line, int priority = 0) {
		return string.Format("<color={0}>{1} {2}</color>", (priority < textColors.Count) ? textColors[priority] : textColors[0], tag, line);
	}

	//used to add custom colors to text
	public void addColors(params string[] newColors) {
		for (int i = 0; i < newColors.Length; i++) {
			textColors.Add(newColors[i]);
		}
	}

	public void toogle (bool visibility) {
		this.isVisible =  visibility;
	}

	public void show() {
		this.isVisible = true;
	}

	public void hide() {
		this.isVisible = false;
	}

	public bool getVisibility() {
		return this.isVisible;
	}

	//used to return the tag of the MessageType
	public string getTag() {
		return this.tag;
	}
}
