using System;
using _Project.Scripts.Figure.Factories;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Figure.Types
{
    public interface IFigure : IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<GameObject, Vector3, PointerEventData> OnStartDraggingEv; 
        public event Action OnFinishDraggingEv; 
        public RectTransform RectTransform { get; }
        
        public void SetColor(Color color);
        public Transform FindNewParent(PointerEventData eventData);
        public void Initialize(Canvas canvas, FigureFactory figureFactory, RectTransform rectTransform);
        
    }
}