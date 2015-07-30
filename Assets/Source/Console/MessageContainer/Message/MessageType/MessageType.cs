using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageType {

	private List<string> textColors;

	private string tag;

	public MessageType (string tag) {
		this.textColors = new List<string>();
		this.textColors.Add("white");
		this.textColors.Add("yellow");
		this.textColors.Add ("red");

		this.tag = tag;
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
}
