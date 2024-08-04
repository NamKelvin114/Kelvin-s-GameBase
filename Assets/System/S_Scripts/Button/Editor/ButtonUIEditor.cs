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

        private SerializedProperty _isPlayAudio;

        private bool _isShowSetting;
        
        private SerializedProperty _isVibrate;

        private SerializedProperty _sizePressed;

        private SerializedProperty _timeToHold;

        private SerializedProperty _audioUIType; 
        
        private SerializedProperty _musicData;
        
        private SerializedProperty _soundData;


        protected override void OnEnable()
        {
            base.OnEnable();
            _buttonUI = target as ButtonUI;
            
            _isPlayAudio = serializedObject.FindProperty("isPlayAudio");
            _isVibrate = serializedObject.FindProperty("isVibrate");
            _sizePressed = serializedObject.FindProperty("sizePressed");
            _timeToHold = serializedObject.FindProperty("timeToHold");
            _audioUIType = serializedObject.FindProperty("audioUIType");
            _musicData = serializedObject.FindProperty("musicData");
            _soundData = serializedObject.FindProperty("soundData");
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
            EditorGUILayout.PropertyField(_isPlayAudio);
            if (_isPlayAudio.boolValue)
            {
                DrawSoundButton();
            }
            EditorGUILayout.PropertyField(_isVibrate);
            EditorGUILayout.PropertyField(_sizePressed);
            EditorGUILayout.PropertyField(_timeToHold);
        }
        private void DrawSoundButton()
        {
            EditorGUILayout.PropertyField(_audioUIType);

            if (_audioUIType.enumValueIndex == (int)AudioUIType.Music)
            {
                EditorGUILayout.PropertyField(_musicData);
            }
            else if (_audioUIType.enumValueIndex == (int)AudioUIType.SoundFX)
            {
                EditorGUILayout.PropertyField(_soundData);
            }
        }
    }
#endif
}