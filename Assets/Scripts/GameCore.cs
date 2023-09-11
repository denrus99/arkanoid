using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [SerializeField]
    private GameObject m_meteorPrefab;
    public Platform Left;
    public Platform Right;
    private Vector3 m_position;
    private Vector3 m_touchPosWorld;
    private IInputProvider m_inputProvider;

    private void Awake()
    {
        SetupPlatform(Right, new Vector3(1, 1, 1), new Vector3(1, 0, 1), false);
        SetupPlatform(Left, new Vector3(0, 0, 1), new Vector3(0, 1, 1), true);
#if UNITY_EDITOR || UNITY_STANDALONE
        m_inputProvider = new KeyboardInput(new List<Platform>(){Left, Right});
#else
        m_inputProvider = new TouchInput(new List<Platform>(){Left, Right});
#endif
        if (m_meteorPrefab != null)
            Instantiate(m_meteorPrefab, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)), Quaternion.identity);
    }

    private void Update()
    {
        m_inputProvider.Update();
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
