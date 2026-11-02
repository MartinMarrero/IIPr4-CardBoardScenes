
using UnityEngine;

public class GazeInteractor : MonoBehaviour
{
    public Transform playerRoot;
    public float gazeDuration = 1f;
    public float maxDistance = 20f;
    public float moveSpeed = 3f;

    private Component currentTarget;
    private float gazeTimer;
    private bool activated;

    private Vector3 moveTarget;
    private bool isMoving;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            GazeCollectible collectible =
                hit.collider.GetComponent<GazeCollectible>();

            GazeMagnet magnet =
                hit.collider.GetComponent<GazeMagnet>();

            Component target = null;

            if (collectible != null)
                target = collectible;
            else if (magnet != null)
                target = magnet;
            else if (hit.collider.CompareTag("Ground"))
                target = hit.collider;

            if (target != currentTarget)
            {
                currentTarget = target;
                gazeTimer = 0f;
                activated = false;
            }

            if (target != null && !activated)
            {
                gazeTimer += Time.deltaTime;

                if (gazeTimer >= gazeDuration)
                {
                    activated = true;

                    if (collectible != null)
                        collectible.Collect(playerRoot);

                    else if (magnet != null)
                        magnet.Activate(playerRoot);

                    else if (hit.collider.CompareTag("Ground"))
                    {
                        moveTarget = hit.point;
                        moveTarget.y = playerRoot.position.y;
                        isMoving = true;
                    }
                }
            }
        }
        else
        {
            currentTarget = null;
            gazeTimer = 0f;
            activated = false;
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!isMoving)
            return;

        playerRoot.position = Vector3.MoveTowards(
            playerRoot.position,
            moveTarget,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(playerRoot.position, moveTarget) < 0.1f)
            isMoving = false;
    }
}
