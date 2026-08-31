string secretWord ="sunshine";
string guess="";
int guessCount=0;
int guesslimit=3;
bool outofguess=false;
while(guess != secretWord && !outofguess)
{
    if (guessCount < guesslimit)
    {
      Console.Write("Enter the guess: ");
    guess=Console.ReadLine() ?? "";
    guessCount++;  
    }
    else
    {
        outofguess=true;
    }

    
}
if (outofguess)
{
    Console.Write("you loose!");

}else{
    Console.Write("you win!!");
}

// exponent method


static int getpow(int baseNum, int powNum)
{
    int result=1;
    for(int i = 0; i < powNum; i++)
    {
        result=result*baseNum;
    }
    return result;
}

Console.Write(getpow(2,3));

// exception handling

try
{
    Console.Write("Enter a number: ");
    int num1=Convert.ToInt32(Console.ReadLine());
     Console.Write("Enter a number: ");
    int num2=Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(num1/num2);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    Console.WriteLine("will always execute!");
}