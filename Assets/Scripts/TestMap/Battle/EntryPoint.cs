using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Awake()
    {
        new GameStateController { }.Initialize(new TestUnitCreator(), new TestMapCreatable());
    }

}
