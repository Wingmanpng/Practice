using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New list model Object", menuName = "List Model Object")]

//ScriptableObject для хранения всех и выбранной пользователем моделей
public class allModelsObjects : ScriptableObject
{
    public List<modelObject> listModelObject;
}
