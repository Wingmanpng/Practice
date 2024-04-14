using System;
using UnityEngine;
using UnityEngine.UI;

public class DecomposeFeaturePresenter : FeaturePresentor
{
    public Slider slider;
    decomposeFeature decFeature;

    public override void CurrentFeatureUIPresent(Ifeature feature)
    {
        decFeature = (decomposeFeature)feature;
        slider.minValue = decFeature.minDistance;
        slider.maxValue = decFeature.maxDistance;
        for (int i = 0; i < feature.Model.transform.childCount; i++)
        {
            decFeature.startPos.Add(feature.Model.transform.GetChild(i).localPosition); 
        }
        slider.onValueChanged.AddListener(ChangeDecomposition);
    }

    private void ChangeDecomposition(float value)
    {
        decFeature.currentValue = value;
        decFeature.FeatureRealization();
    }
}
