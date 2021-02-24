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
    public void StartTakingPicture(){
        StartCoroutine("TakeAPicture");
    }

    public IEnumerator TakeAPicture(){
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //photo.GetPixels((photo.width - cropSize) / 2, 0, cropSize, photo.height, 0);
        Color[] color = photo.GetPixels((photo.width - photo.height) / 2, 0, photo.height, photo.height, 0);
        Texture2D newTex = new Texture2D(photo.height, photo.height);
        newTex.SetPixels(color, 1);
        newTex.Apply();

        //oImg.GetPixels((oImg.width - cropSize) / 2, (oImg.height - cropSize) / 2, cropSize, cropSize, 0);

        //Encode to a PNG
        byte[] bytes = newTex.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(@"photo.png", bytes);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
