using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrainSystem : MonoBehaviour
{
    List<Rigidbody> DrainItemList = new List<Rigidbody>();
    List<Rigidbody> DrainedItemList = new List<Rigidbody>();
    [Inject] Player player;
    [SerializeField] GameObject[] DrainEffect;

    private float _pull_speed = 1;

    private void OnEnable()
    {
        DrainItemList.Clear();
    }

    private void Update()
    {
        foreach (var item in DrainItemList)
        {
            Vector3 drainDir = transform.position - item.transform.position;
            item.AddForce(drainDir * Time.deltaTime * 1000 * _pull_speed);
            float distance = Vector3.Distance(transform.position, item.position);
            if(distance < 1.5f)
            {
                item.gameObject.SetActive(false);
                player.currentAmmo += 1;
                DrainedItemList.Add(item);
            }
        }

        if(DrainedItemList.Count > 0)
        {
            foreach(var item in DrainedItemList)
            {
                DrainItemList.Remove(item);
            }
            DrainedItemList.Clear();
        }
    }

    public void OnSetActiveDraintEffect(bool isAcitve)
    {
        foreach (var item in DrainEffect)
        {
            item.SetActive(isAcitve);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ThrowItem"))
        {
            var itemRigid = other.GetComponent<Rigidbody>();
            DrainItemList.Add(itemRigid);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ThrowItem"))
        {
            var itemRigid = other.GetComponent<Rigidbody>();
            DrainItemList.Remove(itemRigid);
        }
    }    
}
