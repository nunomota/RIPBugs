using UnityEngine;
using System.Collections;

/// <summary>
/// MTAvlTree node.
/// </summary>
public class MTAvlNode {

	private int weight;
	private MTAvlNode up, left, right;
	private MessageType value;

	/// <summary>
	/// Initializes a new instance of the <see cref="MTAvlNode"/> class.
	/// </summary>
	/// <param name="value">MessageType that will define the node.</param>
	public MTAvlNode(MessageType value) {
		this.weight = 0;
		this.up = null;
		this.left = null;
		this.right = null;
		this.value = value;
	}

	/// <summary>
	/// Compare the specified node1 and node2.
	/// </summary>
	/// <returns>
	/// Ultimately will return the result of string.Compare(tag1, tag2), where tag represents the same as node.getValue().getTag()
	/// </returns>
	/// <param name="node1">First <see cref="MTAvlNode"/>.</param>
	/// <param name="node2">Second <see cref="MTAvlNode"/>.</param>
	public static int Compare(MTAvlNode node1, MTAvlNode node2) {
		return node1.CompareTo(node2);
	}

	/// <summary>
	/// Compares the node itself with a specified one.
	/// </summary>
	/// <returns>
	/// Will return the result of string.Compare(tag1, tag2), where tag represents the same as node.getValue().getTag()
	/// </returns>
	/// <param name="cmpNode">Target <see cref="MTAvlNode"/>.</param>
	public int CompareTo(MTAvlNode cmpNode) {
		return string.Compare(this.getValue().getTag(), cmpNode.getValue().getTag());
	}

	/* -------------------------------
	 * ------ Getters & Setters ------
	 ------------------------------- */
	public void setWeight(int weight) {
		this.weight = weight;
	}

	public void setUp(MTAvlNode up) {
		this.up = up;
	}

	public void setLeft (MTAvlNode left) {
		this.left = left;
	}

	public void setRight (MTAvlNode right) {
		this.right = right;
	}

	public void setValue (MessageType value) {
		this.value = value;
	}

	public int getWeight() {
		return this.weight;
	}

	public MTAvlNode getUp() {
		return this.up;
	}

	public MTAvlNode getLeft() {
		return this.left;
	}

	public MTAvlNode getRight() {
		return this.right;
	}

	public MessageType getValue() {
		return this.value;
	}
}
