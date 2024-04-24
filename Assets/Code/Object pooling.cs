using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pooling: MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> pooldic;
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<pool> pools;

    public static Object_pooling instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pooldic = new Dictionary<string, Queue<GameObject>>();

        foreach(pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }
            pooldic.Add(pool.tag, objectpool);  
        }
    }
    public GameObject spawnfrompool(string tag, Vector2 posistion, Quaternion rotation)
    {
        if (!pooldic.ContainsKey(tag))
        {
            Debug.LogWarning("no tag");
            return null;
        }
        GameObject objecttospawn = pooldic[tag].Dequeue();

        objecttospawn.transform.position = posistion;
        objecttospawn.transform.rotation = rotation;
        objecttospawn.SetActive(true);
        pooldic[tag].Enqueue(objecttospawn);
        return objecttospawn;
    }
}
