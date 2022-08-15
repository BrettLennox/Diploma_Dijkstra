using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private GameObject[] _nodes;
    public Node start, end;

    [SerializeField] private List<Node> _result = new List<Node>();
    
    public List<Node> Result
    {
        get => _result;
        private set => _result = value;
    }
    
    private void Awake()
    {
        List<Node> shortestPath = FindShortestPath(start, end);

        Node prevNode = null;
        foreach (Node node in shortestPath)
        {
            if (prevNode != null)
            {
                Debug.DrawLine(node.transform.position + Vector3.up, prevNode.transform.position + Vector3.up, Color.blue, 5f);
            }
            Debug.Log(node.gameObject.name);
            prevNode = node;
        }
    }

    public List<Node> FindShortestPath(Node start, Node end)
    {
        _nodes = GameObject.FindGameObjectsWithTag("Node");
        
        if (AStarAlgorithm(start, end))
        {
            //List<Node> result = new List<Node>();

            Node node = end;
            do
            {
                _result.Insert(0, node);
                node = node.PreviousNode;

            } while (node != null);
            
            
            return _result;
        }

        return null;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool AStarAlgorithm(Node start, Node end)
    {
        List<Node> unexplored = new List<Node>();

        foreach (GameObject obj in _nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n == null) continue;
            n.ResetNode(); //calls ResetNode
            n.SetDirectDistanceToEnd(end.transform.position);
            unexplored.Add(n); //adds current loop Node to the unexplored list
        }

        if (!unexplored.Contains(start) || !unexplored.Contains(end))
        {
            return false;
        }
        start.PathWeight = 0;
        while (unexplored.Count > 0)
        {
            //order based on path
            unexplored.Sort((x,y) => x.PathWeightHeuristic.CompareTo(y.PathWeightHeuristic));
            
            //current is the current shortest path possibility
            Node current = unexplored[0];

            if (current == end)
            {
                break;
            }
            
            unexplored.RemoveAt(0);

            foreach (Node neighbourNode in current.NeighbourNodes)
            {
                if(!unexplored.Contains(neighbourNode)) continue;
                ;
                
                float distance = Vector3.Distance(neighbourNode.transform.position, current.transform.position);
                distance = current.PathWeight + distance;

                if (distance < neighbourNode.PathWeight)
                {
                    neighbourNode.PathWeight = distance;
                    neighbourNode.PreviousNode = current;
                }
                
            }
        }
        
        return true;
    }
}
