using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*

 */
/// <summary>
/// This is not a generic type of HashTable, it was designed simply to store CommandInfo.
/// </summary>
public class CommandHashTable {

	private const int occupationDefault = 50;
	private int occupationFactorPercentage;
	private int size;							//size of the array needed to store the values
	private CommandInfo[] array;				//array behind the HashTable (where the values are stored)
	private static string validCharacters = "0123456789abcdefghijklmnopqrstuvwxyz";

	private MessageType hashTableMessageType;

	/// <summary>
	/// Initializes a new instance of the <see cref="CommandHashTable"/> class.
	/// </summary>
	/// <param name="occupationFactorPercentage">Occupation factor as a percentage.</param>
	public CommandHashTable(int occupationFactorPercentage = occupationDefault) {
		//this.size = calculateAdequateSize(nElements, occupationFactorPercentage);
		//array = new CommandInfo[this.size];

		this.occupationFactorPercentage = occupationFactorPercentage;
		hashTableMessageType = new MessageType("[HashTable]");
	}

	/// <summary>
	/// Populate the HashTable from a specified commandList.
	/// </summary>
	/// <param name="commandList">Command list.</param>
	public void populate(List<CommandInfo> commandList) {
		this.size = calculateAdequateSize(commandList.Count, occupationFactorPercentage);
		this.array = new CommandInfo[this.size];
		foreach(CommandInfo commandInfo in commandList) {
			this.add(commandInfo);
		}
	}
	
	/// <summary>
	/// Add a new element to the HashTable.
	/// </summary>
	/// <param name="commandInfo"><see cref="CommandInfo"/> to be added.</param>
	private void add(CommandInfo commandInfo) {
		int index = hashFunction(commandInfo.getName());
		if (array[index] != null) {
			index = collisionHandler(index);
		}
		array[index] = commandInfo;
	}
	
	/// <summary>
	/// Get the specified <see cref="CommandInfo"/> (linear probing solution).
	/// </summary>
	/// <param name="commandName">Command name.</param>
	public CommandInfo get(string commandName) {
		int index = hashFunction(commandName);
		while (array[index] != null && array[index].getName() != commandName) {
			incIndex(ref index);
		}
		return array[index];
	}
	
	/// <summary>
	/// Calculates and returns the most suitable size for the HashTable, according to the parameters.
	/// </summary>
	/// <returns>The final calculated size.</returns>
	/// <param name="nElements">Number of elements.</param>
	/// <param name="occupationFactorPercentage">Occupation factor as a percentage.</param>
	private int calculateAdequateSize(int nElements, int occupationFactorPercentage) {
		//up to 70 commands with 50% occupation seems about enough
		int[] primeNumbers = new int[]{ 
			2, 3, 5, 7, 11, 13, 17,
			19, 23, 29, 31, 37, 41,
			43, 47, 53, 59, 61, 67,
			71, 73, 79, 83, 89, 97,
			101, 103, 107, 109, 113,
			127, 131, 137, 139, 149};

		//just as a precaution
		if (occupationFactorPercentage <= 0 || occupationFactorPercentage > 100) {
			RIPBugs.console.writeLine(string.Format("Occupation percetage was not within the interval ]0%, 100%]... Defaulting to {0}%", occupationDefault), hashTableMessageType, 1);
			occupationFactorPercentage = occupationDefault;
		}
		int minSizeNeeded = Mathf.CeilToInt((float)nElements * (100.0f/(float)occupationFactorPercentage));
		int adequateSize;
		RIPBugs.console.writeLine(string.Format("Minimum HashTable size for {0} elements, with {1}% of occupation, is {2}", nElements, occupationFactorPercentage, minSizeNeeded), hashTableMessageType, 0);
		//adequateSize becomes the next prime we have greater than the size we need
		if ((adequateSize = nextPrime(minSizeNeeded, primeNumbers)) == -1) {
			//we dont have enough prime numbers, so at least we allocate an odd number of positions 
			adequateSize = (minSizeNeeded % 2 == 0)? minSizeNeeded+1: minSizeNeeded;
		}
		return adequateSize;
	}
	
	/// <summary>
	/// Fetches the prime number exactly above a certain value (if it is in the array).
	/// </summary>
	/// <returns>The prime or -1, if this value cannot be retrieved.</returns>
	/// <param name="minSizeNeeded">Minimum size needed.</param>
	/// <param name="primeNumbers">Prime numbers.</param>
	private int nextPrime(int minSizeNeeded, int[] primeNumbers) {
		if (minSizeNeeded <= primeNumbers[primeNumbers.Length-1]) {
			for (int i = 0; i < primeNumbers.Length; i++) {
				if (minSizeNeeded <= primeNumbers[i]) {
					return primeNumbers[i];
				}
			}
		}
		return -1;
	}

	/// <summary>
	/// Function used to calculate the target index.
	/// </summary>
	/// <remarks>
	/// Works in the same way as binary -> decimal conversion but with a base equal to the number of valid characters and not 2
	/// </remarks>
	/// <returns>The function.</returns>
	/// <param name="commandName">Command name.</param>
	private int hashFunction(string commandName) {
		double sum = 0;
		int charIndex;
		//TODO limit the number of characters that can be analized, as a double cant go to infinity...
		for (int i = 0; i < commandName.Length; i++) {
			charIndex = getCharIndex(commandName[i]);
			if (charIndex == -1) {
				RIPBugs.console.writeLine(string.Format("{0} is not a valid character", commandName[i]), hashTableMessageType, 1);
				return -1;
			}
			sum += ((double)charIndex)*Mathf.Pow(validCharacters.Length, i);
		}
		return (int)(sum % size);
	}

	/// <summary>
	/// Gets the index of the char in the specified alphabet.
	/// </summary>
	/// <returns>The char index.</returns>
	/// <param name="character">Character.</param>
	private int getCharIndex(char character) {
		return validCharacters.IndexOf(character);
	}
	
	/// <summary>
	/// Collisions handler, with simple linear probing.
	/// </summary>
	/// <returns>The index calculated.</returns>
	/// <param name="index">Index of collision.</param>
	private int collisionHandler(int index) {
		int curIndex = index;
		while (array[curIndex] != null) {
			incIndex(ref curIndex);
		}
		return curIndex;
	}
	
	/// <summary>
	/// Increments an index and loops it inside the array.
	/// </summary>
	/// <param name="index">Index.</param>
	private void incIndex(ref int index) {
		index = (index+1 < this.size)? index+1: 0;
	}
}
