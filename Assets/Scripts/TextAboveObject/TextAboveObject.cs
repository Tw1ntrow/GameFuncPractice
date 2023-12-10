using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class TextAboveObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private Vector3 offset;

    private TextMesh textMesh;

    void Start()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        textMesh = GetComponent<TextMesh>();
        this.transform.DOMove(new Vector3(2f, 0f, 0f), 1f).SetLoops(3, LoopType.Yoyo);
    }

    void Update()
    {
        if (targetObject)
        {
            // モデルの上にテキストを配置する
            transform.position = targetObject.position + offset;
        }
        transform.LookAt(Camera.main.transform);
    }

    public void SetText(string newText)
    {
        if (textMesh)
        {
            textMesh.text = newText;
        }
    }
}