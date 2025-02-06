using System;
using _Project.Scripts.Figure.Factories;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace _Project.Scripts.Figure.Types
{
    public class Cube : MonoBehaviour, IFigure
    {
        public event Action<GameObject, Vector3, PointerEventData> OnStartDraggingEv;
        public event Action OnFinishDraggingEv;
        
        private Canvas _canvas;
        private RectTransform _canvasRectTransform; //Временное решение! Переделать!
        private bool _isDublicate = false;
        private float _scaleFactor;
        private FigureFactory _factory;
        private Cube _clone;
        
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _rectTransform;


        [SerializeField] private RectTransform _rectTransformParent; //test
        
        public RectTransform RectTransform => _rectTransform;

        public void Initialize(Canvas canvas, FigureFactory factory, RectTransform parent) //Временное решение! Переделать!
        {
            _canvas = canvas;
            _canvasRectTransform = _canvas.GetComponent<RectTransform>();
            _factory = factory;
            _rectTransformParent = parent;
        }

        private void Start()
        {
            _scaleFactor = _canvas.scaleFactor;
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        public void MarkAsDuplicate(bool isDuplicate)
        {
            _isDublicate = isDuplicate;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector3 worldPosition = _rectTransform.position / _scaleFactor;
            
            if (!_isDublicate)
            {
                _clone = _factory.CreateFigureDuplicate(gameObject);
                _clone.RectTransform.SetParent(_canvasRectTransform, false);
                _clone.RectTransform.position = worldPosition;
            }
            else
            {
                _rectTransform.SetParent(_canvasRectTransform, false);
                _rectTransform.position = worldPosition;
            }
        }
        

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDublicate)
                _clone.RectTransform.anchoredPosition += eventData.delta;
            else
                _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDublicate)
            {
                OnFinishDraggingEv?.Invoke();
            }
            else
            {
                var parent = FindNewParent(eventData);
            
                _rectTransform.SetParent(parent, false);    
            }
        }
        
        public Transform FindNewParent(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                return eventData.pointerCurrentRaycast.gameObject.transform;
            }

            return null;
        }
    }
}