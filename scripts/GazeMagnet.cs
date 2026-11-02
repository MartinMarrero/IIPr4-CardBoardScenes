
using UnityEngine;

public class GazeMagnet : MonoBehaviour
{
    public void Activate(Transform player)
    {
        GazeCollectible.CollectAll(player);
    }
}
