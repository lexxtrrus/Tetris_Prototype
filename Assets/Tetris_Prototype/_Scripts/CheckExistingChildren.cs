using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckExistingChildren : MonoBehaviour
{
    [SerializeField] int _childCount = 0;

    private void Start()
    {
        _childCount = this.gameObject.transform.childCount;
        GameController.Instance.checkAllChildren += CheckChildren;
    }

    private void CheckChildren()
    {
        _childCount = this.gameObject.transform.childCount;

        if (_childCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameController.Instance.checkAllChildren -= CheckChildren;
    }
}
