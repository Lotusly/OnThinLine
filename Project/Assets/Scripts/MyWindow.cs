

using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;
public class MyWindow : EditorWindow
{
	GameObject node;

	float moveSpeed;

	float tempo;

	[SerializeField]Vector3[] place; // last : pitch

	private Transform parent;

	//private int length;
	private float temposOffset;

	private string prefix;

	private Vector2 scrollPos;
	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/My Window")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
		window.Show();
	}

	[SerializeField] protected List<Vector3> nodes = new List<Vector3>();
	

	protected SerializedObject _serializedObject;

	protected SerializedProperty _assetLstProperty;


	protected void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_assetLstProperty = _serializedObject.FindProperty("place");
	}

	void OnGUI()
	{
		_serializedObject.Update();
		EditorGUI.BeginChangeCheck();
		
		

		GUILayout.BeginVertical();
		scrollPos = GUILayout.BeginScrollView(scrollPos,false,true, GUILayout.ExpandHeight(true));
		
		node = EditorGUILayout.ObjectField("Node",node,typeof(GameObject),true) as GameObject;
		parent = EditorGUILayout.ObjectField("Parent", parent, typeof(Transform), true) as Transform;
		moveSpeed = EditorGUILayout.FloatField("MoveSpeed", moveSpeed);
		tempo = EditorGUILayout.FloatField("Tempo", tempo);
		temposOffset = EditorGUILayout.FloatField("Offset", temposOffset);
		prefix = EditorGUILayout.TextField("Prefix", prefix);
		EditorGUILayout.PropertyField(_assetLstProperty, true);

		GUILayout.EndScrollView ();
		GUILayout.EndVertical();

		if (GUILayout.Button("Generate"))
		{
			Generate();
		}
		
		if (EditorGUI.EndChangeCheck())
		{
			_serializedObject.ApplyModifiedProperties();
		}
	}

	void Generate()
	{
	
		float t = temposOffset;
		int l = place.Length;
		for (int i = 0; i < l; i++)
		{
			Debug.Log("Generated");
			Vector3 position;
			position = Vector3.right * t* moveSpeed / tempo * 60+Vector3.up*place[i].z;
			t = t + place[i].x;
			//GameObject newNode = Instantiate(node, position, Quaternion.Euler(Random.value*360,Random.value*360,Random.value*360), parent);
			GameObject newNode = Instantiate(node, position, Quaternion.identity,parent);
			newNode.name = prefix+place[i].y.ToString();
			//GameObject newNode = PrefabUtility.InstantiatePrefab(node) as GameObject;
			//newNode.transform.position = position;
			//newNode.transform.parent = parent;
		}
		
	}
}
