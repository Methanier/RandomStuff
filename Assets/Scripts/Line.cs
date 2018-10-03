using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private Vector3 _p0;
    [SerializeField] private Vector3 _p1;

    public Vector3 PointZero { get { return _p0; } set { _p0 = value; } }
    public Vector3 PointOne { get { return _p1; } set { _p1 = value; } }

    private GameObject _test;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
