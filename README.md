# Infix To Postfix (RPN) in C# .NET
Converting Infix to Reverse Polish Notation using shunting-yard algorithm.

Reverse Polish notation (RPN) is a mathematical notation in which every operator follows all of its operands. It is also known as <b>postfix notation</b> and is normally parenthesis-free. To know more about this notation please see the wikipedia article - http://en.wikipedia.org/wiki/Reverse_Polish_notation

In computer science, the shunting-yard algorithm is a method for parsing mathematical expressions specified in infix notation. Infix notation is the notation commonly used in arithmetical and logical formulae and statements. This algorithm can be used to produce output in Reverse Polish notation (RPN).

<b>The algorithm in detail</b>:
(Source: http://en.wikipedia.org/wiki/Shunting-yard_algorithm)
<li>While there are tokens to be read:</li>
</ul>
<dl>
<dd>
<ul>
<li>Read a token.</li>
<li>If the token is a number, then add it to the output queue.</li>
<li>If the token is a function token, then push it onto the stack.</li>
<li>If the token is a function argument separator (e.g., a comma):</li>
</ul>
<dl>
<dd>
<ul>
<li>Until the token at the top of the stack is a left parenthesis, pop operators off the stack onto the output queue. If no left parentheses are encountered, either the separator was misplaced or parentheses were mismatched.</li>
</ul>
</dd>
</dl>
<ul>
<li>If the token is an operator, o<sub>1</sub>, then:</li>
</ul>
<dl>
<dd>
<ul>
<li>while there is an operator token, o<sub>2</sub>, at the top of the operator stack, and either</li>
</ul>
<dl>
<dd>
<dl>
<dd>o<sub>1</sub> is <i>left-associative</i> and its <i>precedence</i> is less than or equal to that of o<sub>2</sub>, or</dd>
<dd>o<sub>1</sub> is <i>right associative</i>, and has <i>precedence</i> less than that of o<sub>2</sub>,</dd>
</dl>
</dd>
<dd>then pop o<sub>2</sub> off the operator stack, onto the output queue;</dd>
</dl>
<ul>
<li>push o<sub>1</sub> onto the operator stack.</li>
</ul>
</dd>
</dl>
<ul>
<li>If the token is a left parenthesis (i.e. "("), then push it onto the stack.</li>
<li>If the token is a right parenthesis (i.e. ")"):</li>
</ul>
<dl>
<dd>
<ul>
<li>Until the token at the top of the stack is a left parenthesis, pop operators off the stack onto the output queue.</li>
<li>Pop the left parenthesis from the stack, but not onto the output queue.</li>
<li>If the token at the top of the stack is a function token, pop it onto the output queue.</li>
<li>If the stack runs out without finding a left parenthesis, then there are mismatched parentheses.</li>
</ul>
</dd>
</dl>
</dd>
</dl>
<ul>
<li>When there are no more tokens to read:</li>
</ul>
<dl>
<dd>
<ul>
<li>While there are still operator tokens in the stack:</li>
</ul>
<dl>
<dd>
<ul>
<li>If the operator token on the top of the stack is a parenthesis, then there are mismatched parentheses.</li>
<li>Pop the operator onto the output queue.</li>
</ul>
</dd>
</dl>
</dd>
</dl>
<ul>
<li>Exit.</li>
</ul>
