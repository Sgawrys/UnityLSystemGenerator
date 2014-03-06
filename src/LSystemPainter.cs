using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * @author: Stefan Gawrys
 * 
 * Paints the L-System by going through the states.
 */
public class LSystemPainter : MonoBehaviour {
	
	private LinkedList<Vector3> points;
	private Stack<AnglePos> stack;
	
	private float startingAngle;
	public Vector3 currentPosition;
	public float changeAngle;
	
	private LineRenderer renderLine;
	
	public struct AnglePos {
		public float angle;
		public Vector3 position;
		
		public AnglePos(float angle, Vector3 position) {
			this.angle = angle;
			this.position = position;
		}
	}
	
	public float ChangeAngle {
		get { return this.changeAngle; }
		set { this.changeAngle = value; }
	}
	
	public float currentAngle;
	
	//Was used for drawing in the Unity Web Player.
	public Material mat;
	
	private LinkedList<LSystemState> _StateList;
	public LinkedList<LSystemState> StateList {
		set { this._StateList = value; }
		get { return this._StateList; }	
	}
	
	//On Unity Start
	void Start() {
		points = new LinkedList<Vector3>();
		points.AddFirst(new Vector3(0.0f, 0.0f, 0.0f));
		
		renderLine = (LineRenderer)this.GetComponent("LineRenderer");
		
		stack = new Stack<AnglePos>();
	}
	
	//Reset what is on screen before redrawing with new angle
	public void reset() {
		points = new LinkedList<Vector3>();
		points.AddFirst(new Vector3(0.0f, 0.0f, 0.0f));
	}
	
	//Initialization of the instance variables
	public void create(LinkedList<LSystemState> states) {
		this._StateList = states;
		this.startingAngle = this._StateList.Last.Value.Angle;
		this.changeAngle = startingAngle;
		this.stack = new Stack<AnglePos>();
		
		this.points = new LinkedList<Vector3>();
		this.points.AddFirst(new Vector3(0.0f, 0.0f, 0.0f));
		renderState(this._StateList.Last.Value);
	}

	//Used for drawing the L-System with the initial angle
	void renderState(LSystemState state) {
		foreach(char character in state.State) {
			switch(character) {
				case '+': currentAngle -= state.Angle; break;
				case '-': currentAngle += state.Angle; break;
				case '[': stack.Push(new AnglePos(currentAngle, currentPosition)); break;
				case ']': 
						AnglePos obj = stack.Pop();
						currentAngle = obj.angle;
						currentPosition = obj.position;
						break;
				default :
					createObject(); break;
			}
		}
	}
	
	//Used for drawing the L-System after the angle has been changed
	void renderState(LSystemState state, float newAngle) {
		points.Clear();
		foreach(char character in state.State) {
			switch(character) {
				case '+': currentAngle -= newAngle; break;
				case '-': currentAngle += newAngle; break;
				case '[': stack.Push(new AnglePos(currentAngle, currentPosition)); break;
				case ']':
						AnglePos obj = stack.Pop();
						currentAngle = obj.angle;
						currentPosition = obj.position;
						break;
				default :
					createObject(); break;
			}
		}
	}
	
	//Creates a point object for the previous line to be connected to.
	void createObject() {
		float rad = Mathf.Deg2Rad * currentAngle;
		float x = currentPosition.x + Mathf.Sin(rad);
		float y = currentPosition.y + Mathf.Cos(rad);
		
		currentPosition.x = x;
		currentPosition.y = y;
		
		points.AddLast(new Vector3(x,y,0.0f));
	}
	
	// Draws all the lines of the L-System on screen, if angle is changed through Unity, updates the display as well.
	void Update () {
		if(this.changeAngle != this.startingAngle) {
			this.currentAngle = 0.0f;
			this.currentPosition = new Vector3(0.0f,0.0f,0.0f);
			this.stack = new Stack<AnglePos>();
			renderState(this._StateList.Last.Value, this.changeAngle);
			this.startingAngle = this.changeAngle;
		}
		
		if(points.First != null) {
			LinkedListNode<Vector3> currentNode = points.First;
			while(currentNode.Next != null) {
				Debug.DrawLine(currentNode.Value, currentNode.Next.Value, Color.red);
				currentNode = currentNode.Next;
			}
		}

	}
	/*
	 * Was used to display the L-System through Unity Web Player without the use of Debug.DrawLine
	void OnPostRender() {
		if(points.First != null) {
			LinkedListNode<Vector3> currentNode = points.First;
			while(currentNode.Next != null) {
		        GL.PushMatrix();
				mat.SetPass(0);
		        GL.Begin(GL.LINES);
		        GL.Color(Color.red);
		        GL.Vertex(currentNode.Value);
		        GL.Vertex(currentNode.Next.Value);
		        GL.End();
		        GL.PopMatrix();
				currentNode = currentNode.Next;
			}
		}
    }*/
}
