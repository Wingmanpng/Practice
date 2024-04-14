public class CutFeaturePresenter : FeaturePresentor
{
    cutFeature cutFeature;
    public override void CurrentFeatureUIPresent(Ifeature feature)
    {
        cutFeature = (cutFeature)feature;
        cutFeature.FeatureRealization();
    }
}
