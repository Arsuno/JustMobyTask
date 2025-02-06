using _Project.Scripts.Configs;
using _Project.Scripts.Figure.Factories;
using _Project.Scripts.Scroller;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private FigureFactory _factory;
        [SerializeField] private GameConfigScriptableObject _gameConfigScriptableObject;
        //[SerializeField] private Canvas _gameCanvas;
        [SerializeField] private FigureScroller _scroller;
        
        public override void InstallBindings()
        {
            Container.Bind<FigureFactory>().FromInstance(_factory).AsSingle();
            Container.Bind<GameConfig>().FromInstance(_gameConfigScriptableObject.GameConfig).AsSingle();
            Container.Bind<FigureScroller>().FromInstance(_scroller).AsSingle();
            //Container.Bind<Canvas>().FromInstance(_gameCanvas).AsSingle();
        }
    }
}