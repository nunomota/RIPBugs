using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class MyXmlReader {

	private TextAsset textDB;
	private List<CommandInfo> commandList;

	public MyXmlReader(string xmlPath) {
		this.textDB = Resources.Load(xmlPath) as TextAsset;
		this.commandList = new List<CommandInfo>();
	}

	public List<CommandInfo> readXmlCommands() {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(textDB.text);
		XmlNodeList commandTempList = xmlDoc.GetElementsByTagName("Command");
		
		//run for each command in the XML file
		foreach(XmlNode command in commandTempList) {
			CommandInfo newCommandInfo = new CommandInfo(command.Attributes["name"].Value);
			XmlNodeList commandSections = command.ChildNodes;
			//run for each section inside the command
			foreach(XmlNode commandInfo in commandSections) {
				switch(commandInfo.Name) {
				case "description":
					string rawDescription = commandInfo.InnerText;
					rawDescription = rawDescription.Trim('\n', '\r', '\t', ' ');
					//rawDescription.Replace(' ', '*');
					//rawDescription.Replace('\t', '*');
					newCommandInfo.setDescription(rawDescription);
					break;
				case "flags":
					//run for each one of the flags associated with the command
					XmlNodeList flags = commandInfo.ChildNodes;
					foreach(XmlNode flag in flags) {
						FlagInfo newFlagInfo = new FlagInfo(flag.Attributes["name"].Value);
						newFlagInfo.setDescription(flag.InnerText);
						newCommandInfo.addFlag(newFlagInfo);
					}
					break;
				default:
					break;
				}
			}
			commandList.Add(newCommandInfo);
		}
		return commandList;
	}
}
