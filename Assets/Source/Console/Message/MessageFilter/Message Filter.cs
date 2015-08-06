using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MessageFilter {

	private static List<MessageType> typeList = new List<MessageType>();

	public static void register(MessageType messageType) {
		typeList.Add(messageType);
	}

	public static void toogle(string tag, bool visibility) {
		MessageType messageType = find (tag);
		messageType.toogle(visibility);
	}

	public static void toogle(MessageType messageType, bool visibility) {
		messageType.toogle(visibility);
	}

	//NOT optimized, change data structures
	private static MessageType find(string tag) {
		foreach (MessageType messageType in typeList) {
			if (messageType.getTag() == tag) {
				return messageType;
			}
		}
		return null;
	}
}