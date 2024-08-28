using System;
using System.Collections.Generic;

[Serializable]
public class RootObject
{
    public State state = new State();
}

[Serializable]
public class State
{
    public List<Products> @new = new List<Products>();
    public List<Products> used = new List<Products>();
    public List<Products> junk = new List<Products>();
}

[Serializable]
public class Products 
{
    public int id;
    public string name;
    public string category;
    public float cost;
    public int quantity;
    public string state;

}
