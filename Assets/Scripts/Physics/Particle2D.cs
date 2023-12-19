using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public enum ObjectType
    {
        Circle,
        Square
    }
    protected ObjectType m_objectType;
    private Transform m_transform;
    private Vector2 m_position;
    private Vector2 m_velocity;
    private List<Vector2> m_forcesToApply;
    [SerializeField, Tooltip("If object should NOT invoke physics-based collision handling")]
    private bool m_isTrigger;
    [SerializeField]
    private bool m_freezeObject;

    // Editor-Accessible Non-Negative Mass
    [SerializeField, Min(0.0f), Tooltip("Mass of the Object")]
    private float m_mass;

    // Initializing all member variables
    void Awake()
    {
        m_transform = transform;
        m_position = m_transform.position;
        m_velocity = Vector2.zero;
        m_forcesToApply = new List<Vector2>();
    }

    protected void Start()
    {
        GameObject.Find("CollisionHandler").GetComponent<CollisionCheck>().AddObjectToCollisionList(gameObject);
    }

    // Physics Updates
    void FixedUpdate()
    {
        m_position = m_transform.position;
        if (!m_freezeObject)
        {
            float timeStep = Time.unscaledDeltaTime;

            // Calculating Composite 2D Force
            Vector2 appliedForce = ComputeForce2D(m_forcesToApply);
            Vector2 acceleration;

            // Preventing divided by 0 errors
            if (m_mass == 0.0f)
            {
                acceleration = Vector2.zero;
            }
            else
            {
                acceleration = new(appliedForce.x / m_mass, appliedForce.y / m_mass);
            }
        
            // Calculating velocity from acceleration
            Debug.Log(m_velocity);
            m_velocity.x += acceleration.x * timeStep;
            m_velocity.y += acceleration.y * timeStep;
            // Calculating position from velocity
            //Debug.Log(m_position);
            m_position.x += m_velocity.x * timeStep;
            m_position.y += m_velocity.y * timeStep;
            // Assigning new position to object
            m_transform.position = (Vector3)m_position;
        }
        else
        {
            m_velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Function to compute 2D force on an object given a list of forces
    /// </summary>
    /// <param name="forces">List of 2D vectors representing forces</param>
    /// <returns>Vector2</returns>
    private Vector2 ComputeForce2D(List<Vector2> forces)
    {
        float verticalForce = (-9.81f * m_mass);
        float horiztontalForce = 0.0f;
        foreach (Vector2 force in forces) 
        {
            horiztontalForce += force.x;
            verticalForce += force.y;
        }
        m_forcesToApply.Clear();
        return new Vector2(horiztontalForce, verticalForce);
    }

    public void AddForce(Vector2 force)
    {
        m_forcesToApply.Add(force);
    }

    public Vector2 GetPosition() { return m_position; }
    public ObjectType GetObjectType() { return m_objectType; }

    public bool GetIsTrigger() { return m_isTrigger; }

    public void FreezeObject() { m_freezeObject = true; }
    public void UnfreezeObject() { m_freezeObject = false; }
}
