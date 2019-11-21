using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldCreator : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private GameObject _gridPrefab;
    [SerializeField] private GameObject _wallsPrefab;
    [SerializeField] GameObject _bottom;
    [SerializeField] GameObject _leftWall;
    [SerializeField] GameObject _rightWall;
    [SerializeField] GameObject _gridAnchor;


    void Start()
    {
        //bottom wall
        for (float i = 0.2f; i < 5.4f; i+=0.2f)
        {
            GameObject _cubeWallBottom = Instantiate<GameObject>(_brickPrefab);
            _cubeWallBottom.transform.position = new Vector3(i, 0f, 0f);
            _cubeWallBottom.transform.SetParent(_bottom.transform);
            _cubeWallBottom.transform.localScale = new Vector3(0.15f, 0.2f, 0.2f);
        }

        //walls
        for (float i = 0.2f;  i<=8.2f; i+=0.2f) 
        {
            GameObject _cubeWallLeft = Instantiate<GameObject>(_wallsPrefab);
            _cubeWallLeft.transform.position = new Vector3(0f, i, 0f);
            _cubeWallLeft.transform.parent = _leftWall.transform; // same
            _cubeWallLeft.transform.localScale = new Vector3(0.2f, 0.15f, 0.2f);

            GameObject _cubeWallRight = Instantiate<GameObject>(_wallsPrefab);
            _cubeWallRight.transform.position = new Vector3(5.6f, i, 0f);
            _cubeWallRight.transform.SetParent(_rightWall.transform); // same
            _cubeWallRight.transform.localScale = new Vector3(0.2f, 0.15f, 0.2f);
        }

        //grid
        for (float i = 0.2f; i < 5.4f; i+=0.2f)
        {
            for (float j = 0.2f; j <= 7.2f; j+=0.2f)
            {
                GameObject _grid = Instantiate<GameObject>(_gridPrefab);
                _grid.GetComponent<Renderer>().material.color = Color.green;
                _grid.transform.position = new Vector3(i, j, 2f);
                _grid.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                _grid.transform.SetParent(_gridAnchor.transform);
            }
        }

    }    
}
