using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GetImage : MonoBehaviour
{    
    private string filePath = @"C:\Users\GAROTOS\Downloads\b.png";
    private float newScale = 1;

    void Start()
    {
        GetTheImage();
    }

    void Update()
    {
        
    }

    public void GetTheImage(){
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(filePath)){
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            // this.GetComponent<RectTransform>().sizeDelta = new Vector2(tex.width, tex.height);
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(tex.height, tex.height);

            float hScale = ((float)Screen.height/(float)tex.height);
            // print(hScale);
            // float wScale = ((float)Screen.width/(float)tex.width);
            // print(wScale);
            // newScale = (hScale < wScale) ? hScale : wScale;

            // this.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1);
            this.GetComponent<RectTransform>().localScale = new Vector3(hScale, hScale, 1);
            print(Screen.height + "  " + tex.height);
            print(Screen.width + "  " + tex.width);
        }

        Texture2D photo = new Texture2D(tex.height, tex.height);
        photo.SetPixels(tex.GetPixels((tex.width - tex.height) / 2, 0, tex.height, tex.height, 0));
        photo.Apply();

        this.GetComponent<RawImage>().texture = photo;
    }
}
