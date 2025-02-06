using System.Collections.Generic;
using _Project.Scripts.Figure.Factories;
using _Project.Scripts.Figure.Types;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Scroller
{
    public class FigureScroller : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private ScrollRect _scrollRect;

        private List<IFigure> _elements;
        private FigureFactory _factory;
        
        public bool CanScroll => _scrollRect.enabled;
        public List<IFigure> Elements => _elements;

        [Inject]
        public void Construct(FigureFactory factory)
        {
            _factory = factory;
        }
        
        public void SetAvailableState(bool state)
        {
            _scrollRect.enabled = state;
        }

        private void Start()
        {
            _elements = new List<IFigure>();
               
            _elements.AddRange(_factory.CreateFigures(_parent));
        }
    }
}