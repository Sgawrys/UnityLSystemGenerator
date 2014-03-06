using UnityEngine;
using System.Collections;
/*
 * @author: Stefan Gawrys
 * 
 * What shows up inside the LSystemEditor, what sort of GUI features
 */
public class LSystemGui : MonoBehaviour
{
	public Rect windowRect;
	public string startText = "";
	public string ruleText = "";
	public string iterText = "";
	public string anglText = "";
	
	private float guiAngle = 0.0f;
	private float cameraDistance = 0.5f;
	
	void OnGUI() {
		LSystemPainter painter = (LSystemPainter)this.GetComponent("LSystemPainter");
		guiAngle = GUI.HorizontalSlider(new Rect(0,0,Screen.width,20),guiAngle,0.0F,360.0F);
		cameraDistance = GUI.VerticalSlider(new Rect(Screen.width-20,30,20,Screen.height-50), cameraDistance, 0.01f, 2.0f);
		
		Camera.main.transform.position = new Vector3(0.0f, 0.0f, -499.9f*cameraDistance);
		painter.ChangeAngle = guiAngle;
		
		windowRect = GUI.Window(0, new Rect(0,30,320,320), GenerateNewSystem, "");
	}
	
	void GenerateNewSystem(int windowID) {
		GUI.Label(new Rect(10,15,64,32), "Start :");
		startText = GUI.TextField(new Rect(64,10,240,32), startText);
		
		GUI.Label(new Rect(10,50,64,32), "Rules :");
		ruleText = GUI.TextField(new Rect(64,50,240,32), ruleText);
		
		GUI.Label(new Rect(10,85,64,32), "Iterations :");
		iterText = GUI.TextField(new Rect(104,85,200,32), iterText);
		
		GUI.Label(new Rect(10,120,64,32), "Angle :");
		anglText = GUI.TextField(new Rect(104,120,200,32), anglText);
		
		if(GUI.Button (new Rect(10,280,128,32), "Generate")) {
			LSystemPainter paint = (LSystemPainter)this.GetComponent("LSystemPainter");
			paint.reset();
			LSystemParser parse = (LSystemParser)this.GetComponent("LSystemParser");
			parse.initialize("","",startText,ruleText,int.Parse(iterText),float.Parse(anglText));
			parse.reset();	
		}
	}
}

