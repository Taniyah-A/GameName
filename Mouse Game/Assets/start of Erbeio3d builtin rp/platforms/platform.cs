using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] Vector3 distance = new Vector3(6, 0, 0);
    [SerializeField] float waitTime = 2f;

    private Vector3 startPos, endPos, targetPos;
    private Vector3 lastPosition;
    private Vector3 platformDelta;
    private float nextMoveTime;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + distance;
        targetPos = endPos;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Time.time < nextMoveTime)
        {
            platformDelta = Vector3.zero;
            return;
        }

        Vector3 currentPos = transform.position;
        Vector3 newPos = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        // Calculate exactly how much we moved this frame
        platformDelta = newPos - lastPosition;

        transform.position = newPos;
        lastPosition = newPos;

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            targetPos = (targetPos == endPos) ? startPos : endPos;
            nextMoveTime = Time.time + waitTime;
        }
    }

    // Public property so the player can grab the movement
    public Vector3 GetDelta() => platformDelta;
}