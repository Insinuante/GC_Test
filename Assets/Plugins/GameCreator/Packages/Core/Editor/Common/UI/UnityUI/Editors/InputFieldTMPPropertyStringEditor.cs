using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.UnityUI;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common.UnityUI
{
    [CustomEditor(typeof(InputFieldTMPPropertyString))]
    public class InputFieldTMPPropertyStringEditor : TMP_InputFieldEditor
    {
        private const string USS_PATH = EditorPaths.COMMON + "UI/UnityUI/StyleSheets/InputField";

        private const string NAME_ROOT = "GC-UI-InputField-Root";
        private const string NAME_HEAD = "GC-UI-InputField-Head";
        private const string NAME_BODY = "GC-UI-InputField-Body";
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement { name = NAME_ROOT };
            VisualElement head = new VisualElement { name = NAME_HEAD };
            VisualElement body = new VisualElement { name = NAME_BODY };
            
            StyleSheet[] sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet sheet in sheets) root.styleSheets.Add(sheet);
            
            root.Add(head);
            root.Add(body);
            
            IMGUIContainer buttonIMGUI = new IMGUIContainer(this.OnInspectorGUI);
            head.Add(buttonIMGUI);

            SerializedProperty fromSource = this.serializedObject.FindProperty("m_SetFromSource");
            SerializedProperty onChangeSet = this.serializedObject.FindProperty("m_OnChangeSet");

            PropertyField fieldFromSource = new PropertyField(fromSource);
            PropertyField fieldOnChangeSet = new PropertyField(onChangeSet);
            
            body.Add(fieldFromSource);
            body.Add(fieldOnChangeSet);

            return root;
        }

        // CREATE: --------------------------------------------------------------------------------

        [MenuItem("GameObject/Game Creator/UI/Input Field - TextMeshPro", false, 0)]
        public static void CreateElement()
        {
            GameObject canvas = UnityUIUtilities.GetCanvas();
            
            TMP_DefaultControls.Resources resources = UnityUIUtilities.GetTMPStandardResources();
            GameObject inputGO = TMP_DefaultControls.CreateInputField(resources);
             
            inputGO.transform.SetParent(canvas.transform, false);
            inputGO.layer = UIUtils.LAYER_UI;

            TMP_InputField input = inputGO.GetComponent<TMP_InputField>();
            Graphic targetGraphic = input.targetGraphic;
            Graphic placeholderComponent = input.placeholder;
            TMP_Text textComponent = input.textComponent;

            DestroyImmediate(input);
            
            input = inputGO.AddComponent<InputFieldTMPPropertyString>();
            input.targetGraphic = targetGraphic;
            input.placeholder = placeholderComponent;
            input.textComponent = textComponent;
            
            Undo.RegisterCreatedObjectUndo(inputGO, $"Create {inputGO.name}");
            Selection.activeGameObject = inputGO;
        }
    }
}