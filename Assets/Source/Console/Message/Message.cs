using UnityEngine;
using System.Collections;

public class Message {

	private string messageText;
	private MessageType messageType;
	private int priority;

	private MessageType defaultMessageType;

	public Message(string messageText, MessageType messageType = default(MessageType), int priority = 0) {
		this.messageText = messageText;
		this.defaultMessageType = new MessageType(">>");
		this.messageType = (messageType != null) ? messageType : defaultMessageType;
		this.priority = priority;
	}

	public string getText() {
		return messageType.colorText(messageText, priority);
	}
}
