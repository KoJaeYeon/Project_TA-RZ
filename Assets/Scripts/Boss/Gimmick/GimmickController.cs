using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProBuilder;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _downValue;
    [SerializeField] private float _upValue;

    [SerializeField] private ProBuilderMesh m_Editor;

    private void Awake()
    {
        m_Editor = GetComponent<ProBuilderMesh>();
    }

    private void Update()
    {
        Test();
    }

    private void Test()
    {
        Vector3 scale = m_Editor.transform.localScale;
        scale.x += Time.deltaTime;
        scale.z += Time.deltaTime;
        m_Editor.transform.localScale = scale;
    }
}
