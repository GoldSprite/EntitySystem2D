using System;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {
    public interface IState {
        string Name { get; }
        public int Priority { get; }
        bool Enter();
        bool Exit();
        void OnEnter();
        void OnExit();
        void Run();
    }
}