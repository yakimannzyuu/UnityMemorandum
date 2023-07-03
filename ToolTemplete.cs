// #if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ToolTemplete : EditorWindow
{
    const string MENU_ITEM_NAME = "Tools/Templete";
    List<EditorWindowUtil.MessageContent> messages;

    [MenuItem(MENU_ITEM_NAME)]
    static void Open()// UnityEditor
    {
        var window = GetWindow<ToolTemplete>(typeof(ToolTemplete));
        window.Init();
    }

    void OnGUI()// EditorWindow
    {
        GUI_Main();
        foreach(EditorWindowUtil.MessageContent content in messages)
            EditorGUILayout.HelpBox(content.Text, content.Type);
    }

    void OnDestroy()// EditorWindow
    {
        Save();
    }

#region MainTool
    const string SAVE_KEY = "Tools_Templete";
    string text;
    bool openButton;
    void Init()
    {
        messages = new List<EditorWindowUtil.MessageContent>();
        Show();
        Load();
    }

    void GUI_Main()
    {
        text = GUILayout.TextArea(text);

        GUILayout.Space(16);

        if(EditorWindowUtil.GUI_FoldOut("Buttons", ref openButton))
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if(GUILayout.Button("A"))
                {
                    EditorWindowUtil.AddMessage(messages, new EditorWindowUtil.MessageContent(1, "A", MessageType.Info));
                    Debug.Log("A" + text);
                }
                if(GUILayout.Button("B"))
                {
                    EditorWindowUtil.AddMessage(messages, new EditorWindowUtil.MessageContent(1, "B", MessageType.Info));
                    Debug.Log("B");
                }
                if(GUILayout.Button("C"))
                {
                    EditorWindowUtil.AddMessage(messages, new EditorWindowUtil.MessageContent(1, "C", MessageType.Info));
                    Debug.Log("C");
                }
            }
        }
    }

    void Save()
    {
        EditorUserSettings.SetConfigValue(SAVE_KEY, text);
    }

    void Load()
    {
        text = EditorUserSettings.GetConfigValue(SAVE_KEY);
    }
#endregion
}

public static class EditorWindowUtil
{
    public static bool GUI_FoldOut(string title, ref bool display)
    {
        var style = new GUIStyle("ShurikenModuleTitle")
        {
            font = new GUIStyle(EditorStyles.label).font,
            border = new RectOffset(15, 7, 4, 4),
            fixedHeight = 22,
            contentOffset = new Vector2(20f, -2f)
        };
        
        var rect = GUILayoutUtility.GetRect(16f, 22f, style);
        GUI.Box(rect, title, style);

        var e = Event.current;
        var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
        if(e.type == EventType.Repaint)
            EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
            
        if(e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            display = !display;
            e.Use();
        }

        return display;
    }

    public static void AddMessage(List<MessageContent> list, MessageContent content)
    {
        DeleteMessage(list, content.ID);
        list.Add(content);
    }
    public static void DeleteMessage(List<MessageContent> list, int id)
    {
        list.RemoveAll(value => value.ID == id);
    }
    public struct MessageContent
    {
        public int ID;
        public string Text;
        public MessageType Type;

        public MessageContent(int id, string text, MessageType type)
        {
            ID = id;
            Text = text;
            Type = type;
        }
    }
}