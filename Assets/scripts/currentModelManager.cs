using System;
using UnityEngine;

//Скрипт для выбора настройки нужных способностей модели
public class currentModelManager : MonoBehaviour
{
    //Список всех способностей, которые можно выбрать для модели
    public GameObject model;
    [SerializeReference, SubclassSelector] public Ifeature[] CurrentFeatureList = Array.Empty<Ifeature>();
    private void Awake()
    {
        for (int i = 0; i < CurrentFeatureList.Length; i++)
        {
            CurrentFeatureList[i].Model = model;
        }
    }
}
