using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [Header("Snake Settings")]
    public GameObject bodySegmentPrefab;
    public float moveStep = 0.5f;   // Distance to move each tick
    public float moveRate = 0.2f;   // How often the snake moves (seconds)
    public int maxSegments = 1000;  // Preallocate body slots

    private Transform[] bodySegments;
    private int currentLength = 0;
    private float moveTimer;

    private Vector3 moveDirection = Vector3.forward; // Initial direction

    void Start()
    {
        // Allocate and deactivate all body segments
        bodySegments = new Transform[maxSegments];
        for (int i = 0; i < maxSegments; i++)
        {
            GameObject segment = Instantiate(bodySegmentPrefab);
            segment.SetActive(false);
            bodySegments[i] = segment.transform;
        }

        // Align to grid (optional)
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / moveStep) * moveStep,
            Mathf.Round(transform.position.y / moveStep) * moveStep,
            Mathf.Round(transform.position.z / moveStep) * moveStep
        );
    }

    void Update()
    {
        HandleInput();

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveRate)
        {
            moveTimer = 0f;
            Move();
        }
    }

    void HandleInput()
    {
        // Prevent instant reverse direction if you want to add that rule later
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector3.back)
            moveDirection = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector3.forward)
            moveDirection = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector3.right)
            moveDirection = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector3.left)
            moveDirection = Vector3.right;
    }

    void Move()
    {
        // Save the head's current position
        Vector3 previousPosition = transform.position;

        // Move the head
        transform.position += moveDirection * moveStep;

        // Move each body segment to the previous position of the segment in front
        for (int i = 0; i < currentLength; i++)
        {
            Vector3 temp = bodySegments[i].position;
            bodySegments[i].position = previousPosition;
            previousPosition = temp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            AddBodySegment();
            Destroy(other.gameObject);
        }
    }

    void AddBodySegment()
    {
        if (currentLength >= maxSegments)
        {
            Debug.LogWarning("Max snake length reached!");
            return;
        }

        // Spawn at tail position or just behind head if first segment
        Vector3 newPos = (currentLength > 0)
            ? bodySegments[currentLength - 1].position
            : transform.position - moveDirection * moveStep;

        Transform segment = bodySegments[currentLength];
        segment.position = newPos;
        segment.rotation = Quaternion.identity;
        segment.gameObject.SetActive(true);

        currentLength++;
    }
}
