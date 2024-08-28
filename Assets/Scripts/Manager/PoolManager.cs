using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public static class QueueExtensions
{
    public static void EnqueuePool<T>(this Queue<T> queue, T item) where T : Component
    {
        item.gameObject.SetActive(false);
        queue.Enqueue(item);
    }

    public static T DequeuePool<T>(this Queue<T>queue) where T : Component
    {
        if(queue.Count == 0)
        {
            return null;
        }

        T item = queue.Dequeue();
        item.gameObject.SetActive(true);
        return item;    
    }
}

public class Pool
{
    public Queue<Component> _queue;
    public int _count;
    public Transform _transform;

    public Pool(Transform transform)
    {
        _queue = new Queue<Component>();
        _count = 0;
        _transform = transform;
    }
}

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, Pool> _objectPools = new Dictionary<string, Pool>();
    [Inject]
    public DiContainer _di;

    //몬스터가 죽었을 때 Action 같은 이벤트로 죽었음을 알려준다 bool 값
    //풀을 생성하는 메서드. 프리팹의 이름으로 풀을 생성한다.
    public void CreatePool(GameObject prefab, int count = 50)
    {
        string itemType = prefab.name;

        if(!_objectPools.ContainsKey(itemType)) //키가 없다면
        {
            GameObject pool = new GameObject(); //게임 오브젝트를 생성

            pool.transform.SetParent(this.transform);
            pool.name = prefab.name + "Pool";
            _objectPools.Add(itemType, new Pool(pool.transform)); //딕셔너리에 풀 추가

            for (int i = 0; i < count; i++) //아이템을 생성하고 큐에 넣는 부분
            {
                GameObject item = _di.InstantiatePrefab(prefab, _objectPools[itemType]._transform);
                item.name = itemType;
                _objectPools[itemType]._queue.EnqueuePool(item.GetComponent<Component>());
                _objectPools[itemType]._count++;
            }
        }
    } 

    //사용한 오브젝트를 다시 풀에 반환하는 메서드
    public void EnqueueObject(GameObject item)
    {
        string itemType = item.name;

        if (!_objectPools.ContainsKey(itemType))
        {
            CreatePool(item);
        }

        item.transform.SetParent(_objectPools[itemType]._transform);
        _objectPools[itemType]._queue.EnqueuePool(item.GetComponent<Component>());
    }

    //현재 풀에 있는 모든 오브젝트 비활성화
    public void AllDestroyObject(GameObject prefab)
    {
        string itemType = prefab.name;

        if (!_objectPools.ContainsKey(itemType))
        {
            CreatePool(prefab);
        }

        for(int i= 0; i < _objectPools[itemType]._transform.childCount; i++)
        {
            GameObject item = _objectPools[itemType]._transform.GetChild(i).gameObject;

            if (item.activeSelf)
            {
                EnqueueObject(item);
            }
        }
    }

    //사용하고자 하는 오브젝트 반환
    public GameObject DequeueObject(GameObject prefab)
    {
        string itemType = prefab.name;

        if (!_objectPools.ContainsKey(itemType)) //키가 없으면 풀 생성
        {
            CreatePool(prefab);
        }
        //컴포넌트 형식으로 저장했기 때문에 Dequeue역시 컴포넌트 형식으로 가져옴.
        Component? dequeueObject = _objectPools[itemType]._queue.DequeuePool();

        if (dequeueObject != null)
        {
            return dequeueObject.gameObject; //오브젝트 반환
        }
        else
        {
            CreatePool(prefab, _objectPools[itemType]._count); //없으면 풀 생성하고 반환
            return DequeueObject(prefab);
        }
    }

}
