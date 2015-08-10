using UnityEngine;
using System.Collections;

/// <summary>
/// Used as a container for all <see cref="Message"/>. It will mimic an implementation of a circular array.
/// </summary>
public class MessageContainer {

	private const int defaultNumberOfMessages = 20;
	private int messagesStored;

	private Message[] messages;		//Array of Messages sotred
	private int maxMessages;		//Limit of messages to be stored
	private int indexOfLast;		//index of the last element of the array

	private MessageType messageType;

	/// <summary>
	/// Initializes a new instance of the <see cref="MessageContainer"/> class.
	/// </summary>
	/// <param name="numberOfMessages">Number of maximum messages that can be stored.</param>
	public MessageContainer(int numberOfMessages) {
		this.messagesStored = 0;
		this.maxMessages = numberOfMessages;
		this.messages = new Message[(numberOfMessages > 0) ? numberOfMessages : defaultNumberOfMessages];
		this.indexOfLast = messages.Length-1;

		this.messageType = new MessageType("[MsgCont]");
		this.messageType.addColors("blue");
	}
	
	/// <summary>
	/// Adds a new message to the end of the array.
	/// </summary>
	/// <param name="message"><see cref="Message"/> to be added.</param>
	public void addMessage(Message message) {
		if (message.getType().getVisibility()) {
			Debug.Log("MessageType enabled, adding message");
			incIndex(ref indexOfLast);
			this.messages[indexOfLast] = message;
			messagesStored += (messagesStored < maxMessages)? 1: 0;
		} else {
			Debug.Log("MessageType disabled, discarding message");
		}
	}
	
	/// <summary>
	/// Increments a certain index.
	/// </summary>
	/// <param name="targetIndex">Target index.</param>
	private void incIndex(ref int targetIndex) {
		targetIndex = ((targetIndex+1) < maxMessages) ? targetIndex+1: 0;
	}
	
	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>All <see cref="Message"/> stored as a single string.</returns>
	public override string ToString() {
		string fullString = "";
		//if indexOfLast is the last index, incIndex(indexOfLast) will be the 1st
		int curIndex = indexOfLast;
		incIndex(ref curIndex);
		while (curIndex != indexOfLast) {
			if (messages[curIndex] != null) {
				fullString += messages[curIndex].getText() + "\n";
			}
			incIndex(ref curIndex);
		}
		//last line will not have "\n" at the end
		if (messages[curIndex] != null) {
			fullString += messages[curIndex].getText();
		}
		return fullString;
	}
	
	/// <summary>
	/// Gets the number of stored messages.
	/// </summary>
	public int Count() {
		return this.messagesStored;
	}
}
