using UnityEngine;
using UnityEditor;
using System.Collections;
/*
 * @author: Stefan Gawrys
 * 
 * Quick and dirty wizard for allowing the user to enter L-System through Unity
 * 
 */
public class LSystemEditor : ScriptableWizard {
	
	static Camera cam;
	
	public string variables = "";
	public string constants = "";
	public string start = "";
	public string rules = "";
	public int iteration = 0;
	public float angle = 0.0f;
	
	public bool guiOn = false;
	
	[MenuItem("GameObject/Create Other/L-System...")]
	static void CreateWizard () {
		cam = Camera.current;
		ScriptableWizard.DisplayWizard("Create L-System", typeof(LSystemEditor));
	}
	
	void OnWizardCreate() {
		GameObject lsys = new GameObject("L-System");
		lsys.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		
		LSystemParser parse = (LSystemParser)lsys.AddComponent(typeof(LSystemParser));
		LSystemPainter render = (LSystemPainter)lsys.AddComponent(typeof(LSystemPainter));
		
		if(guiOn) {
			LSystemGui gui = (LSystemGui)lsys.AddComponent(typeof(LSystemGui));	
		}
		
		parse.initialize(variables, constants, start, rules, iteration, angle);
		
	}
}