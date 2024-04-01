using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSprite.UnityPlugins.EntitySystem2D {
    public abstract class State : IState {
        public int Priority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool OnEnterEnd => throw new NotImplementedException();

        public bool Enter()
        {
            throw new NotImplementedException();
        }

        public bool Exit()
        {
            throw new NotImplementedException();
        }

        public void InitState()
        {
            InitState0();
        }

        public void OnEnter()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        protected virtual void InitState0() { }
    }
}
