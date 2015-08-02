using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class CommandHandler {

	private TextAsset commandDB;				//the XML that will be read
	private MessageType commandMessageType;		//the custom MessageType for console prints
	private List<CommandInfo> commandList;		//the list of supported commands

	public CommandHandler() {
		this.commandDB = Resources.Load ("Console/Commands/DataBase") as TextAsset;
		this.commandMessageType = new MessageType("[CmdHndl]");
		this.commandList = new List<CommandInfo>();

		readXml();
	}

	//values below 0 mean an error ocurred
	public int execute(Command command) {
		if (command == null) {
			return -1;
		}
		switch (command.getName()) {
		case "help":
			string[] options = command.getOptions();
			if (options.Length == 0) {
				//TODO print the entire list of commands
			} else {
				//TODO print only the command's information
			}
			break;
		default:
			RIPBugs.console.writeLine(string.Format("{0}: command not found. Type 'help' to get a list of existing commands", command.getName()));
			return -1;
		}
		return 0;
	}

	private void readXml() {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(commandDB.text);
		XmlNodeList commandTempList = xmlDoc.GetElementsByTagName("Command");

		//run for each command in the XML file
		foreach(XmlNode command in commandTempList) {
			CommandInfo newCommandInfo = new CommandInfo(command.Attributes["name"].Value);
			XmlNodeList commandSections = command.ChildNodes;
			//run for each section inside the command
			foreach(XmlNode commandInfo in commandSections) {
				switch(commandInfo.Name) {
				case "description":
					newCommandInfo.setDescription(commandInfo.InnerText);
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
	}
}
