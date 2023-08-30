using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 m_initialPos;
    private Vector3 m_endPos;

    public void SetInitialPosition(Vector3 initialPosition, Vector3 endPosition)
    {
        m_initialPos = initialPosition;
        m_endPos = endPosition;
        transform.position = m_initialPos;
    }
    
    public void SetPosition(float value)
    {
        transform.position = Vector3.Lerp(m_initialPos, m_endPos, value);
    }
}
