using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphProcessor;
using UnityEditor.Callbacks;
using System.IO;
using Examples.Editor._05_All;
using Plugins.Examples.Editor.BaseGraph;

public class GraphAssetCallbacks
{
	[MenuItem("Assets/Create/GraphProcessor", false, 10)]
	public static void CreateGraphPorcessor()
	{
		var graph = ScriptableObject.CreateInstance< BaseGraph >();
		ProjectWindowUtil.CreateAsset(graph, "GraphProcessor.asset");
	}
	
	[MenuItem("Assets/Create/GraphProcessor_NP", false, 10)]
	public static void CreateGraphPorcessor_NP()
	{
		var graph = ScriptableObject.CreateInstance< NPBehaveGraph >();
		ProjectWindowUtil.CreateAsset(graph, "NPBehaveGraph.asset");
	}
	
	[MenuItem("Assets/Create/GraphProcessor_Skill", false, 10)]
	public static void CreateGraphPorcessor_Skill()
	{
		var graph = ScriptableObject.CreateInstance< SkillGraph >();
		ProjectWindowUtil.CreateAsset(graph, "SkillGraph.asset");
	}

	// N_打开NodeGraph资源文件时调用
	[OnOpenAsset(0)]
	public static bool OnBaseGraphOpened(int instanceID, int line)
	{
		Debug.Log("N_OnBaseGraphOpened！");
		var baseGraph = EditorUtility.InstanceIDToObject(instanceID) as BaseGraph;
		return InitializeGraph(baseGraph);
	}

	// N_初始化一个Graph视图窗口
	public static bool InitializeGraph(BaseGraph baseGraph)
	{
		if (baseGraph == null) return false;
		
		switch (baseGraph)
		{
			case SkillGraph skillGraph:
				EditorWindow.GetWindow<SkillGraphWindow>().InitializeGraph(skillGraph);
				break;
			case NPBehaveGraph npBehaveGraph:
				EditorWindow.GetWindow<NPBehaveGraphWindow>().InitializeGraph(npBehaveGraph);
				break;
			default:
				EditorWindow.GetWindow<FallbackGraphWindow>().InitializeGraph(baseGraph);
				break;
		}

		return true;
	}
}
