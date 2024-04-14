using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//Способность декомпозиции объектов
public class decomposeFeature : Ifeature
{
    public int maxDistance;
    public int minDistance=1;
    public float currentValue;
    public List<Vector3> startPos;
    public GameObject Model { get; set; }

    [SerializeField] List<GameObject> partsToDecompose;
    readonly string Name = "Декомпозиция";
    string Ifeature.Name => Name;

    public void FeatureRealization()
    { 
        for (int i = 0; i < partsToDecompose.Count; i++)
        {
            Vector3 childPos = partsToDecompose[i].transform.localPosition;
            childPos.x = objectMove(startPos[i].x, currentValue);
            childPos.y = objectMove(startPos[i].y, currentValue);
            childPos.z = objectMove(startPos[i].z, currentValue);
            partsToDecompose[i].transform.localPosition = childPos;
        }
        Debug.Log("Декомпозиция");
    }     
    float objectMove(float coord, float value)
    {
        if (Math.Abs(coord * value) > Math.Abs(coord * maxDistance))
        {
            return coord * maxDistance;
        }
        if (Math.Abs(coord * value) < Math.Abs(coord * minDistance))
        {
            return coord * minDistance;
        }
        return coord*value;
    }

}
