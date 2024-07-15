using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Kelvin.ButtonUI
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ButtonUI), true)]
    [CanEditMultipleObjects]
    public class ButtonUIEditor : ButtonEditor
    {
        private ButtonUI _buttonUI;

        private SerializedProperty _isPlaySound;

        private bool _isShowSetting;

        private SerializedProperty _isVibrate;

        private SerializedProperty _sizePressed;

        private SerializedProperty _timeToHold;

        protected override void OnEnable()
        {
            base.OnEnable();
            _buttonUI = target as ButtonUI;
            _isPlaySound = serializedObject.FindProperty("isPlaySound");
            _isVibrate = serializedObject.FindProperty("isVibrate");
            _sizePressed = serializedObject.FindProperty("sizePressed");
            _timeToHold = serializedObject.FindProperty("timeToHold");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            GUILayout.Space(5);
            GUILayout.Space(5);
            _isShowSetting = EditorGUILayout.Toggle("Show Setting", _isShowSetting);
            if (_isShowSetting) DrawSetting();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        private void DrawSetting()
        {
            EditorGUILayout.PropertyField(_isPlaySound);
            EditorGUILayout.PropertyField(_isVibrate);
            EditorGUILayout.PropertyField(_sizePressed);
            EditorGUILayout.PropertyField(_timeToHold);
        }
    }
#endif
}