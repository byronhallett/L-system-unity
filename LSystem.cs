using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LSystem {

    // NESTED CLASSES
    class Symbol {
        private char key;
        private System.Action function;

        public Symbol(char name, System.Action function) {
            this.key = key;
            this.function = function;
        }

        public void Evaluate() {
            function();
        }
    }

    class Rule {

        public float probability;
        public List<char> rewrite;

        public Rule(float probability, string rewrite) {
            this.probability = probability;
            this.rewrite = rewrite.Replace(" ", "").ToList();

        }
    }

    // PUBLIC MEMBERS
    public LSystem() {
        this.symbols = new Dictionary<char, Symbol>();
        this.rules = new Dictionary<char, List<Rule>>();
        this.system = new List<char>();
    }

    public void AddSymbol(char key, System.Action function) {
        symbols.Add(key, new Symbol(key, function));
    }

    public void AddRule(char symbol, float probability, string rewrite) {
        if (!rules.ContainsKey(symbol)) {
            rules.Add(symbol, new List<Rule>());
        }
        Rule rule = new Rule(probability, rewrite);
        foreach (var c in rule.rewrite) {
            if (!symbols.ContainsKey(c)){
                throw new KeyNotFoundException("Rule may insert a non existent symbol");
            }
        }
        rules[symbol].Add(rule);
    }

    public bool SetSystem(string symbols) {
        foreach (var s in symbols) {
            if (symbols.Contains(s)) continue;
            return false;
        }
        system = symbols.ToList();
        return true;
    }

    public void EvolveSystem(int numGenerations){
        for (int i = 0; i < numGenerations; i++) {
            evolve();
        }
    }

    public string GetSystemString() {
        return new string(system.ToArray());
    }

    public void EvaluateSystem() {
        foreach (var key in system) {
            Symbol toEval = symbols[key];
            toEval.Evaluate();
        }
    }

    // PRIVATE MEMBERS
    private void evolve() {
        List<char> newSystem = new List<char>();
        foreach (var symbol in system) {

            if (rules.ContainsKey(symbol)) {
                List<Rule> myRules = rules[symbol];
                float mySum = myRules.Sum((arg) => arg.probability);
                float r = Random.Range(0, mySum);
                Rule selectedRule = myRules.Find((obj) => { r-=obj.probability; return r<0; });
                newSystem.AddRange(selectedRule.rewrite);
            } else {
                newSystem.Add(symbol);
            };
        }
        system = newSystem;
    }

    private Dictionary<char, Symbol> symbols;
    private Dictionary<char, List<Rule>> rules;
    private List<char> system;

}
