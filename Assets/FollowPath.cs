using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    private int _moveIndex = 0;
    private float offset = 0.3f;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Node node in GameObject.Find("Pathfinding").GetComponent<AStar>().Result)
        {
            _waypoints.Add(node.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) > offset)
        {
            dir = _waypoints[_moveIndex].position - transform.position;
            dir.Normalize();
            transform.Translate((dir * _moveSpeed) * Time.deltaTime);
        }
        

        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) <= offset && _moveIndex < _waypoints.Count - 1)
        {
            _moveIndex++;
        }
    }
}
