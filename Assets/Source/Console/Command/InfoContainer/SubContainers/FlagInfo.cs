using UnityEngine;
using System.Collections;

/// <summary>
/// Child class of <see cref="InfoContainer"/>. Used to store a <see cref="Command"/> flag's specifications.
/// </summary>
public class FlagInfo : InfoContainer {

	private bool dependencies;

	/// <summary>
	/// Initializes a new instance of the <see cref="FlagInfo"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="dependencies">Dependencies.</param>
	public FlagInfo(string name, string dependencies = "null") : base(name) {
		this.dependencies = (dependencies == "null") ? false: true;
	}

	/// <summary>
	/// Returns a string with simple information.
	/// </summary>
	public override string simpleString () {
		return string.Format(format: "{0}{1}", arg0: base.simpleString(), arg1: (this.dependencies)? "*": "");
	}

	/// <summary>
	/// Returns a string with detailed information.
	/// </summary>
	public override string detailedString () {
		return string.Format(format: "{0}{1}\t\t{2}", arg0: this.name, arg1: (this.dependencies)? "*": "", arg2: this.description);
	}
}
