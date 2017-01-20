using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class SceneViewWindow : EditorWindow
{
	private Vector2 _tScrollPos;

	[MenuItem( "Window/Scene View Window" )]
	static void Init()
	{
		GetWindow<SceneViewWindow>( "Scene View" );
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		_tScrollPos = EditorGUILayout.BeginScrollView( _tScrollPos, false, false );

		GUILayout.Label( "Scenes In Build", EditorStyles.boldLabel );
		for( int i = 0; i < EditorBuildSettings.scenes.Length; i++ )
		{
			EditorBuildSettingsScene tScene = EditorBuildSettings.scenes[i];
			if( tScene.enabled )
			{
				string sSceneName = Path.GetFileNameWithoutExtension( tScene.path );

				if( GUILayout.Button( i + ": " + sSceneName, new GUIStyle( GUI.skin.GetStyle( "Button" ) ) { alignment = TextAnchor.MiddleLeft } ) )
				{
					if( EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() )
					{
						EditorSceneManager.OpenScene( tScene.path );
					}
				}
			}
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}
}
