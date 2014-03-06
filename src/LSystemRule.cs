using System;
using System.Collections;

/*
 * @author: Stefan Gawrys
 * 
 * L-System rule class
 */

public class LSystemRule
{
	public LSystemRule(char index, string rule) {
		this.Index = index;
		this.Rule = rule;
	}
	
	/*
	 * The character corresponding to the associated rule.
	 */
	private char _Index;
	public char Index {
		set { this._Index = value; }
		get { return this._Index; }
	}
	
	/*
	 * The rule at a certain character index.
	 */
	private string _Rule;
	public string Rule {
		set { this._Rule = value; }
		get { return this._Rule; }
	}
}

