using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/// <summary>
/// My xml reader. Class used to read all the Commands in a XML file and load the into memory.
/// </summary>
public class MyXmlReader {

	private TextAsset textDB;
	private List<CommandInfo> commandList;

	/// <summary>
	/// Initializes a new instance of the <see cref="MyXmlReader"/> class.
	/// </summary>
	/// <param name="xmlPath">Xml file path.</param>
	public MyXmlReader(string xmlPath) {
		this.textDB = Resources.Load(path: xmlPath) as TextAsset;
		this.commandList = new List<CommandInfo>();
	}

	/// <summary>
	/// Reads the xml commands.
	/// </summary>
	/// <returns>The xml commands as List<Commands>.</returns>
	public List<CommandInfo> readXmlCommands() {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xml: textDB.text);
		XmlNodeList commandTempList = xmlDoc.GetElementsByTagName(name: "Command");
		
		//run for each command in the XML file
		foreach(XmlNode command in commandTempList) {
			CommandInfo newCommandInfo = new CommandInfo(name: command.Attributes["name"].Value);
			XmlNodeList commandSections = command.ChildNodes;
			//run for each section inside the command
			foreach(XmlNode commandInfo in commandSections) {
				switch(commandInfo.Name) {
				case "description":
					string rawDescription = commandInfo.InnerText;
					rawDescription = rawDescription.Trim(trimChars: new char[]{'\n', '\r', '\t', ' '});
					//rawDescription.Replace(' ', '*');
					//rawDescription.Replace('\t', '*');
					newCommandInfo.setDescription(description: rawDescription);
					break;
				case "flags":
					//run for each one of the flags associated with the command
					XmlNodeList flags = commandInfo.ChildNodes;
					foreach(XmlNode flag in flags) {
						FlagInfo newFlagInfo = new FlagInfo(name: flag.Attributes["name"].Value);
						newFlagInfo.setDescription(description: flag.InnerText);
						newCommandInfo.addFlag(flagInfo: newFlagInfo);
					}
					break;
				default:
					break;
				}
			}
			commandList.Add(item: newCommandInfo);
		}
		return commandList;
	}
}
