using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandInfo : InfoContainer {

	private List<FlagInfo> flags;

	public CommandInfo(string name) : base(name) {
		this.flags = new List<FlagInfo>();
	}

	//method used to add a new flag to a certain command
	public void addFlag(FlagInfo flagInfo) {
		flags.Add(flagInfo);
	}

	public override string ToString() {
		string finalString = string.Format("{0}\n\nFlags:\n\n", base.ToString());
		for (int i = 0; i < flags.Count; i++) {
			finalString += flags[i].ToString() + "\n";
		}
		return finalString;
	}
}