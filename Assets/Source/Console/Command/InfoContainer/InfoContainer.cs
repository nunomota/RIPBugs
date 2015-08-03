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

	public string getName() {
		return this.name;
	}

	public string getDescription() {
		return this.description;
	}

	public virtual string simpleString () {
		return this.name;
	}

	public virtual string detailedString() {
		return string.Format ("{0}:\n{1}", this.name, this.description);
	}
}
