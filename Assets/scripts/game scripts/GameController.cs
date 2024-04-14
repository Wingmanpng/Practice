using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public Transform modelParent;
    public allModelsObjects ModelsObject;
    public GameObject changeableObject;

    public event ModelHandler OnModelChanged ;

    modelObject currentModelObject;   
    List<modelObject> allModelsObject;
    int currentModelId;

    private void Start()
    {
        allModelsObject = ModelsObject.listModelObject;
        for (int i = 0; i < allModelsObject.Count; i++)
        {
            if (allModelsObject[i].isCurrentObject)
            {
                currentModelId = i;
                allModelsObject[i].isCurrentObject = false;
                break;
            }
        }
        ChangeCurrentModel(currentModelId);
    }

    //Функция для создания модели
    public void SpawnModel()
    {
        Destroy(changeableObject);
        GameObject spawnedObject = Instantiate(currentModelObject.model, Vector3.zero, Quaternion.identity, modelParent);
        spawnedObject.transform.localPosition = new Vector3(0, 0, 0);
        changeableObject = spawnedObject;
    }

    public void NextModel()
    {
        currentModelId++;
        ChangeCurrentModel(currentModelId);
    }

    public void PrevModel()
    {
        currentModelId--;
        ChangeCurrentModel(currentModelId);
    }
    //Проверка переданного в функцию id для смены текущей модели на принадлежность к области массива моделей
    private void ChangeCurrentModel(int currentId)
    {
        if (currentId > allModelsObject.Count - 1)
        {
            currentId = 0;
        }
        if (currentId < 0)
        {
            currentId = allModelsObject.Count - 1;
        }
        
        currentModelObject = allModelsObject[currentId];
        currentModelId = currentId;
        OnModelChanged?.Invoke(currentModelObject);
        SpawnModel();
    }
}
public delegate void ModelHandler(modelObject currentModelObject);