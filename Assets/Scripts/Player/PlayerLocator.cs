using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerLocator Instance { get; private set; }
    public Transform PlayerTransform => transform;

    private void Awake()
    {
        Instance = this;
    }
}
