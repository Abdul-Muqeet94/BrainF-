//ee https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

string result = new BrainFuck(",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.",char.ConvertFromUtf32(8) + char.ConvertFromUtf32(9)).execute();
Console.WriteLine(result);

public class BrainFuck
{

    // data
    private int i;

    private List<int> data ;
    // input tring 
    private string input = "";
    // output string 
    private string output = "";
    
    // For looping though each token
    private int c;
    
    // source code
    private string source = "";

    private Dictionary<int, int> open_bracket;
    private Dictionary<int, int> closing_brackets;
    
    // action to invoke on each token
    private Dictionary<char, Action> actions = new Dictionary<char, Action>();
    
    

    public BrainFuck(string code,string _input)
    {
        
        this.i = 0;
        // data
        this.data = new List<int>();
        this.data.Add(0);
        // output
        this.input = _input;
        this.output = "";
        // source code
        this.c = 0;
        this.source = code;
        
        open_bracket = new Dictionary<int, int>();
        closing_brackets = new Dictionary<int, int>();
        
        // Token with their methods
        actions.Add('>',move_forward);
        actions.Add('<',move_backward);
        actions.Add('+',increment);
        actions.Add('-',decrement);
        actions.Add('.',output_value);
        actions.Add(',',input_value);
        actions.Add('[',jump_forward);
        actions.Add(']',jump_backward);
    }

    
    // populates the bracket and save its location for jumps
    public void get_brackets()
    {
        Stack<int> bracket_stack = new Stack<int>(); 
        char[] code = this.source.ToCharArray();
        int i = 0;
        while (i < code.Length)
        {
            if ( code[i] == '[')
                    // push the value in stack
                    bracket_stack.Push(i);
            else if(code[i] == ']')
            {
                int temp_val=bracket_stack.Pop();
                this.open_bracket[temp_val] = i;
                this.closing_brackets[i] = temp_val;
            }
            i++;
        }

    }

    public string execute()
    {
        get_brackets();
        this.c = 0;
        List<Char> data = new List<char>();
        this.output = "";
        char [] source = this.source.ToCharArray();
        int len = source.Length;

        while (c < len)
        {
            // loop through each of the inputs
            actions[source[this.c]].Invoke();
        }
        return this.output;
    }
    
    private void move_forward()
    {
        this.i++;
        if (this.i == this.data.Count)
            this.data.Insert(this.i,0);
        this.c++;
    }

    private void move_backward()
    {
        this.i--;
        if (this.i < 0)
            this.i = 0;
        this.c++;
    }

    private void increment()
    {
        this.data[this.i]++;
        if (this.data[this.i] == 256)
            this.data[this.i] = 0;
        this.c++;
    }

    private void decrement()
    {
        this.data[this.i]--;
        if (this.data[this.i] == -1)
            this.data[this.i] = 255;
        this.c++;
    }

    private void output_value()
    {
        this.output += (char)this.data[this.i];
        this.c++;
    }

    private void input_value()
    {
        if (this.input.Length==0)
            throw new Exception("Input expected!");
        this.data.Insert(this.i,this.input[0]);
        this.input=input.Substring(1);
        this.c++;
    }

    private void jump_forward()
    {
        if (this.data[this.i]==0)
            this.c = this.open_bracket[this.c] + 1; 
        else 
            this.c++;
    }

    private void jump_backward()
    {
        if (this.data[this.i] != 0)
            this.c = this.closing_brackets[this.c] + 1;
        else
            this.c++;
    }

}

