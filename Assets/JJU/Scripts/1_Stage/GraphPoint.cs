using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Graph 테스트
public class GraphPoint : MonoBehaviour
{
    public GameObject[] vertexs;
    public Graph<Point> PathGraph;
    void Start()
    {
        PathGraph = new Graph<Point>();


        vertexs = GameObject.FindGameObjectsWithTag("Point");


        foreach(GameObject game in vertexs)
        {            
            GraphNode<Point> point = game.GetComponent<Point>().node;
            PathGraph.AddNode(point);

            foreach(var edge in game.GetComponent<Point>().egdes)
            {
                PathGraph.AddEdge(point, edge.GetComponent<Point>().node);
            }
        }
        PathGraph.DebugPrintLinks();
    }
}


// GraphNode 클래스
public class GraphNode<T>
{
    public List<GraphNode<T>> _neighbors;
    public List<int> _weights;

    public T Data { get; set; }

    public GraphNode()
    {

    }

    public GraphNode(T value)
    {
        this.Data = value;
    }

    public List<GraphNode<T>> Neighbors
    {
        get
        {
            _neighbors = _neighbors ?? new List<GraphNode<T>>();
            return _neighbors;
        }        
        
    }

    public List<int> Weights
    {
        get
        {
            _weights = _weights ?? new List<int>();
            return _weights;
        }
    }
}


// Graph 클래스
public class Graph<T>
{
    private List<GraphNode<T>> _nodeList;

    public Graph()
    {
        _nodeList = new List<GraphNode<T>>();
    }


    /// <summary>
    /// 단일 노드 넣을때
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public GraphNode<T> AddNode(T data)
    {
        GraphNode<T> n = new GraphNode<T>(data);
        _nodeList.Add(n);
        return n;
    }

    /// <summary>
    /// 리스트로 된 노드 넣을때
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public GraphNode<T> AddNode(GraphNode<T> node)
    {
        _nodeList.Add(node);
        return node;
    }


    /// <summary>
    /// 정점 연결시키기
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="oneway"></param>
    /// <param name="weight"></param>
    public void AddEdge(GraphNode<T> from, GraphNode<T> to, bool oneway = true, int weight = 0)
    {
        from.Neighbors.Add(to);
        from.Weights.Add(weight);

        if (!oneway)
        {
            to.Neighbors.Add(from);
            to.Weights.Add(weight);
        }
    }

    public void DebugPrintLinks()
    {
        foreach (GraphNode<T> graphNode in _nodeList)
        {
            foreach (var n in graphNode.Neighbors)
            {
                string s = graphNode.Data + " - " + n.Data;
                Debug.Log(s);
            }
        }
    }
}