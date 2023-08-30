using UnityEngine;

public class GameCore : MonoBehaviour
{
    public Platform Left;
    public Platform Right;
    private Vector3 position;
    private Vector3 touchPosWorld;

    private void Awake()
    {
        SetupPlatform(Right, new Vector3(1, 1, 1), new Vector3(1, 0, 1), false);
        SetupPlatform(Left, new Vector3(0, 0, 1), new Vector3(0, 1, 1), true);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var u = touch.position.x / Screen.width;
            Left.SetPosition(u);
            Right.SetPosition(u);
        }
    }

    private void SetupPlatform(Platform platform, Vector3 viewportInitPos, Vector3 viewportEndPos, bool isLeft)
    {
        var initPos = Camera.main.ViewportToWorldPoint(viewportInitPos);
        var endPos = Camera.main.ViewportToWorldPoint(viewportEndPos);
        var bounds = platform.GetComponent<Collider>().bounds;
        if (isLeft)
        {
            initPos += new Vector3(bounds.extents.x, bounds.extents.y);
            endPos += new Vector3(bounds.extents.x, -bounds.extents.y);
        }
        else
        {
            initPos += new Vector3(-bounds.extents.x, -bounds.extents.y);
            endPos += new Vector3(-bounds.extents.x, bounds.extents.y);
        }
        platform.SetInitialPosition(initPos, endPos);
    }
}
