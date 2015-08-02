using UnityEngine;
using System.Collections;

public class InfoContainer {

	protected string name;
	protected string description;

	public InfoContainer(string name) {
		this.name = name;
	}

	public void setDescription(string description) {
		this.description = description;
	}

	public override string ToString () {
		return string.Format ("{0}:\n{1}", this.name, this.description);
	}
}
