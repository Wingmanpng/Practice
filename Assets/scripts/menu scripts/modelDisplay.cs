using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Скрипт для отображения информации о модели в интерфейс
public class modelDisplay : MonoBehaviour
{
    public MenuController manager;
    public modelObject modelObject;
    public Text modelName;
    public Image modelImage;

    private void Start()
    {
        modelName.text = modelObject.modelName;
        modelImage.sprite = modelObject.modelImage;
        GetComponent<Button>().onClick.AddListener(delegate {
            manager.clickOnModel(modelObject);
            SceneManager.LoadScene(1);
        });       
    }
}
