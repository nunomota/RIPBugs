using UnityEngine;
using System.Collections;

public class FlagInfo : InfoContainer {

	private bool dependencies;

	public FlagInfo(string name, bool dependencies = false) : base(name) {

	}

	public override string simpleString () {
		return string.Format("{0}{1}", base.simpleString(), (this.dependencies)? "*": "");
	}

	public override string detailedString () {
		return string.Format("{0}{1}\t\t{2}", this.name, (this.dependencies)? "*": "", this.description);
	}
}
