using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public static class EXDOBezier
{
    public static Tweener DOBezier(this Transform transform, Vector3 startPos,Vector3 controlPos,Vector3 endPOs, float time,Action callback = null)
    {
       if(transform == null) {
            return null;
        }

        DOTween.Kill(transform);
        Vector3[] path = BezierPath(startPos, controlPos, endPOs);
        return transform.DOPath(path, time).OnComplete(() => {
            if (callback != null) callback();
        }).SetEase(Ease.InQuad);
    }
   
    private static Vector3[] BezierPath(Vector3 startPos,Vector3 controlPos,Vector3 endPos)
    {
        Vector3[] path = new Vector3[10];
        for (int i = 1; i <= 10; i++) {
            float t = i / 10.0f;
            path[i - 1] = Bezier(startPos, controlPos, endPos, t);
        }
        return path;
    }

    private static Vector3 Bezier(Vector3 startPos,Vector3 controPos,Vector3 endPos,float t)
    {
        return (1 - t) * (1 - t) * startPos + 2 * t * (1 - t) * controPos + t * t * endPos;
    }
}
