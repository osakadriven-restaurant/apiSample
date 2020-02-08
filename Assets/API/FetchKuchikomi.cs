using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FetchKuchikomi : MonoBehaviour
{

    public static List<string> Comments;
    public static bool HasSet = false;


    // Start is called before the first frame update
    public void Query(string shopID)
    {
        StartCoroutine(GETRequest($"https://api.gnavi.co.jp/PhotoSearchAPI/v3/?keyid=8446b8f3a55150243fe036fa0fa7b8d3&shop_id={shopID}"));
    }

    IEnumerator GETRequest(string url)
    {
        var request = new UnityWebRequest(url, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        string response = request.downloadHandler.text;
        Comments = PurseResponse(response);
        FetchKuchikomi.HasSet = true;
    }

    private List<string> PurseResponse(string response)
    {
        List<string> comment = new List<string>();
        foreach(var line in response.Split('\n'))
        {
            if (line.Contains("\"comment\": "))
            {
                string a1 = line.Trim();
                a1 = a1.Replace("\"comment\": ", "");
                a1 = a1.Replace(",", "");
                a1 = a1.Replace("\"", "");
                comment.Add(a1);
            }
        }
        return comment;
    }
}
