using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] int poolSize;
    [SerializeField] private RectTransform parent;
    
    private Queue<GameObject> _pool;
    private static ObjectPool _instance;
    private List<GameObject> clone = new List<GameObject>();

    public static ObjectPool Instance
    {
        get{ return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        _pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown("space"))
        {
            clone[3].SetActive(false);
        }*/
    }

    /// <summary>
    /// 오브젝트 풀에 새로운 오브젝트 생성 함수
    /// </summary>
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(Prefab, parent);
        newObject.SetActive(false);
        _pool.Enqueue(newObject);
        clone.Add(newObject);
    }
    
    /// <summary>
    /// 오브젝트 풀에 있는 오브젝트 반환 함수
    /// </summary>
    /// <returns>오브젝트 풀에 있는 오브젝트</returns>
    public GameObject GetObject()
    {
        if(_pool.Count == 0) CreateNewObject();
        
        GameObject dequeueObject = _pool.Dequeue();
        dequeueObject.SetActive(true);
        return dequeueObject;
    }
    
    /// <summary>
    /// 사용한 오브젝트를 오브젝트 풀로 되돌려주는 함수
    /// </summary>
    /// <param name="returnObject">반환할 오브젝트</param>
    public void ReturnCard(GameObject returnObject)
    {
        returnObject.SetActive(false);
        _pool.Enqueue(returnObject);
    }
}
