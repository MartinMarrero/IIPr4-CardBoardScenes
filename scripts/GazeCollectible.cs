
using UnityEngine;
using System.Collections.Generic;

public class GazeCollectible : MonoBehaviour
{
    public float attractionSpeed = 5f;

    private Transform player;
    private bool collected;

    private static List<GazeCollectible> allCollectibles =
        new List<GazeCollectible>();

    private void OnEnable()
    {
        if (!allCollectibles.Contains(this))
            allCollectibles.Add(this);
    }

    private void OnDisable()
    {
        allCollectibles.Remove(this);
    }

    public void Collect(Transform target)
    {
        if (collected)
            return;

        player = target;
        collected = true;
    }

    public static void CollectAll(Transform target)
    {
        foreach (GazeCollectible item in
                 allCollectibles.ToArray())
        {
            if (item != null)
                item.Collect(target);
        }
    }

    private void Update()
    {
        if (!collected || player == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            attractionSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, player.position) < 0.3f)
            gameObject.SetActive(false);
    }
}
