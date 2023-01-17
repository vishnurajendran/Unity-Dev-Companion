using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DevCompanion
{
    public class DevCompanionEditorWindow : EditorWindow
    {
        private const string MODEL = "text-davinci-003";
        
        [MenuItem("Window/Dev Companion")]
        private static void OpenCompanion()
        {
            var window = EditorWindow.GetWindow<DevCompanionEditorWindow>();
            window.titleContent = new GUIContent() { text = "Dev Companion" };
            window.Show();
        }

        private string query = string.Empty;
        private string answer = string.Empty;
        private Vector2 scrollPos;

        private string bttnText = "Submit";
        
        private void OnEnable()
        {
            EditorApplication.update += Update;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        private void Update()
        {
            Repaint();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.HelpBox("Dev companion powered by ChatGPT, ask away get your queries answered", MessageType.Info);
            EditorGUILayout.Space(20);
            query = EditorGUILayout.TextArea(query, GUILayout.Height(50));
            
            if (GUILayout.Button(bttnText))
            {
                MakeRequest();
            }
            
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox("Results will popup here", MessageType.Info);
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();

            GUI.enabled = !string.IsNullOrEmpty(answer);
            if (GUILayout.Button("Copy to Clipboard"))
            {
                EditorGUIUtility.systemCopyBuffer = answer;
            }
            
            if (GUILayout.Button("Save to file"))
            {
                SaveToFile();
            }
            
            EditorGUILayout.EndHorizontal();
            
            GUI.enabled = true;
            if (!string.IsNullOrEmpty(answer))
            {
                GUIStyle style = new GUIStyle(EditorStyles.textArea);
                style.wordWrap = true;

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MinHeight(this.position.height - 350));
                answer = EditorGUILayout.TextArea(answer, style,GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
            }
        }

        private void SaveToFile()
        {
            var path = EditorUtility.SaveFilePanel("Dev Companion File Save", "Assets", "chat-gpt output", "*.*");
            if (string.IsNullOrEmpty(path))
                return;
            File.WriteAllText(path, answer);
            EditorUtility.DisplayDialog("File Saved", $"File Saved to {path}","Ok");
        }
        
        private async void MakeRequest()
        {
            Debug.Log($"Requesting {query}");
            bttnText = "Waiting for response";
            answer = await GPTApi.MakeRequest(query);
            answer = answer.Trim();
            bttnText = "Submit";
        }
    }
}
