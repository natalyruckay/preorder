using System;
using System.Collections.Generic;
using System.Text;

namespace PreOrder
{
    class Controls
    {
        public bool IsInt32(int value)
        {
            if (value <= Int32.MaxValue && value >= Int32.MinValue)
            {
                return true;
            }
            return false;
        }

        public bool IsSign(char c)
        {
            char[] signs = { '+', '-', '/', '*', '~' };

            if (Array.Exists(signs, element => element == c))
            {
                return true;
            }
            return false;
        }

        public bool Format_control(string[] expr)
        {
            foreach (string e in expr)
            {
                if (!(char.TryParse(e, out char sgn) && IsSign(sgn)) && !(int.TryParse(e, out int n) && IsInt32(checked(n))))
                {
                    return false;
                }
            }
            return true;
        }
    }



    class Evaluate
    {
        public void Evaluation(int a, int b, char operat, Stack<int> stack)
        {
            Controls c = new Controls();
            if (c.IsInt32(checked(a)) == true && c.IsInt32(checked(b)) == true)
            {
                try
                {
                    switch (operat)
                    {
                        case '+':
                            stack.Push(checked(a + b));
                            break;
                        case '-':
                            stack.Push(checked(b - a));
                            break;
                        case '*':
                            stack.Push(checked(a * b));
                            break;
                        case '/':
                            stack.Push(checked(b / a));
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Overflow Error");
                    return;
                }
            }
            return;
        }

        public void Reverse (StringBuilder input)
        {
            char t;
            int end = input.Length - 1;
            int start = 0;

            while(end - start > 0)
            {
                t = input[end];
                input[end] = input[start];
                input[start] = t;

                start++;
                end--;
            }
        }

        public void Convert(string exp)
        {
            StringBuilder input = new StringBuilder(exp);
            Reverse(input);

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                    if (c == ' ')
                    {
                        continue;
                    }

                    if (c == '*' || c == '/' || c == '+' || c == '-' || c == '~')
                    {
                        int s1, s2;
                        if (c == '~')
                        {
                            s2 = stack.Pop();
                            s2 = -s2;
                            stack.Push(s2);
                        }
                        else
                        {
                            s1 = stack.Pop();
                            s2 = stack.Pop();

                            if (s2 == 0 && c == '/')
                            {
                                Console.WriteLine("Divide Error");
                                return;
                            }
                            else
                            {
                                Evaluation(s2, s1, c, stack);
                            }
                        }
                    }

                    else
                    {
                        
                        StringBuilder temp = new StringBuilder();

                        while (Char.IsDigit(c))
                        {
                            temp.Append(c);
                            i++;
                            c = input[i];
                        }
                        i--;

                        Reverse(temp);
                        

                        try
                        {
                            int num = int.Parse(temp.ToString());
                            stack.Push(num);
                        }
                        catch
                        {
                            Console.WriteLine("Format Error");
                            return;
                        }
                    }
            }

            if (stack.Count == 1)
            {
                int result = stack.Pop();
                Console.WriteLine(result);
            }
            else if (stack.Count > 1)
            {
                Console.WriteLine("Format Error");
            }
        }
    }



    class Program
    {
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            Controls c = new Controls();

            string[] control = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (c.Format_control(control) == true)
            {
                Evaluate p = new Evaluate();
                p.Convert(input);
            }
            else
            {
                Console.WriteLine("Format Error");
            }
        }
    }

}