using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {
	
	private static GameObject last;
	private static Shader old_shader;

	
	public static void highlight(GameObject obj, Color color)
	{
		if(obj == null)
		{
			if(last != null)
				last.renderer.material.shader = old_shader;	
			
			return;
		}
		
		GameObject obj_with_renderer = findRendererRecursive(obj);
				
		if(last != null)
		{
			last.renderer.material.shader = old_shader;	
		}
		
		old_shader = obj_with_renderer.renderer.material.shader;
		last = obj_with_renderer;

		obj_with_renderer.renderer.material.shader = highlightShader();
		obj_with_renderer.renderer.material.SetColor("_OutlineColor", color);

	}
	
	private static Shader highlightShader()
	{
		Shader ret = Shader.Find( "Toon/Basic Outline" );
		
		return ret;
	}
	
		
	private static GameObject findRendererRecursive(GameObject parent)
	{
		if(parent.renderer != null)
		{
			return parent;
		}
		
		foreach(Transform child in parent.transform)
		{
			GameObject ret = findRendererRecursive(child.gameObject);
			if(ret != null)
				return ret;
		}
		
		return null;
	}
}
