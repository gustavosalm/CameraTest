using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    private float newScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        webCamTexture.deviceName = WebCamTexture.devices[1].name;
        webCamTexture.Play();
        GetComponent<RawImage>().texture = webCamTexture;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(webCamTexture.width, webCamTexture.height);
        float hScale = ((float)Screen.height/(float)webCamTexture.height);
        float wScale = ((float)Screen.width/(float)webCamTexture.width);
        newScale = /*(hScale < wScale) ? hScale : wScale*/ hScale;
        this.GetComponent<RectTransform>().localScale = new Vector3(newScale, newScale, 1);
        print(Screen.height + "  " + webCamTexture.height);
        print(Screen.width + "  " + webCamTexture.width);
    }

    void Update(){
        GetComponent<RawImage>().texture = webCamTexture;
    }

    public void StartTakingPicture(){
        StartCoroutine("TakeAPicture");
    }

    public IEnumerator TakeAPicture(){
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(1, 1);
        if(webCamTexture.height < webCamTexture.width){
            photo = new Texture2D(webCamTexture.height, webCamTexture.height);
            photo.SetPixels(webCamTexture.GetPixels((webCamTexture.width - webCamTexture.height) / 2, 0, webCamTexture.height, webCamTexture.height));
            photo.Apply();
        }
        else{
            photo = new Texture2D(webCamTexture.width, webCamTexture.width);
            photo.SetPixels(webCamTexture.GetPixels(0, (webCamTexture.height - webCamTexture.width) / 2, webCamTexture.width, webCamTexture.width));
            photo.Apply();
        }

        //photo.GetPixels((photo.width - cropSize) / 2, 0, cropSize, photo.height, 0);
        //oImg.GetPixels((oImg.width - cropSize) / 2, (oImg.height - cropSize) / 2, cropSize, cropSize, 0);

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        GameObject.Find("path").GetComponent<Text>().text = Application.persistentDataPath;
        print(Application.persistentDataPath);
        NativeGallery.SaveImageToGallery(photo, "GalleryTest", "photo" + System.DateTime.Now.ToString("h:mm:ss tt") + ".png");
        //File.WriteAllBytes($"{Application.persistentDataPath}/photo.png", bytes);
        //yield return new WaitForSeconds(4);
        //GameObject.Find("path").GetComponent<Text>().text = (File.Exists($"{Application.persistentDataPath}/photo.png")) ? "existe" : "nope";
    }
}
