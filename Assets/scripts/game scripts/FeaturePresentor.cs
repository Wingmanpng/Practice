using UnityEngine;

//Абстракция для презенторов способностей
public abstract class FeaturePresentor:MonoBehaviour
{
    protected GameController _gameController;
    public void Init(GameController gameController)
    {
        _gameController = gameController;
    }
    public abstract void CurrentFeatureUIPresent(Ifeature feature);
}
