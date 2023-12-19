using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private List<GameObject> m_collidableObjects;

    private void Awake()
    {
        m_collidableObjects = new List<GameObject>();
    }
    private void FixedUpdate()
    {
        if (m_collidableObjects.Count > 1)
        {
            for (int i = 0; i < m_collidableObjects.Count - 1; ++i)
            {
                for (int j = i + 1; j < m_collidableObjects.Count; j++)
                {
                    GameObject obj1 = m_collidableObjects[i];
                    GameObject obj2 = m_collidableObjects[j];
                    if (obj1.GetComponent<Particle2D>().GetObjectType() == Particle2D.ObjectType.Circle)
                    {
                        if (obj2.GetComponent<Particle2D>().GetObjectType() == Particle2D.ObjectType.Circle)
                        {
                            if(CheckCircleVsCircle2D(obj1.GetComponent<Circle2D>(), obj2.GetComponent<Circle2D>()))
                            {
                                Debug.Log(obj1 + " " + obj2 + " True");
                                // Handle Collision of Circle v Circle Here
                                obj1.GetComponent<Particle2D>().FreezeObject();
                                obj2.GetComponent<Particle2D>().FreezeObject();
                            }
                            else
                            {
                                Debug.Log(obj1 + " " + obj2 + " False");
                            }
                        }
                    }
                }
            }
        }
    }

    private bool CheckCircleVsCircle2D(Circle2D c1, Circle2D c2)
    {
        Vector2 c1Pos = c1.GetPosition();
        Vector2 c2Pos = c2.GetPosition();
        float xDis = c1Pos.x - c2Pos.x;
        float yDis = c1Pos.y - c2Pos.y;
        float distance = Mathf.Sqrt((xDis * xDis) + (yDis * yDis));
        if(distance < (c1.GetRadius() + c2.GetRadius()))
        {
            return true;
        }
        return false;
    }

    public void AddObjectToCollisionList(GameObject obj)
    {
        m_collidableObjects.Add(obj);
    }

    public void RemoveObjectFromCollisionList(GameObject obj) 
    {
        m_collidableObjects.Remove(obj);
    }
}
