using System;
using System.Collections.Generic;
//словарь, который хранит нужные презенторы для каждой способности
public static class FeaturePresentorDictionary
{
    public static Dictionary<Type, Type> featurePresenterDict = new()
    {   //Ключ - тип самой способности, значение - тип нужного презентора
        {typeof(cutFeature), typeof(CutFeaturePresenter)},
        {typeof(decomposeFeature), typeof(DecomposeFeaturePresenter)},
        {typeof(colorChange), typeof(ColorFeaturePresentor)},
    };    
}
