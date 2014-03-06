using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
/*
 * @author: Stefan Gawrys
 * 
 * Parses through what rules and start state the user has entered and expands it.
 */
public class LSystemParser : MonoBehaviour {
	
	public readonly char RULE_SPLITTER = ',';
	public readonly char RULE_ASSIGNER = '>';
	
	public string variables = "";
	public string constants = "";
	public string start = "";
	public string rules = "";
	public int iterations = 0;
	public float angle;
	
	private List<LSystemRule> ruleList;
	private LinkedList<LSystemState> stateList;
	private LSystemState state;
	
	
	// Use this for initialization
	void Start () {
		
		ruleList = new List<LSystemRule>();
		stateList = new LinkedList<LSystemState>();
		
		/*Set the initial state of the rule system*/
		state = new LSystemState(angle, 0, start);
		stateList.AddFirst(state);
		
		this.RuleAggregation();
		this.IterateState();
		 
		
		LSystemPainter painter = (LSystemPainter)this.GetComponent("LSystemPainter");
		painter.create(stateList);
	}
	
	public void reset() {
		ruleList = new List<LSystemRule>();
		stateList = new LinkedList<LSystemState>();
		
		/*Set the initial state of the rule system*/
		state = new LSystemState(angle, 0, start);
		stateList.AddFirst(state);
		
		this.RuleAggregation();
		this.IterateState();
		 
		
		LSystemPainter painter = (LSystemPainter)this.GetComponent("LSystemPainter");
		painter.create(stateList);
		
	}
	
	//Splits the rules based on whatever rule splitter is defined as, then the multiple strings are initialized as LSystemRules.
	public void RuleAggregation() {
		string[] splitRules = rules.Split(RULE_SPLITTER);
		LSystemRule rule;
		foreach(string splitString in splitRules) {
			string[] splitAssign = splitString.Split(RULE_ASSIGNER);
			for(int i = 0; i < splitAssign.Length; i+=2) {
				rule = new LSystemRule(splitAssign[i][0], splitAssign[i+1]);
				ruleList.Add(rule);
			}
		}
	}
	
	//Creates the L-System states using the list of known rules by expanding each character with the use of StringBuilder
	public void IterateState() {
		LSystemState currentState;
		LSystemRule currentRule;
		Predicate<LSystemRule> ruleFinder;
		StringBuilder sb = new StringBuilder();
		
		for(int i = 1; i <= iterations; i++) {
			currentState = stateList.Last.Value;
			sb.Length = 0;
			foreach(char character in currentState.State) {
				ruleFinder = delegate(LSystemRule x){ return x.Index == character; };
				currentRule = ruleList.Find(ruleFinder);
				if(currentRule != null) {
					sb.Append(currentRule.Rule);
				}else{
					sb.Append (character);
				}
			}
			currentState = new LSystemState(angle, i, sb.ToString());
			stateList.AddLast(currentState);
		}
	}
	
	
	//Initialization of this parser.
	public void initialize(string vars, string consts, string strt,
		string lrules, int iters, float angl) {
		this.iterations = iters;
		this.variables = vars;
		this.constants = consts;
		this.start = strt;
		this.rules = lrules;
		this.angle = angl;
	}
}
