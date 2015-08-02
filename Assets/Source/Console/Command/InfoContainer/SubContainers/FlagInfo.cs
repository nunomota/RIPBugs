using UnityEngine;
using System.Collections;

public class FlagInfo : InfoContainer {
	
	public FlagInfo(string name) : base(name) {

	}

	public override string ToString () {
		return string.Format("{0}\t\t{1}", this.name, this.description);
	}
}
