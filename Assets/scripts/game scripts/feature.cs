using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Абстракция будущего вида различных способностей
public interface Ifeature
{
    string Name { get; }
    public GameObject Model { get; set; }
    //Функция, которая принимает аргументом саму модель, которую надо будет изменять
    public void FeatureRealization();
}
