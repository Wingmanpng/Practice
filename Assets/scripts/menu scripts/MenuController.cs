using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject modelCardPrefab;
    public GameObject parentToSpawn;
    public allModelsObjects modelsObjects;
    List<modelObject> models;

    private void Start()
    {
        models = modelsObjects.listModelObject;
        //Цикл, спавнящий объекты интерфейса в rect scroll
        for (int i = 0; i < models.Count; i++)
        {
            //Спавн самого интерфейса и заполнение интерфейса
            GameObject modelObject = Instantiate(modelCardPrefab, Vector3.zero, Quaternion.identity, parentToSpawn.transform);
            modelObject.transform.localPosition = new Vector3(0, 0, 0);
            modelObject.transform.GetComponent<modelDisplay>().modelObject = models[i];
            modelObject.transform.GetComponent<modelDisplay>().manager = this;
        }
    }
    public void clickOnModel(modelObject modelObject)
    {
        modelObject.isCurrentObject = true;
    }
}
