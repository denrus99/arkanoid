using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour
{
    private readonly List<ContactPoint> m_contactPoints = new List<ContactPoint>();
    private Rigidbody m_rigidbody;
    private Vector3 m_velocity;
    public float Force;
    public bool IsDestroyed { get; private set; }
    
    private void Start()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_velocity = Random.insideUnitCircle.normalized * Force;
    }

    private void FixedUpdate()
    {
        if (IsDestroyed)
            return;
        if (Camera.main != null)
        {
            var currentPosition = Camera.main.WorldToViewportPoint(transform.position);
            if (currentPosition.x < -0.25 || currentPosition.x > 1.25 || currentPosition.y < -0.25 || currentPosition.y > 1.25)
            {
                IsDestroyed = true;
                Instantiate(gameObject, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)), Quaternion.identity);
                Destroy(gameObject);
            }
        }

        m_rigidbody.velocity = m_velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        var contacts = other.GetContacts(m_contactPoints);
        for (int i = 0; i < contacts; i++)
        {
            m_velocity = Vector3.Reflect(m_velocity, m_contactPoints[i].normal);
            break;
        }
    }
}
