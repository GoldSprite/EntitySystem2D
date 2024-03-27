using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using GoldSprite.UnityPlugins.GUtils;
using GoldSprite.UnityPlugins.MyInputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    [Serializable]
    public class FinateStateMachine {
        public bool debugLog;
        [ShowFsm]
        [SerializeField]
        public string draw;
        public Dictionary<Type, IState> states = new();
        public IState currentState;
        public IState defaultState;
        private int LastPriority;

        public void InitState(IState state)
        {
            defaultState = currentState = state;
            AddState(state, 0);
            OnEnterState(state);
        }

        public void AddState(IState state, int priotity)
        {
            states.Add(state.GetType(), state);
            LastPriority += priotity;
            state.Priority = priotity;
        }

        public void UpdateNextState()
        {
            FDebug("尝试转换状态...");
            var target = currentState;
            //遍历条件, 进入优先级最高的那个状态
            foreach (var (type, state) in states) {
                if (!EnterState(state, target)) continue;
                target = state;
            }
            if (target.Exit()) target = defaultState;  //计算完优先级后再判断返回到默认状态
            var change = currentState != target;
            if (change) {
                OnEnterState(target);
                FDebug("转换成功到: " + target);
            }
        }

        protected bool EnterState(IState state) { return EnterState(state, currentState); }
        protected bool EnterState(IState state, IState cState)
        {
            if (state == cState || cState.Priority > state.Priority) return false;
            return state.Enter();
        }

        protected void OnEnterState(IState targetState)
        {
            currentState.OnExit();
            FDebug($"{"Fsm"}退出状态: {currentState}.");
            currentState = targetState;
            FDebug($"{"Fsm"}进入状态: {currentState}.");
            currentState.OnEnter();
        }

        public void Update()
        {
            currentState.Run();

            if (currentState.Exit())
                UpdateNextState();
        }

        public void FDebug(string msg)
        {
            if (!debugLog) return;
            Debug.Log(msg);
        }

        public T GetState<T>()
        {
            if (states.TryGetValue(typeof(T), out IState val)) return (T)(object)val;
            return default(T);
        }
    }


    public class ShowFsmAttribute : PropertyAttribute { }


#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowFsmAttribute))]
    public class FsmDrawer : PropertyDrawer {
        private float height;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Application.isPlaying) return;
            height = 0;
            var target = property.serializedObject.targetObject;
            var fsm = ReflectionHelper.GetField<FinateStateMachine>(target);
            if (fsm == null) {
                return;
            }

            var cState = fsm.currentState;
            if (cState != null) {
                var lineMargin = 5f;
                position.height = EditorGUIUtility.singleLineHeight;
                height += EditorGUIUtility.singleLineHeight + lineMargin;
                height += lineMargin;
                position.y += lineMargin;

                var cStateStr = cState.GetType().Name;
                EditorGUI.TextField(position, cStateStr);

                //height += lineMargin;
                EditorUtility.SetDirty(target);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
    }
#endif
}