using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    [SerializeField] private GameObject parentContainer;

    [SerializeField] private int quantitiesContainer;
    
    public GameObject containerPrefab;

    [SerializeField] private Sprite[] birdArray;

    [SerializeField] private string[] nameArray;
    
    [SerializeField] private VerticalLayoutGroup layout;
    
    void Start()
    {
        SetUserData();
    }

    private void SetUserData()
    {
        for (int i = 1; i <= quantitiesContainer; i++)
        {
            int randomNumber = Random.Range(0, 1000);
            
            int rndNumber = Random.Range(0, birdArray.Length - 1);
            Sprite birdImage = birdArray[rndNumber];

            int rndNum = Random.Range(0, nameArray.Length - 1);
            string name = nameArray[rndNum];
            var user = new User("Adorable " + name, (i < 10) ? "0" + i : "" + i, birdImage, randomNumber);
            GameObject instantiate = Instantiate(containerPrefab,layout.transform);
            instantiate.GetComponent<ShowUserInfo>().SetUI(user);
        }
    }
}

