using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;

public class VanillaLeagueScript : MonoBehaviour
{
    [SerializeField] private GameObject parentPanel;

    [SerializeField] private GameObject adorablePrefab;

    [SerializeField] private int numberObject;
    
    [SerializeField] private Sprite[] birdArray;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= numberObject; i++)
        {
            int randomNumber = Random.Range(0, 1000);
            int rndNumber = Random.Range(0, birdArray.Length - 1);
            Sprite birdImage = birdArray[rndNumber];
            GameObject instantiate = Instantiate(adorablePrefab,parentPanel.transform);
            for (int j = 0; j < instantiate.transform.childCount; j++)
            {
                if (instantiate.transform.GetChild(j).name == "Number")
                {
                    instantiate.transform.GetChild(j).GetComponent<Text>().text = "" + randomNumber;
                }

                if (instantiate.transform.GetChild(j).name == "OrderNumber")
                {
                    if (i < 10)
                    {
                        instantiate.transform.GetChild(j).GetComponent<Text>().text = "0" + i+".";
                    }
                    else if(i == 1000)
                    {
                        instantiate.transform.GetChild(j).GetComponent<Text>().rectTransform.position = new Vector3(-412,-1,0);
                        instantiate.transform.GetChild(j).GetComponent<Text>().text = "" + i+".";
                    }
                    else
                    {
                        instantiate.transform.GetChild(j).GetComponent<Text>().text = "" + i+".";    
                    }
                }

                if (instantiate.transform.GetChild(j).name == "bgInfoPanel03")
                {
                    Debug.Log(instantiate.transform.childCount);
                    instantiate.transform.GetChild(j).GetChild(0).GetComponent<Image>().sprite = birdImage; 
                }
            }
        }
    }
}
