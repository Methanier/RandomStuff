﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

    private BezierCurve _curve;
    private Transform _handleTransform;
    private Quaternion _handleRotation;

    private const int lineSteps = 10;

    private const float directionScale = 0.5f;

    private void OnSceneGUI()
    {
        _curve = target as BezierCurve;
        _handleTransform = _curve.transform;
        _handleRotation = Tools.pivotRotation == PivotRotation.Local ? _handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);
        Vector3 p3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p2, p3);

        ShowDirections();
        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);

        /*
        Handles.color = Color.green;
        Vector3 lineStart = _curve.GetPoint(0f);
        Handles.DrawLine(lineStart, lineStart + _curve.GetDirection(0f));
        for(int i = 1; i <= lineSteps; i++)
        {
            Vector3 lineEnd = _curve.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + _curve.GetDirection(i / (float)lineSteps));
            lineStart = lineEnd;
        }*/
    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = _handleTransform.TransformPoint(_curve.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, _handleRotation);
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_curve, "Move Point");
            EditorUtility.SetDirty(_curve);
            _curve.points[index] = _handleTransform.InverseTransformPoint(point);
        }

        return point;
    }

    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = _curve.GetPoint(0f);
        Handles.DrawLine(point, point + _curve.GetDirection(0f) * directionScale);
        for (int i = 1; i <= lineSteps; i++)
        {
            point = _curve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + _curve.GetDirection(i / (float)lineSteps) * directionScale);
        }
    }
}
