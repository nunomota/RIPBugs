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
		if (messageType != null) {
			RIPBugs.console.writeLine(string.Format("Turning '{0}' tag '{1}'", tag, (visibility)? "on": "off"), priority: 1);
			messageType.toogle(visibility);
		} else {
			RIPBugs.console.writeLine(string.Format("Could not find '{0}' tag...", tag), priority: 2);
		}
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