using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    private float _xMin = 0.2f;
    private float _xMax = 5.4f;
    private GameObject _lastTriggerEnter;
    private GameObject _currentTriggerEnter;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            _lastTriggerEnter = this.gameObject.GetComponentInParent<TriggerCheck>().LastTriggerEnter;
            _currentTriggerEnter = other.gameObject.transform.parent.gameObject;

            if (_lastTriggerEnter == _currentTriggerEnter) return;

            this.gameObject.GetComponentInParent<TriggerCheck>().LastTriggerEnter = _currentTriggerEnter;
            GameObject _figure = Game.Instance.CurrentFigure;
            _figure.transform.position += new Vector3(0f, 0.2f, 0f);
            Game.Instance.ReplaceFigure();
            _lastTriggerEnter = null;
        }

        if (other.tag == "Wall")
        {
            _lastTriggerEnter = this.gameObject.GetComponentInParent<TriggerCheck>().LastTriggerEnter;
            _currentTriggerEnter = other.gameObject.transform.parent.gameObject;

            if (_lastTriggerEnter == _currentTriggerEnter) return;

            this.gameObject.GetComponentInParent<TriggerCheck>().LastTriggerEnter = _currentTriggerEnter;
            GameObject _figure = Game.Instance.CurrentFigure;List<GameObject> _bricks = new List<GameObject>();
            int parts = _figure.transform.childCount;

            for (int i = 0; i < parts; i++)
            {
                Transform _trans = _figure.gameObject.transform.GetChild(i);
                GameObject _temp = _trans.gameObject;
                _bricks.Add(_temp);
            }

            for (int i = 0; i < _bricks.Count; i++)
            {
                Vector3 pos = _bricks[i].transform.position;
                if(pos.x < _xMin)
                {
                    _figure.transform.position += new Vector3(0.2f, 0f, 0f);
                }

                if (pos.x > _xMax)
                {
                    _figure.transform.position -= new Vector3(0.2f, 0f, 0f);
                }
            }

            this.gameObject.GetComponentInParent<TriggerCheck>().LastTriggerEnter = null;
        }
    }
}
