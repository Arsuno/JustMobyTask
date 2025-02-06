using System.Collections.Generic;
using _Project.Scripts.Configs;
using _Project.Scripts.Figure.Types;
using _Project.Scripts.Scroller;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Figure.Factories
{
    public class FigureFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _rectanglePrefab;
        [SerializeField] private GameObject _cubePrefab;

        [SerializeField] private Canvas _gameCanvas; //Временное решение
        [SerializeField] private RectTransform _scrollRectTransform;
        
        private int _cubeCounts;
        private int _rectangleCount;

        private Color[] _figuresColors;
        private FigureScroller _figureScroller; //Скорее всего стоит убрать из этого класса

        private readonly List<IFigure> _spawnedFigures = new();
        
        [Inject]
        public void Construct(GameConfig config, FigureScroller scroller)
        {
            _cubeCounts = config.CubesCount;
            _rectangleCount = config.RectangleCount;
            _figuresColors = config.FiguresColors;
            _figureScroller = scroller;
        }

        public Cube CreateFigureDuplicate(GameObject original)
        {
            var duplicate = Instantiate(original);
            var cubeComponent = duplicate.GetComponent<Cube>();

            cubeComponent.Initialize(_gameCanvas, gameObject.GetComponent<FigureFactory>(), _scrollRectTransform); 
            cubeComponent.MarkAsDuplicate(true);

            return cubeComponent;
        }

        public List<IFigure> CreateFigures(Transform parent)
        {
            _spawnedFigures.Clear();
            
            CreateFigureInstances(_rectanglePrefab, parent, _rectangleCount);
            CreateFigureInstances(_cubePrefab, parent, _cubeCounts);

            InitializeFigures();
            
            return _spawnedFigures;
        }
        
        private void CreateFigureInstances(GameObject prefab, Transform parent, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var figure = Instantiate(prefab, parent.transform).GetComponent<IFigure>();
                
                figure.SetColor(_figuresColors[i]);
                _spawnedFigures.Add(figure);
            }
        }
        
        private void InitializeFigures()
        {
            foreach (var figure in _spawnedFigures)
            {
                figure.Initialize(_gameCanvas, gameObject.GetComponent<FigureFactory>(), _scrollRectTransform); //Убрать такую инициализацию от сюда, сделать через Zenject
            }
        }
    }
}