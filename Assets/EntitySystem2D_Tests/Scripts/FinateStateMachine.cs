using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using GoldSprite.UnityPlugins.GUtils;
using GoldSprite.UnityPlugins.MyInputSystem;

namespace GoldSprite.UnityPlugins.EntitySystem2D.Tests {

    [Serializable]
    public class FinateStateMachine {
        [ShowFsm]
        [SerializeField]
        public string draw;
        public Dictionary<Type, IState> states = new();
        public IState currentState;
        public IState defaultState;

        public void InitState(IState state)
        {
            defaultState = currentState = state;
            states.Add(state.GetType(), state);
        }

        public void AddState(IState state)
        {
            states.Add(state.GetType(), state);
        }

        public void UpdateNextState()
        {
            var target = currentState;
            //遍历条件, 进入优先级最高的那个状态
            foreach (var (type, state) in states) {
                if (!EnterState(state, target)) continue;
                target = state;
            }
            if (target.Exit()) target = defaultState;  //计算完优先级后再判断返回到默认状态
            var change = currentState != target;
            if (change) OnEnterState(target);
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
            Debug.Log($"{"Fsm"}退出状态{currentState}.");
            currentState = targetState;
            currentState.OnEnter();
            Debug.Log($"{"Fsm"}进入状态{currentState}.");
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
            if(cState != null) {
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