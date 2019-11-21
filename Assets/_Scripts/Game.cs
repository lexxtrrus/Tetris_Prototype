using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    #region Singleton
    private Game() { }
    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if(_instance == null) { _instance = new Game(); }
            return _instance;
        }
        set { _instance = value; }
    }
    #endregion

    #region Properties
    public Vector3 POS
    {
        get { return _currentFigure.transform.position; }
        set { _currentFigure.transform.position = value; }
    }
    
    public GameObject CurrentFigure
    {
        get
        {
            return _currentFigure;
        }
    }
    #endregion

    #region Fields
    [Header("Set In Inspector")]
    [SerializeField] List<GameObject> _figures;

    [Header("Set Dynamically")]
    [SerializeField] private GameObject _currentFigure;
    [SerializeField] private GameObject _nextFigure;

    private float _beginTimeY;
    private float _stepTimeY = 0.2f;
    private float _beginTimeX;
    private float _stepTimeX = 0.1f;
    #endregion

    void Awake()
    {
        Instance = this;

        int _index = ChooseFigure();
        _currentFigure = Instantiate<GameObject>(_figures[_index]);
        float _xRandom = Random.Range(2, 4);
        _currentFigure.transform.position = new Vector3(_xRandom, 8.2f, 0f);
        _nextFigure = ShowNextFigure();

        _beginTimeX = Time.time;
        _beginTimeY = Time.time;
    }
    void Update()
    {
            float _currentTime = Time.time;
            {
                if (_currentTime - _beginTimeX >= _stepTimeX)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        Vector3 _pos = POS;
                        _pos.x -= 0.2f;
                        POS = _pos;
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        Vector3 _pos = POS;
                        _pos.x += 0.2f;
                        POS = _pos;
                    }

                    _beginTimeX = _currentTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentFigure.transform.Rotate(Vector3.forward, 90f);
            }

            if (_currentTime - _beginTimeY >= _stepTimeY)
            {
                Vector3 _pos = POS;
                _pos.y -= 0.2f;
                POS = _pos;
                _beginTimeY = _currentTime;
            }
    }


    private void SetCurrentFigure(GameObject _figure)
    {
        float _xRandom = Random.Range(2, 4);
        _figure.transform.position = new Vector3(_xRandom, 8.2f, 0f);
        _currentFigure = _figure;
        _nextFigure = ShowNextFigure();
    }

    private GameObject ShowNextFigure()
    {
        int _index = ChooseFigure();
        GameObject _go = Instantiate<GameObject>(_figures[_index]);
        _go.transform.position = new Vector3(25f, 50f, 0f);
        return _go;
    }

    private int ChooseFigure()
    {
       return Random.Range(0, _figures.Count);
    }

    public void ReplaceFigure()
    {
        SetCurrentFigure(_nextFigure);
    }
}
