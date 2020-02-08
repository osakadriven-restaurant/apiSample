using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutStoreIcon : MonoBehaviour
{
    /// <summary>
    /// 初期値の緯度
    /// </summary>
    public static readonly double BaseLatitude = 34.708435;


    /// <summary>
    /// 初期値の経度
    /// </summary>
    public static readonly double BaseLongitude = 135.499802;


    public GameObject icon;
    public GurunaviFetch fetch;
    public static bool HasPut = false;

    Dictionary<string, Vector3> positions = new Dictionary<string, Vector3>();

    private void Start()
    {
        PutIcon();
        fetch.Query(BaseLatitude, BaseLongitude);
        StartCoroutine(PutIcon());
    }

    IEnumerator PutIcon()
    {
        while(GurunaviFetch.HasSet != true)
        {
            yield return new WaitForSeconds(0.2f);
        }
        foreach(var storedata in GurunaviFetch.Gurunavis)
        {
            float pos_x = (float)(storedata.Latitude - BaseLatitude) * 111000f;
            float pos_z = (float)(storedata.Longitude - BaseLongitude) * 111000f;

            Instantiate(icon, new Vector3(pos_x, 0, pos_z), Quaternion.identity);
            positions.Add(storedata.Name, new Vector3(pos_x, 0, pos_z));
        }
        HasPut = true;
    }

    public Vector3 GetPositionOf(string StoreName)
    {
        foreach (KeyValuePair<string, Vector3> kvp in positions)
        {
            if(kvp.Key == StoreName)
            {
                return kvp.Value;
            }
        }
        Debug.Log("見つからなかった");
        return Vector3.zero;
    }
}
