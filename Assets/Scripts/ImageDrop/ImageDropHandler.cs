using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ImageDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private RawImage image;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            string[] droppedFiles = DragAndDrop.paths;

            if (droppedFiles != null && droppedFiles.Length > 0)
            {
                string imagePath = droppedFiles[0];
                LoadImage(imagePath);
            }
        }
    }

    private void LoadImage(string path)
    {
        if (System.IO.File.Exists(path))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);// 仮のサイズでテクスチャを作成
            texture.LoadImage(fileData);
            image.texture = texture;
        }
    }
}