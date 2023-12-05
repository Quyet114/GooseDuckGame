using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadAvatar : MonoBehaviour
{
    //private SpriteRenderer spriteRenderer;
    public RawImage rawImage;
    public string imageUrl;
    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        imageUrl = LoginScript.loginResponse.profilePicture;
        StartCoroutine(LoadImage(imageUrl));
    }

    // Update is called once per frame


    IEnumerator LoadImage(string imageUrl)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            // Xử lý lỗi khi không thể tải hình ảnh từ URL
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            //dùng Sprite
            /*Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            spriteRenderer.sprite = sprite;*/

            //dùng Raw
            rawImage.texture = texture;

        }
    }
}
