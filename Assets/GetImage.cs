using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GetImage : MonoBehaviour
{    
    private string filePath = @"C:\Users\GAROTOS\Downloads\photo4940890537658001801.jpg";

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
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        this.GetComponent<RawImage>().texture = tex;
    }
}
