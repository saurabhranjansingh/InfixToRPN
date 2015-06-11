using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixToRPN
{
    class Program
    {
        const int LEFT_ASSOCIATIVE = 0;
        const int RIGHT_ASSOCIATIVE = 1;
        static Dictionary<string, Operator> Operators = new Dictionary<string, Operator>();

        static void Main(string[] args)
        {
            String InfixString = " 3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3";
            Console.WriteLine("Input Expression: {0}", InfixString);
            try
            {
                InitializeOperators();
                string output = GetRPN(InfixString);
                Console.WriteLine("The input string in Reverse Polished Notation : {0}", output);
                Console.WriteLine("Finished.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occured : {0}", e.Message);
            }
            Console.ReadKey();
        }

        private static void InitializeOperators()
        {
            Operators.Add("^", new Operator { operatorCode = "^", associativity = RIGHT_ASSOCIATIVE, precedence = 2 });
            Operators.Add("%", new Operator { operatorCode = "%", associativity = LEFT_ASSOCIATIVE, precedence = 1 });
            Operators.Add("/", new Operator { operatorCode = "/", associativity = LEFT_ASSOCIATIVE, precedence = 1 });
            Operators.Add("*", new Operator { operatorCode = "*", associativity = LEFT_ASSOCIATIVE, precedence = 1 });
            Operators.Add("-", new Operator { operatorCode = "-", associativity = LEFT_ASSOCIATIVE, precedence = 0 });
            Operators.Add("+", new Operator { operatorCode = "+", associativity = LEFT_ASSOCIATIVE, precedence = 0 });
        }

        private static bool isOperator(String token)
        {
            return Operators.ContainsKey(token);
        }

        private static string GetRPN(string infixString)
        {
            try
            {
                char[] separator = { ' ' };
                string[] tokensArray = infixString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                Stack<string> operatorStack = new Stack<string>();
                StringBuilder outputQueue = new StringBuilder();

                //Read the tokens one by one
                for (int i = 0; i < tokensArray.Length; i++)
                {
                    string token = tokensArray[i];

                    //If the token is a left parenthesis (i.e. "("), then push it onto the stack.
                    if (token.Equals("("))
                    {
                        operatorStack.Push(token);
                    }
                    //If the token is a right parenthesis (i.e. ")"):
                    else if (token.Equals(")"))
                    {
                        bool LeftParenthesisFound = false;
                        //Until the token at the top of the stack is a left parenthesis, pop operators off the stack onto the output queue.
                        for (int k = 0; k < operatorStack.Count; k++)
                        {
                            if (operatorStack.Peek().Equals("("))
                            {
                                //Pop the left parenthesis from the stack, but not onto the output queue.                                
                                operatorStack.Pop();
                                LeftParenthesisFound = true;
                                break;
                            }
                            else
                            {
                                outputQueue.Append(operatorStack.Pop() + " ");
                            }
                        }

                        //If the token at the top of the stack is a function token, pop it onto the output queue.
                        // << To be implemented >>

                        //If the stack runs out without finding a left parenthesis, then there are mismatched parentheses
                        if (operatorStack.Count == 0 && LeftParenthesisFound == false)
                        {
                            throw (new Exception("Parentheses Mismatching."));
                        }
                    }
                    //If the token is an operator o1
                    else if (isOperator(token))
                    {
                        //While there is an operator token, o2, at the top of the operator stack
                        while (operatorStack.Count > 0 && isOperator(operatorStack.Peek()))
                        {
                            string tokenAtTopOfStack = operatorStack.Peek();

                            Operator o1 = Operators.Values.Where(opCode => opCode.operatorCode.Equals(token)).First();
                            Operator o2 = Operators.Values.Where(opCode => opCode.operatorCode.Equals(tokenAtTopOfStack)).First();

                            //if o1 is left-associative and its precedence is less than or equal to that of o2, or
                            //o1 is right associative, and has precedence less than that of o2,
                            if ((o1.associativity == LEFT_ASSOCIATIVE && o1.precedence <= o2.precedence) || (o1.associativity == RIGHT_ASSOCIATIVE && o1.precedence < o2.precedence))
                            {
                                //pop o2 off the operator stack, onto the output queue      
                                outputQueue.Append(operatorStack.Pop() + " ");
                                continue;
                            }
                            break;
                        }
                        //push o1 onto the operator stack.                        
                        operatorStack.Push(token);
                    }
                    //If token is a number
                    else
                    {
                        //add it to the output queue
                        outputQueue.Append(token + " ");
                    }
                }

                //When there are no more tokens to read:
                //If the operator token on the top of the stack is a parenthesis, then there are mismatched parentheses.
                if (operatorStack.Count > 0)
                {
                    if (operatorStack.Peek().Equals("(") || operatorStack.Peek().Equals(")"))
                    {
                        throw (new Exception("Parentheses Mismatching."));
                    }

                    //While there are still operator tokens in the stack:
                    while (operatorStack.Count > 0)
                    {
                        if (isOperator(operatorStack.Peek()))
                        {
                            //Pop the operator onto the output queue.
                            outputQueue.Append(operatorStack.Pop() + " ");
                        }
                        else
                        {
                            operatorStack.Pop();
                        }
                    }
                }

                return outputQueue.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
