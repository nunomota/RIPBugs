using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/// <summary>
/// Class used to handle <see cref="Command"/> behaviour.
/// </summary>
public class CommandHandler {
	
	private MessageType commandMessageType;		//the custom MessageType for console prints
	private List<CommandInfo> commandList;		//the list of supported commands (to be iterated)
	private CommandHashTable commandTable;		//hashtable of supportted commands (for quick search)

	/// <summary>
	/// Initializes a new instance of the <see cref="CommandHandler"/> class.
	/// </summary>
	public CommandHandler() {
		this.commandMessageType = new MessageType("[CmdHndl]");
		loadXml("Console/Commands/DataBase");
	}
	
	/// <summary>
	/// Execute the specified <see cref="Command"/>.
	/// </summary>
	/// <returns>
	/// Values bigger or equal to 0 mean everything went well. Otherwhise an error ocurred
	/// </returns>
	/// <param name="command"><see cref="Command"/> to execute.</param>
	public int execute(Command command) {

		/* ------ Return Values -----
		 * -1 > null Command provided
		 * -2 > Command not found
		 -------------------------- */

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
			return -2;
		}
		return 0;
	}

	/// <summary>
	/// Loads the xml into memory.
	/// </summary>
	/// <param name="xmlPath">Xml file path.</param>
	private void loadXml(string xmlPath) {
		MyXmlReader xmlReader = new MyXmlReader(xmlPath);
		commandList = xmlReader.readXmlCommands();
		createHashTable();
	}

	/// <summary>
	/// Creates the hash table needed for <see cref="Command"/> search.
	/// </summary>
	private void createHashTable() {
		//Debug.Log(string.Format ("List has {0} commands", commandList.Count));
		commandTable = new CommandHashTable();
		commandTable.populate(commandList);
	}

	/// <summary>
	/// Prints the <see cref="Command"/> list.
	/// </summary>
	private void printCommandList() {
		CommandInfo helpInfo = commandTable.get("help");
		RIPBugs.console.writeLine(string.Format("help:\n{0}\n", helpInfo.getDescription()), commandMessageType, 0);
		foreach(CommandInfo commandInfo in commandList) {
			if (commandInfo.getName() != "help") {
				RIPBugs.console.writeLine(commandInfo.simpleString(), commandMessageType, 0);
			}
		}
	}

	/// <summary>
	/// Prints the <see cref="Command"/> info.
	/// </summary>
	/// <param name="commandName">Command name.</param>
	private void printCommandInfo(string commandName) {
		CommandInfo commandInfo = commandTable.get(commandName);
		if (commandInfo != null) {
			RIPBugs.console.writeLine(commandInfo.detailedString());
		} else {
			commandNotFoundPrint(commandName);
		}
	}

	/// <summary>
	/// Simmple print.
	/// </summary>
	/// <param name="commandName">Command's name.</param>
	private void commandNotFoundPrint(string commandName) {
		RIPBugs.console.writeLine(string.Format("{0}: command not found. Type 'help' to get a list of existing commands", commandName));
	}
}
