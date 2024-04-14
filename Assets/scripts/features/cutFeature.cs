using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//Способность разреза объектов
public class cutFeature : Ifeature
{
    public GameObject Model { get; set; }

    readonly string Name = "Разрез";
    string Ifeature.Name => Name;
    [SerializeField] List<GameObject> partsToCut;

    public void FeatureRealization()
    {
        //Каждая нечетная часть модели просто отключается
        for (int i = 0; i < partsToCut.Count; i++)
        {
            partsToCut[i].gameObject.SetActive(false); 
        }
        Debug.Log("Разрез");
    }
}
