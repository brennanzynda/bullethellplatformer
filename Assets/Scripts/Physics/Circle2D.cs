using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle2D : Particle2D
{
    [SerializeField]
    private float m_radius;

    new
        // Start is called before the first frame update
        void Start()
    {
        m_objectType = ObjectType.Circle;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRadius() { return m_radius; }
}
