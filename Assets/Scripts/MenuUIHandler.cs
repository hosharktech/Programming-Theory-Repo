using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    
    private string m_animalName;
    public string animalName
    {
        get { return m_animalName; }
        set { m_animalName = value; }
    }

   
    private float m_speed;
    public float speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }

   
    private float m_power;
    public float power
    {
        get { return m_power; }
        set { m_power = value; }
    }

    private int m_animalType;
    public int animalType
    {
        get { return m_animalType; }
        set { m_animalType = value; }
    }

    public TMP_InputField nameInput;
    public Slider speedSlider;
    public Slider powerSlider;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI animalTypeText;
    public GameObject warningText;
    public TMP_Dropdown typeOption;

    // Update is called once per frame
    void Update()
    {
        animalTypeText.text = typeOption.options[typeOption.value].text;
        SliderUpdate();
    }

    void SliderUpdate()
    {
        speedText.text = "" + Mathf.Round(speedSlider.value);
        powerText.text = "" + Mathf.Round(powerSlider.value);
    }

    
    public void StartNew()
    {
        m_animalName = nameInput.text;
        m_speed = speedSlider.value;
        m_power = powerSlider.value;
        m_animalType = typeOption.value;

        if (!string.IsNullOrEmpty(nameInput.text))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            warningText.SetActive(true);
            warningText.GetComponent<TextMeshProUGUI>().text = "You must enter the name!";
        }
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Previous()
    {
        if(typeOption.value > 0)
        {
            typeOption.value--;
        }
        else
        {
            typeOption.value = typeOption.options.Count;
        }
    }
    public void Next()
    {
        if(typeOption.value < (typeOption.options.Count -1))
        {
            typeOption.value++;
        }
        else
        {
            typeOption.value = 0;
        }
        
    }

    [System.Serializable]
    public class SaveData
    {
        public string saveAnimalName;
        public float saveSpeed;
        public float savePower;
        public int saveType;
    }
    public void Save()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            SaveData data = new SaveData();
            data.saveAnimalName = nameInput.text;
            data.saveSpeed = speedSlider.value;
            data.savePower = powerSlider.value;
            data.saveType = typeOption.value;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            warningText.SetActive(false);
            Debug.Log("Save!");
        }
        else
        {
            warningText.SetActive(true);
            warningText.GetComponent<TextMeshProUGUI>().text = "You must enter the name!";
        }
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if( File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            nameInput.text = data.saveAnimalName;
            speedSlider.value = data.saveSpeed;
            powerSlider.value = data.savePower;
            typeOption.value = data.saveType;
            warningText.SetActive(false);
            Debug.Log("Load!");
        }
        else
        {
            warningText.SetActive(true);
            warningText.GetComponent<TextMeshProUGUI>().text = "You have not saved!";
        } 
    }
}
