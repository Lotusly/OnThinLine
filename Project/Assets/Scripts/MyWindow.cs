

using UnityEngine;
using UnityEditor;
public class MyWindow : EditorWindow
{
	GameObject node;

	float moveSpeed;

	float tempo;

	Vector2[] place; // last : pitch

	private Transform parent;
	
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/My Window")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
		window.Show();
	}

	void OnGUI()
	{
		/*GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle("Toggle", myBool);
		myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup();*/
		node = EditorGUILayout.ObjectField("Node",node,typeof(GameObject),true) as GameObject;
		moveSpeed = EditorGUILayout.FloatField("MoveSpeed", moveSpeed);
		tempo = EditorGUILayout.FloatField("Tempo", tempo);
		//EditorGUILayout.PropertyField()

	}

	void Generate()
	{
		float t = 0;
		int l = place.Length;
		for (int i = 0; i < l; i++)
		{
			Vector3 position;
			position = Vector3.right * t* moveSpeed / tempo * 60;
			t = t + place[i].x;
			Instantiate(node, position, Quaternion.identity, parent);
		}
	}
}
