using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simple filter used to enable user to choose which <see cref="MessageType"/> beome visible in the <see cref="Console"/>
/// </summary>
public static class MessageFilter {

	private static List<MessageType> typeList = new List<MessageType>();

	/// <summary>
	/// Register the specified <see cref="MessageType"/>.
	/// </summary>
	/// <param name="messageType"><see cref="MessageType"/> to be added.</param>
	public static void register(MessageType messageType) {
		typeList.Add(item: messageType);
	}

	/// <summary>
	/// Toogle the specified <see cref="MessageType"/> visibility by tag.
	/// </summary>
	/// <param name="tag">Target tag.</param>
	/// <param name="visibility">Desired visibility.</param>
	public static void toogle(string tag, bool visibility) {
		MessageType messageType = find (tag: tag);
		if (messageType != null) {
			RIPBugs.console.writeLine(msg: string.Format("Turning '{0}' tag '{1}'", tag, (visibility)? "on": "off"), priority: 1);
			messageType.toogle(visibility: visibility);
		} else {
			RIPBugs.console.writeLine(msg: string.Format("Could not find '{0}' tag...", tag), priority: 2);
		}
	}

	/// <summary>
	/// Toogle the specified tag's visibility.
	/// </summary>
	/// <param name="tag">Target <see cref="MessageType"/>.</param>
	/// <param name="visibility">Desired visibility.</param>
	public static void toogle(MessageType messageType, bool visibility) {
		messageType.toogle(visibility: visibility);
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