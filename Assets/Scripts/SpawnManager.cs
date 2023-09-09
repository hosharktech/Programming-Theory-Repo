using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public TMP_Dropdown typeOption;
    public GameObject[] animalPrefab;
    GameObject currentAnimal;
    // Start is called before the first frame update
    void Start()
    {
        currentAnimal = Instantiate(animalPrefab[typeOption.value], transform.position, Quaternion.identity);
        typeOption.onValueChanged.AddListener(delegate { SpawnAnimal(typeOption.value); });
    }

    // Update is called once per frame
    void Update()
    {
        RotateAnimal();
    }

    void SpawnAnimal(int index)
    {
        if(currentAnimal != null)
        {
            Destroy(currentAnimal);
        }
        currentAnimal = Instantiate(animalPrefab[index], transform.position, Quaternion.identity);
    }

    void RotateAnimal()
    {
        if (currentAnimal != null)
        {
            currentAnimal.transform.Rotate(0, 0.05f, 0);
        }
    }
}
