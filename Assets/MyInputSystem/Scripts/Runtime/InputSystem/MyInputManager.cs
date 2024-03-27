#define JoyStick

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GoldSprite.UnityPlugins.MyInputSystem {
    public abstract partial class MyInputManager : MonoBehaviour {
        //引用
        protected static InputActions InputActions { get; set; }
        public static MyInputManager Instance { get; private set; }
        private Dictionary<InputActionMap, bool> InputEnables;

        //配置
        [ShowInputManager]
        [SerializeField]
        private string draw;
        public bool debugLog = true;
        //实时
        private Dictionary<Delegate, object> actionValues = new();


        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;

            InitManager();

            InitActions();
        }

        private void InitManager()
        {
            InputActions = new InputActions();
            InputActions.Enable();

            InputEnables = SetInputActionMaps();
        }

        protected abstract Dictionary<InputActionMap, bool> SetInputActionMaps();

        protected virtual void InitActions()
        {
        }


        private void OnDisable()
        {
            InputActions.Disable();
        }

        public void RegisterActionListener<T>(InputAction keyAction, Action<T> act, bool log = false)
        {
            Action<InputAction.CallbackContext> proxy = (c) => {
                var valObj = keyAction.ReadValueAsObject();
                if (valObj == null)
                    valObj = default(T);
                T t = (T)Convert.ChangeType(valObj, typeof(T));

                var disable = !IsInputEnable(keyAction.actionMap);
                if (disable) return;  //输入禁用时返回
                actionValues[act] = t;  //自动存值
                act?.Invoke(t);

                //debug log
                if (!log && !debugLog) return;
                Debug.Log($"[InputSystem]: {keyAction.name}: {t}");
            };
            keyAction.performed += proxy;
            keyAction.canceled += proxy;
            actionValues.Add(act, null);
        }


        /// <summary>
        /// 是否启用了该输入行动表
        /// </summary>
        /// <param name="actionMap"></param>
        /// <returns></returns>
        private bool IsInputEnable(InputActionMap actionMap)
        {
            return (!InputEnables.ContainsKey(actionMap)) || InputEnables[actionMap];
        }


        /// <summary>
        /// 设置输入启用/禁用
        /// </summary>
        /// <param name="actionMap">对应行为表</param>
        /// <param name="boo">启用或禁用</param>
        public void SetInputEnable(InputActionMap actionMap, bool boo)
        {
            if (!InputEnables.ContainsKey(actionMap)) return;
            InputEnables[actionMap] = boo;
        }


        public T GetValue<T>(Action<T> act)
        {
            if (!actionValues.ContainsKey(act) || actionValues[act] == null) return default(T);
            return (T)(object)actionValues[act];
        }


    }


    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ShowInputManagerAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowInputManagerAttribute))]
    public class MyInputManagerDrawer : PropertyDrawer {
        private float height;
        private bool init;
        private Color backColor;

        public void Init()
        {
            if (init) return;

            //EditorApplication.QueuePlayerLoopUpdate();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init();
            height = 0;
            var input = ReflectionHelper.GetInputManager<MyInputManager>(property);
            if (input == null) {
                return;
            }

            var actionValues = ReflectionHelper.GetValue<MyInputManager, Dictionary<Delegate, object>>(input, "actionValues");
            if (actionValues != null && actionValues.Count > 0) {
                position.height = EditorGUIUtility.singleLineHeight;
                //Debug.Log("Draw");
                float lineMargin = 5f;

                //backColor = EditorGUI.ColorField(position, backColor);
                //backColor.a = 1;
                //height += EditorGUIUtility.singleLineHeight;
                //position.y += EditorGUIUtility.singleLineHeight + lineMargin;

                var i = 0;
                var totalHeight = (actionValues.Count) * (EditorGUIUtility.singleLineHeight + lineMargin) + lineMargin;
                Rect boxRect = new Rect(position.x, position.y, position.width, totalHeight);
                EditorGUI.DrawRect(boxRect, backColor);

                height += lineMargin;
                position.y += lineMargin;
                foreach (var (k, v) in actionValues) {
                    string label1 = k.Method.Name;
                    GUIContent labelContent = new GUIContent(label1);
                    float labelWidth = GUI.skin.label.CalcSize(labelContent).x;
                    float keyStartX = 300f;
                    float keyWidth = position.width - keyStartX;
                    {
                        var keyRect = new Rect(position.x, position.y, labelWidth, position.height);
                        var valRect = new Rect(position.x + keyStartX, position.y, keyWidth - lineMargin, position.height);
                        EditorGUI.LabelField(keyRect, label1);
                        //height += EditorGUIUtility.singleLineHeight;
                        //position.y += EditorGUIUtility.singleLineHeight;
                        EditorGUI.TextField(valRect, v == null ? "" : v.ToString());
                        if (i != actionValues.Count - 1) {
                        }
                        height += EditorGUIUtility.singleLineHeight;
                        position.y += EditorGUIUtility.singleLineHeight + lineMargin;
                        //height += EditorGUIUtility.singleLineHeight;
                        //position.y += EditorGUIUtility.singleLineHeight;
                        //EditorGUI.TextField(position, v + "");
                    }
                    i++;
                }
                height += EditorGUIUtility.singleLineHeight + lineMargin;

                EditorUtility.SetDirty(property.serializedObject.targetObject); //实时刷新重绘
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }
    }
#endif


    public class ReflectionHelper {

        public static V GetValue<T, V>(T obj, string fieldName, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var fieldInfo = typeof(T).GetField(fieldName, flags);
            if (fieldInfo == null) return default(V);
            return (V)fieldInfo.GetValue(obj);
        }

        public static T GetInputManager<T>(SerializedProperty property)
        {
            var target = property.serializedObject.targetObject;
            if (target is T t) {
                return t;
            }
            return default(T);
        }
    }


    //#if JoyStick
    //    public partial class MyInputManager : MonoBehaviour {

    //        /// <summary>
    //        /// 更新同步所有Action值
    //        /// </summary>
    //        private void Update()
    //        {
    //            if (joystick != null)
    //                UpdateJoystickValue();
    //        }


    //    /// <summary>
    //    /// 更改时<para/>
    //    /// 刷新Joystick轮盘输入
    //    /// </summary>
    //    private void UpdateJoystickValue()
    //    {
    //        var move = MoveActionValue;
    //        move.x = joystick.Horizontal;
    //        move.y = joystick.Vertical;
    //        if (oldJoystickMoveActionValue != move)
    //        {
    //            MoveActionValue = oldJoystickMoveActionValue = move;
    //            //Debug.Log("刷新摇杆值");
    //        }
    //    }

    //    private void GetJoystick()
    //    {
    //        joystick = GameObject.FindObjectOfType<Joystick>().GetComponent<Joystick>();
    //    }
    //    }
    //#endif


}