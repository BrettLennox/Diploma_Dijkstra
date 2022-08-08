using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    /// <summary>
    /// total cost of shortest path to this node
    /// </summary>
    private float _pathWeight = int.MaxValue;
    public float PathWeight { get => _pathWeight; set => _pathWeight = value; }
    /// <summary>
    /// following the shortest path, previousNode is the previous step on that path
    /// </summary>
    private Node _previousNode = null;
    public Node PreviousNode { get => _previousNode; set => _previousNode = value; }
    /// <summary>
    /// Nodes this node is connected to
    /// </summary>
    [SerializeField] private List<Node> _neighbourNodes;
    public List<Node> NeighbourNodes { get => _neighbourNodes; }

    private void Start()
    {
        ResetNode();
        ValidateNeighbours();
    }

    public void ResetNode()
    {
        PathWeight = int.MaxValue;
        PreviousNode = null;
    }

    public void AddNeighbourNode(Node node)
    {
        NeighbourNodes.Add(node);
    }

    private void OnDrawGizmos()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, node.transform.position);
            
        }
    }

    private void OnValidate()
    {
        ValidateNeighbours();
    }

    private void ValidateNeighbours()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;
            
            if (!node._neighbourNodes.Contains(this))
            {
                node.AddNeighbourNode(this);
            }
        }
    }
}
