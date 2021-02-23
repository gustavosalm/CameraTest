using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    // Start is called before the first frame update
    void Start()
    {
        var width = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
        print(Screen.width);
        print(width);
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(width, Camera.main.orthographicSize * 2.0f);
        
        //transform.localScale = new Vector3(width, width);
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        print(Screen.width + "  " + Screen.height);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        GetComponent<RawImage>().texture = webCamTexture;
    }
}
