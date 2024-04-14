using UnityEngine;

public class ColorFeaturePresentor : FeaturePresentor
{
    public CUIColorPicker ColorPicker;
    colorChange colorFeature;
    public override void CurrentFeatureUIPresent(Ifeature feature)
    {
        colorFeature = (colorChange)feature;
        ColorPicker.applyColor.onClick.AddListener(ChangeColor);
    }
    private void ChangeColor()
    {
        colorFeature.colorToChange = ColorPicker.Color;
        colorFeature.FeatureRealization();
    }
}
