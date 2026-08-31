using UnityEngine;

public class Bait : MonoBehaviour
{
    public static Transform Current { get; private set; }

    private void OnEnable()
    {
        Current = transform;
    }

    private void OnDisable()
    {
        if (Current == transform)
        {
            Current = null;
        }
    }
}
