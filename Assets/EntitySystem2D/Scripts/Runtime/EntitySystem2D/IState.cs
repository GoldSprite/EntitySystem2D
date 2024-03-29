using System;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public interface IState {
        public int Priority { get; set; }
        bool Enter();
        bool Exit();
        void OnEnter();
        void OnExit();
        void Run();
    }
}