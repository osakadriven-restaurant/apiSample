using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 destination = new Vector3(2,0,2);

    public PutStoreIcon storeIcon;
    public GameObject Human;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SetDestination("甘太郎 茶屋町店"));
    }

    IEnumerator SetDestination(string storeName)
    {
        while (PutStoreIcon.HasPut != true)
        {
            yield return new WaitForSeconds(0.2f);
        }
        destination = storeIcon.GetPositionOf(storeName);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(destination);
    }
}
