## L-System class for unity

### Usage:
```csharp
LSystem l = new LSystem();
// Add a symbol for every constant and variable.
// Each symbol must be bound to a function to give it formal functionality
// Each function is a c# Action, which is a delegate with no params and no return
l.AddSymbol('A', GoForward);
l.AddSymbol('B', GoForward);
l.AddSymbol('+', Turn);
l.AddSymbol('-', Turn);
// Add rewrite rules to use in evolution
// key, probablity, rewrite string
l.AddRule('A', 0.5f, "B - A - B");
l.AddRule('A', 0.5f, "B - B - B");
l.AddRule('B', 1.0f, "A + B + A");
// Choose a starting system state (can be string)
l.SetSystem("A");
// Evolve the system by n generations
// This will change the systms internal state
l.EvolveSystem(3);
// Print string if desired, while designing pattern
// print(l.GetSystemString());
// Call the functions bound to each symbol in the current system state
l.EvaluateSystem();
```
