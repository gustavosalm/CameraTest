using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class GetImage : MonoBehaviour
{    
    private string filePath = "";
    private float newScale = 1;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void GetFromGallery(){
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery(( path ) => {
            Debug.Log( "Image path: " + path );
            if( path != null )
            {
                // Create Texture from selected image
                // Texture2D texture = NativeGallery.LoadImageAtPath( path, Screen.width );
                // if( texture == null ){
                //     Debug.Log( "Couldn't load texture from " + path );
                //     return;
                // }
                GetTheImage(path);
                // Assign texture to a temporary quad and destroy it after 5 seconds                
            }
        }, "Select a PNG image", "image/png" );
	    Debug.Log( "Permission result: " + permission );
    }

    public void GetTheImage(string path){
        Texture2D tex = null;
        filePath = path;
        byte[] fileData;
        this.transform.forward = Camera.main.transform.forward;
        if (File.Exists(filePath)){
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            this.transform.forward = Camera.main.transform.forward;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(tex.width, tex.height);
            // this.GetComponent<RectTransform>().sizeDelta = new Vector2(tex.height, tex.height);
            float hScale = ((float)Screen.height/(float)tex.height);
            // print(hScale);
            float wScale = ((float)Screen.width/(float)tex.width);
            // print(wScale);
            newScale = (hScale < wScale) ? hScale : wScale;
            this.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1);
            // this.GetComponent<RectTransform>().localScale = new Vector3(hScale, hScale, 1);
            // print(Screen.height + "  " + tex.height);
            // print(Screen.width + "  " + tex.width);
        }
        print(Convert.ToBase64String(tex.EncodeToJPG()).Length);
        Texture2D scaled = new Texture2D((int)(tex.width*0.7f), (int)(tex.height*0.7f), TextureFormat.ARGB32, true);
        print(tex.format);
        Graphics.ConvertTexture(tex, scaled);
        tex = scaled;
        print(Convert.ToBase64String(scaled.EncodeToJPG()).Length);
        // Texture2D photo = new Texture2D(tex.height, tex.height);
        // photo.SetPixels(tex.GetPixels((tex.width - tex.height) / 2, 0, tex.height, tex.height, 0));
        // photo.Apply();
        this.GetComponent<RawImage>().texture = scaled;
    }
}
