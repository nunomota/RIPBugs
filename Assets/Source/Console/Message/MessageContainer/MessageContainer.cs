using UnityEngine;
using System.Collections;

//this class will mimic an implementation of a circular array
public class MessageContainer {

	private const int defaultNumberOfMessages = 20;
	private int messagesStored;

	private Message[] messages;		//Array of Messages sotred
	private int maxMessages;		//Limit of messages to be stored
	private int indexOfLast;		//index of the last element of the array

	public MessageContainer(int numberOfMessages) {
		this.messagesStored = 0;
		this.maxMessages = numberOfMessages;
		this.messages = new Message[(numberOfMessages > 0) ? numberOfMessages : defaultNumberOfMessages];
		this.indexOfLast = messages.Length-1;
	}

	//adds a new message to the end of the array
	public void addMessage(Message message) {
		incIndex(ref indexOfLast);
		this.messages[indexOfLast] = message;
		messagesStored += (messagesStored < maxMessages)? 1: 0;
	}

	//increments a certain index
	private void incIndex(ref int targetIndex) {
		targetIndex = ((targetIndex+1) < maxMessages) ? targetIndex+1: 0;
	}

	//returns all messages stored as a single string
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

	//returns the number of stored messages
	public int Count() {
		return this.messagesStored;
	}
}
