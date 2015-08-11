using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that defines a message type.
/// </summary>
public class MessageType {

	private List<string> textColors;
	private bool isVisible;
	private string tag;

	/// <summary>
	/// Initializes a new instance of the <see cref="MessageType"/> class.
	/// </summary>
	/// <param name="tag">Tag to be used for the instance.</param>
	public MessageType (string tag) {
		this.textColors = new List<string>();
		this.textColors.Add("white");
		this.textColors.Add("yellow");
		this.textColors.Add ("red");
		this.isVisible = true;

		this.tag = tag;
		MessageFilter.register(this);
	}
	
	/// <summary>
	/// Method used by console to color text in RTF.
	/// </summary>
	/// <returns>The colored text.</returns>
	/// <param name="line">The message's text.</param>
	/// <param name="priority">Priority.</param>
	public string colorText(string line, int priority = 0) {
		return string.Format("<color={0}>{1} {2}</color>", (priority < textColors.Count) ? textColors[priority] : textColors[0], tag, line);
	}
	
	/// <summary>
	/// Used to add custom colors to this <see cref="MessageType"/>.
	/// </summary>
	/// <param name="newColors">New colors to be added.</param>
	public void addColors(params string[] newColors) {
		for (int i = 0; i < newColors.Length; i++) {
			textColors.Add(item: newColors[i]);
		}
	}

	/// <summary>
	/// Toogle the instance visibility.
	/// </summary>
	/// <param name="visibility">Visibility of all <see cref="Message"/> instances of this type.</param>
	public void toogle (bool visibility) {
		this.isVisible =  visibility;
	}

	/// <summary>
	/// Turns visibility of all <see cref="Message"/> instances to true.
	/// </summary>
	public void show() {
		this.isVisible = true;
	}

	/// <summary>
	/// Turns visibility of all <see cref="Message"/> instances to false.
	/// </summary>
	public void hide() {
		this.isVisible = false;
	}

	/// <summary>
	/// Gets the visibility of a <see cref="MessageType"/>.
	/// </summary>
	/// <returns>Boolean value representing the visibility of <see cref="Message"/> of this type.</returns>
	public bool getVisibility() {
		return this.isVisible;
	}
	
	/// <summary>
	/// Used to return the tag of the <see cref="MessageType"/>.
	/// </summary>
	/// <returns>The tag as a string.</returns>
	public string getTag() {
		return this.tag;
	}
}
