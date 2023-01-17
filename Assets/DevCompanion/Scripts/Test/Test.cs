using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tets : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        Debug.Log("Requested...");
        var str = await DevCompanion.GPTApi.MakeRequest("write a hello world program in c++");
        Debug.Log(str);
    }
}
