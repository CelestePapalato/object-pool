using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class ObjectPoolData
    {
        public GameObject objectToPool;
        public int quantity;
    }

    public static ObjectPool instance;

    public ObjectPoolData[] ObjectsToPool;

    private List<GameObject> pool = new List<GameObject>();

    private Dictionary<string, List<GameObject>> taggedPool = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        foreach (ObjectPoolData poolData in ObjectsToPool)
        {
            for (int i = 0; i < poolData.quantity; i++)
            {
                GameObject tmp = Instantiate(poolData.objectToPool);
                tmp.SetActive(false);

                if (!taggedPool.ContainsKey(tmp.tag))
                {
                    taggedPool.Add(tmp.tag, new List<GameObject>());
                }
                taggedPool[tmp.tag].Add(tmp);
            }
        }

        foreach(List<GameObject> poolData in taggedPool.Values)
        {
            pool.AddRange(poolData);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject instance in pool)
        {
            if (!instance.activeInHierarchy)
            {
                return instance;
            }
        }
        return null;
    }

    public GameObject GetPooledObject(string tag)
    {
        if (taggedPool.ContainsKey(tag))
        {
            foreach (GameObject obj in taggedPool[tag])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }
        return null;
    }
}
