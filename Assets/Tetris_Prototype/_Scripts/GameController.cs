using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private List<GameObject> _figurePrefabs;
    [SerializeField] private Text _highScoreCount;
    [SerializeField] private Button exit;

    [Header("Set Dynamically")]
    [SerializeField] private GameObject _currentFigure;
    [SerializeField] private GameObject _nextFigure;

    private Transform[] _coordsCurrentFigure;
    private Transform[,] _coordXYFigures;

    private float _beginTime;
    private bool _isMovingDown = true;
    private int _score = 0;
    private int _countOfDeleatedLines;

    public Action checkAllChildren;

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null) _instance = new GameController();
            return _instance;
        }
    }



    void Awake()
    {
        _instance = this;

        _coordXYFigures = new Transform[10, 25];

        int _randomCurFigure = UnityEngine.Random.Range(0, 7);
        _currentFigure = Instantiate<GameObject>(_figurePrefabs[_randomCurFigure]);

        int random = UnityEngine.Random.Range(2, 7);

        if (_currentFigure.tag == "NeedCorrectPosition")
        {
            _currentFigure.transform.position = new Vector3(random + 0.5f, 20.5f, 0f);
        }
        else
        {
            _currentFigure.transform.position = new Vector3(random, 21f, 0);
        }

        _coordsCurrentFigure = new Transform[_currentFigure.transform.childCount];

        for (int i = 0; i < _coordsCurrentFigure.Length; i++)
        {
            _coordsCurrentFigure[i] = _currentFigure.transform.GetChild(i).transform;
        }

        _nextFigure = ShowNextFigure();
        _countOfDeleatedLines = 0;
        _beginTime = Time.time;
    }

    void Update()
    {      
        if(Time.time - _beginTime >= 0.3f)
        {
            _isMovingDown = true;
            _beginTime = Time.time;

            float _x = Input.GetAxisRaw("Horizontal");

            if (_x != 0f)
            {
                bool yep = true;

                for (int i = 0; i < _coordsCurrentFigure.Length; i++)
                {
                    int x = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.x);                    
                    if (x + _x < 0 || x + _x > 9)
                    {
                        yep = false;
                        break;
                    }
                }
                
                if(yep) MoveFigureHorizontal(_x);
            }

            foreach (Transform _t in _coordsCurrentFigure)
            {
                int y = Mathf.RoundToInt(_t.transform.position.y);
                if (y <= 0f)
                {
                    SetCurrentFigure(_nextFigure);
                    _isMovingDown = false;
                    break;
                }
            }

            if(_isMovingDown) MoveFigureDown();
        }

        if (Input.GetKeyDown(KeyCode.Space)) RotateFigure(); 
    }

    private void MoveFigureDown()
    {
        _currentFigure.transform.position += Vector3.down;

        for (int i = 0; i < _coordsCurrentFigure.Length; i++)
        {            
            int x = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.x);
            int y = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.y);
            if (_coordXYFigures[x, y] != null)
            {
                _currentFigure.transform.position += Vector3.up;
                SetCurrentFigure(_nextFigure);
                break;
            }
            else
            {
                return;
            }
        }
    }

    private void GameOver()
    {
        foreach(Transform _t in _coordsCurrentFigure)
        {
            if(_t.transform.position.y > 19)
            {
                Debug.Log("GAME OVER!");
                this.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void MoveFigureHorizontal(float _x)
    {
        _currentFigure.transform.position += new Vector3(_x, 0f, 0f);

        for (int i = 0; i < _coordsCurrentFigure.Length; i++)
        {
            int x = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.x);
            int y = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.y);
            if (_coordXYFigures[x, y] != null)
            {
                _currentFigure.transform.position -= new Vector3(_x, 0f, 0f);
                break;
            }
        }
    }

    private void RotateFigure()
    {
        _currentFigure.transform.Rotate(Vector3.forward, 90f);

        for (int i = 0; i < _coordsCurrentFigure.Length; i++)
        {
            if(_coordsCurrentFigure[i].transform.position.x > 9 || _coordsCurrentFigure[i].transform.position.x < 0) { _currentFigure.transform.Rotate(Vector3.forward, -90f); break; }
            
            int x = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.x);
            int y = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.y);

            if(_coordXYFigures[x,y] != null) { _currentFigure.transform.Rotate(Vector3.forward, -90f); break; }
        }
    }

    private void SetCurrentFigure(GameObject _figure)
    {
        GameOver();
        AddCoordinates(); // прежде чем заменить текущую фигуру на новую, надо добавить координаты кубиков в общий список занятых координат
        checkAllChildren();
        CheckLines();

        if (_countOfDeleatedLines == 1) { _score += 100;}
        if (_countOfDeleatedLines == 2) { _score += 300;}
        if (_countOfDeleatedLines == 3) { _score += 700;}
        if (_countOfDeleatedLines == 4) { _score += 1500;}
        
        _highScoreCount.text = "POINTS: " + _score;
        _countOfDeleatedLines = 0;

        int random = UnityEngine.Random.Range(2, 7);

        if (_figure.tag == "NeedCorrectPosition")
        {
            _figure.transform.position = new Vector3(random+ 0.5f, 20.5f, 0f);
        }
        else
        {
            _figure.transform.position = new Vector3(random, 21f, 0);
        }

        _currentFigure = _figure;

        _coordsCurrentFigure = new Transform[_currentFigure.transform.childCount];

        for (int i = 0; i < _coordsCurrentFigure.Length; i++)
        {
            _coordsCurrentFigure[i] = _currentFigure.transform.GetChild(i);
        }

        _nextFigure = ShowNextFigure();


        
    }

    private GameObject ShowNextFigure()
    {
        int _randomNextFigure = UnityEngine.Random.Range(0, 7);
        GameObject _go = Instantiate<GameObject>(_figurePrefabs[_randomNextFigure]);
        _go.transform.position = new Vector3(25f, 50f, 0f);
        return _go;
    }

    private void AddCoordinates()
    {
        for (int i = 0; i < _coordsCurrentFigure.Length; i++)
        {
            int x = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.x);
            int y = Mathf.RoundToInt(_coordsCurrentFigure[i].transform.position.y);
            _coordXYFigures[x, y] = _coordsCurrentFigure[i];
        }
    }

    private void CheckLines()
    {
        List<int> _lineHaveToDelete = new List<int>();

        //я хер его знает как организовать грамотное удаление 4-х строк, надо подумать
        for (int y = 0; y < 20; y++)
        {
            if (CheckLine(y)) _lineHaveToDelete.Add(y);
        }

        int _lines = _lineHaveToDelete.Count;

        if(_lines > 0)
        {
            for (int y = _lineHaveToDelete.Count - 1; y >= 0; y--)
            {
                DeleteLine(y);
            }
        }        
    }

    private void DeleteLine(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            Destroy(_coordXYFigures[x, y].transform.gameObject);
        }
        LineWasDeleted(y);
        _countOfDeleatedLines++;
    }

    private bool CheckLine(int y)
    {
        bool delete = true;

        for (int x = 9; x >=0 ; x--)
        {
            if(_coordXYFigures[x,y] == null)
            {
                delete = false;
                break;
            }
        }

        return delete;
    }

    private void LineWasDeleted(int deleatedLine)
    {
        int downLine = deleatedLine + 1;

        for (int y = downLine; y < 20; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if(_coordXYFigures[x,y] != null)
                {
                    _coordXYFigures[x, y].transform.position += Vector3.down;
                    _coordXYFigures[x, y - 1] = _coordXYFigures[x, y];
                    _coordXYFigures[x, y] = null;
                }
            }
        }
    }
}
