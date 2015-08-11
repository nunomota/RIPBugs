using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Child class of <see cref="InfoContainer"/>. Used to store a <see cref="Command"/> specifications.
/// </summary>
public class CommandInfo : InfoContainer {

	private List<FlagInfo> flags;

	/// <summary>
	/// Initializes a new instance of the <see cref="CommandInfo"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	public CommandInfo(string name) : base(name) {
		this.flags = new List<FlagInfo>();
	}
	
	/// <summary>
	/// Method used to add a new flag to a certain <see cref="Command"/>.
	/// </summary>
	/// <param name="flagInfo">Flag info.</param>
	public void addFlag(FlagInfo flagInfo) {
		flags.Add(item: flagInfo);
	}

	/// <summary>
	/// Returns a string with simple information.
	/// </summary>
	public override string simpleString () {
		string finalString = base.simpleString();
		for (int i = 0; i < flags.Count; i++) {
			finalString += " " + flags[i].simpleString();
		}
		return finalString;
	}

	/// <summary>
	/// Returns a string with detailed information.
	/// </summary>
	public override string detailedString() {
		string finalString = string.Format(format: "{0}\n\nFlags:\n\n", arg0: base.detailedString());
		for (int i = 0; i < flags.Count; i++) {
			finalString += "\t" + flags[i].detailedString() + "\n";
		}
		return finalString;
	}
}