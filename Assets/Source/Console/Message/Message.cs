using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to store a single message's content (used by the console)
/// </summary>
public class Message {

	private string messageText;
	private MessageType messageType;
	private int priority;

	private MessageType defaultMessageType;

	/// <summary>
	/// Initializes a new instance of the <see cref="Message"/> class.
	/// </summary>
	/// <param name="messageText">Message text.</param>
	/// <param name="messageType">Message type.</param>
	/// <param name="priority">Priority.</param>
	public Message(string messageText, MessageType messageType = default(MessageType), int priority = 0) {
		this.messageText = messageText;
		this.defaultMessageType = new MessageType(">>");
		this.messageType = (messageType != null) ? messageType : defaultMessageType;
		this.priority = priority;
	}

	/// <summary>
	/// Gets the <see cref="MessageType"/> associated with a <see cref="Message"/> instance.
	/// </summary>
	/// <returns>The <see cref="MessageType"/>.</returns>
	public MessageType getType() {
		return messageType;
	}

	/// <summary>
	/// Gets the text a <see cref="Message"/> contains.
	/// </summary>
	/// <returns>A RTF string, formatted according to the <see cref="MessageType"/> specifications.</returns>
	public string getText() {
		return messageType.colorText(messageText, priority);
	}
}
