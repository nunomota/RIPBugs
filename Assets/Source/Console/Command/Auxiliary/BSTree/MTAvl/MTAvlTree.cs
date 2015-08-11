using UnityEngine;
using System.Collections;

/// <summary>
/// Class that represents a custom made AVL tree. 
/// Aims to optimize the search of MessageType objects only.
/// </summary>
public class MTAvlTree {

	private MTAvlNode root;

	/// <summary>
	/// Initializes a new instance of the <see cref="MTAvlTree"/> class.
	/// </summary>
	public MTAvlTree () {
		this.root = null;
	}
	
	/// <summary>
	/// Method called from outside the class to add a new MessageType to the tree
	/// </summary>
	/// <param name="value">MessageType to add.</param>
	public void add(MessageType value) {
		MTAvlNode curNode = null;
		MTAvlNode nextNode = root;
		MTAvlNode newNode = new MTAvlNode(value: value);
		int nodeCmp = 0;							//it is irrelevant the initial value set to this variable
		while (nextNode != null) {
			curNode = nextNode;
			if ((nodeCmp = newNode.CompareTo(cmpNode: curNode)) != 0) {
				nextNode = (nodeCmp < 0)? curNode.getLeft(): curNode.getRight();
			} else {
				//another MessageType already exists with the exact same tag... don't add a new one
				//TODO add some kind of warning
				return;
			}
		}
		insertNode(father: curNode,newNode:  newNode, nodeCmp: nodeCmp);
	}

	/// <summary>
	/// Inserts a MTAvlNode as a child of another MTAvlNode
	/// </summary>
	/// <param name="father">Node to be used as a 'father'.</param>
	/// <param name="newNode">Node to be added as a 'child'.</param>
	/// <param name="nodeCmp">Value that indicates the side in which to add the 'child' (right or left).</param>
	private void insertNode(MTAvlNode father, MTAvlNode newNode, int nodeCmp) {
		if (father != null) {
			newNode.setUp(node: father);
			if (nodeCmp < 0) {
				father.setLeft(node: newNode);
			} else {
				father.setRight(node: newNode);
			}
			balanceTree(bottomNode: newNode);
		} else {
			//means there was still no Node inserted in the tree
			this.root = newNode;
		}
	}
	
	/// <summary>
	/// Balances the AVL tree after inserting a node
	/// </summary>
	/// <param name="bottomNode">Bottom node.</param>
	private void balanceTree(MTAvlNode bottomNode) {
		//TODO balance the AVL tree
	}
}
