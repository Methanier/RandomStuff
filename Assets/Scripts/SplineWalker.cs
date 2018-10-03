using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    [SerializeField] private BezierSpline _spline;
    [SerializeField] private float _duration;

    private float _progress;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _progress += Time.deltaTime / _duration;
        if(_progress > 1f)
        {
            _progress = 1f;
        }
        transform.localPosition = _spline.GetPoint(_progress);
	}
}
