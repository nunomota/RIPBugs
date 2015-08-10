using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class CommandHandler {
	
	private MessageType commandMessageType;		//the custom MessageType for console prints
	private List<CommandInfo> commandList;		//the list of supported commands (to be iterated)
	private CommandHashTable commandTable;		//hashtable of supportted commands (for quick search)

	public CommandHandler() {
		this.commandMessageType = new MessageType("[CmdHndl]");
		loadXml("Console/Commands/DataBase");
	}

	//values below 0 mean an error ocurred
	public int execute(Command command) {
		if (command == null) {
			return -1;
		}
		string[] options = command.getOptions();
		switch (command.getName()) {
		case "help":
			if (options.Length == 0) {
				printCommandList();
			} else {
				printCommandInfo(options[0]);
			}
			break;
		case "filter":
			if (options.Length < 2) {
				//TODO some warning about wrong command usage
				//maybe count dependencies and compare to that value...?
			} else {
				MessageFilter.toogle(options[0], (options[1] == "on")? true: false);
			}
			break;
		default:
			commandNotFoundPrint(command.getName());
			return -1;
		}
		return 0;
	}

	private void loadXml(string xmlPath) {
		MyXmlReader xmlReader = new MyXmlReader(xmlPath);
		commandList = xmlReader.readXmlCommands();
		createHashTable();
	}

	private void createHashTable() {
		//Debug.Log(string.Format ("List has {0} commands", commandList.Count));
		commandTable = new CommandHashTable();
		commandTable.populate(commandList);
	}

	private void printCommandList() {
		CommandInfo helpInfo = commandTable.get("help");
		RIPBugs.console.writeLine(string.Format("help:\n{0}\n", helpInfo.getDescription()), commandMessageType, 0);
		foreach(CommandInfo commandInfo in commandList) {
			if (commandInfo.getName() != "help") {
				RIPBugs.console.writeLine(commandInfo.simpleString(), commandMessageType, 0);
			}
		}
	}

	private void printCommandInfo(string commandName) {
		CommandInfo commandInfo = commandTable.get(commandName);
		if (commandInfo != null) {
			RIPBugs.console.writeLine(commandInfo.detailedString());
		} else {
			commandNotFoundPrint(commandName);
		}
	}

	private void commandNotFoundPrint(string commandName) {
		RIPBugs.console.writeLine(string.Format("{0}: command not found. Type 'help' to get a list of existing commands", commandName));
	}
}
