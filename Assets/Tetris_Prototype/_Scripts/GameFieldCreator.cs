using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldCreator : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _gridPrefab;
    [SerializeField] private GameObject _wallsAnchor;
    [SerializeField] private GameObject _gridAnchor;


    void Awake()
    {
        for (float i = -1f; i <= 10f; i++)
        {
            GameObject _cubeWallBottom = Instantiate<GameObject>(_wallPrefab);
            _cubeWallBottom.transform.position = new Vector3(i, -1f, 0f);
            _cubeWallBottom.transform.SetParent(_wallsAnchor.transform);;
        }
        for (float i = 0f;  i<=19; i++) 
        {
            GameObject _cubeWallLeft = Instantiate<GameObject>(_wallPrefab);
            _cubeWallLeft.transform.position = new Vector3(-1f, i, 0f);
            _cubeWallLeft.transform.parent = _wallsAnchor.transform;

            GameObject _cubeWallRight = Instantiate<GameObject>(_wallPrefab);
            _cubeWallRight.transform.position = new Vector3(10f, i, 0f);
            _cubeWallRight.transform.SetParent(_wallsAnchor.transform);
        }
        for (float i = 0f; i <= 19f; i++)
        {
            for (float j = 0f; j <= 9f; j++)
            {
                GameObject _grid = Instantiate<GameObject>(_gridPrefab);
                _grid.transform.position = new Vector3(j, i, 0f);
                _grid.transform.SetParent(_gridAnchor.transform);
            }
        }
    }    
}
