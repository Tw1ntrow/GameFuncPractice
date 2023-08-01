using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Awake()
    {
        new GameStateController { }.Initialize(null,null);
    }

}
