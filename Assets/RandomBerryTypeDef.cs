using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBerryTypeDef : MonoBehaviour
{
    public GameObject SpaceItemList;
    public GameObject OutPut;


    private void Start()
    {
        int RandomPoint = (int)(Random.Range(2.7f, 4.5f)*10);
        OutPut = Instantiate(SpaceItemList.transform.GetChild(RandomPoint), transform.position, Quaternion.identity, transform).gameObject;
        OutPut.transform.parent = transform.parent;
        Destroy(gameObject);
    }
}
