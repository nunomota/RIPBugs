using UnityEngine;
using System.Collections;

/// <summary>
/// Info container (to be used for <see cref="Command"/> description).
/// </summary>
public class InfoContainer {

	protected string name;
	protected string description;

	/// <summary>
	/// Initializes a new instance of the <see cref="InfoContainer"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	public InfoContainer(string name) {
		this.name = name;
	}

	/// <summary>
	/// Sets the description.
	/// </summary>
	/// <param name="description">Description.</param>
	public void setDescription(string description) {
		this.description = description;
	}

	/// <summary>
	/// Gets the name.
	/// </summary>
	public string getName() {
		return this.name;
	}

	/// <summary>
	/// Gets the description.
	/// </summary>
	public string getDescription() {
		return this.description;
	}

	/// <summary>
	/// Method meant to be used by child classes.
	/// </summary>
	/// <returns>
	/// Simple string containing the name.
	/// </returns>
	public virtual string simpleString () {
		return this.name;
	}

	/// <summary>
	/// Method meant to be used by child classes.
	/// </summary>
	/// <returns>
	/// Detailed string containing both name and description.
	/// </returns>
	public virtual string detailedString() {
		return string.Format ("{0}:\n{1}", this.name, this.description);
	}
}
