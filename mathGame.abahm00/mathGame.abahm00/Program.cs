using System.Diagnostics;

string? readValue;
int correctAnswers = 0;
List<string> listOfGamesPlayed = new List<string>();
bool exited = false;
Stopwatch stopWatch = new Stopwatch();

while (!exited)
{
    PlayGame();
}

void PlayGame()
{
    correctAnswers = 0;
    stopWatch.Reset();

    Console.Clear();

    Console.WriteLine("Press 1 if you want to play game.");
    Console.WriteLine("Press 2 if you want to show games history.");

    int inputNum = 0;
    while (inputNum != 1 && inputNum != 2)
    {
        inputNum = CheckInputNumberValidity();
    }

    if (inputNum == 1)
    {
        Console.WriteLine("Enter number of questions between 5 and 10.");
        int questionNumber = CheckQuestionNumberValidity();

        Console.WriteLine("Enter type of operation you want. (+, -, *, /)");
        char operation = CheckOperationValidity();

        stopWatch.Start();

        for (int i = 0; i < questionNumber; i++)
        {
            Console.Clear();
            GenerateQuestion(operation);
        }

        stopWatch.Stop();

        TimeSpan timeSpan = stopWatch.Elapsed;

        Console.WriteLine($"You got {correctAnswers} answers correct.");
        Console.WriteLine($"You finished in {timeSpan:mm\\:ss}");
        AddGameToList(operation, questionNumber, timeSpan);

        Console.WriteLine("Press any key to play again.");
        Console.WriteLine("Press e to exit.");
    }
    else
    {
        ShowGameList();
    }

    if (Console.ReadKey().KeyChar == 'e')
    {
        exited = true;
    }
}

void AddGameToList(char operation, int questionNumber, TimeSpan timeSpan)
{
    string operationString = "";
    switch (operation)
    {
        case '+':
            operationString = "Addition";
            break;
        case '-':
            operationString = "Subtraction";
            break;
        case '*':
            operationString = "Multiplication";
            break;
        case '/':
            operationString = "Division";
            break;
    }

    listOfGamesPlayed.Add($"{DateTime.UtcNow} - {operationString} - {correctAnswers} correct answers out of {questionNumber} " +
        $"it took you {timeSpan:mm\\:ss}");
}

void ShowGameList()
{
    if (listOfGamesPlayed.Count == 0)
    {
        Console.WriteLine("There no games in the list.");
    }

    foreach (string game in listOfGamesPlayed)
    {
        Console.WriteLine(game);
    }

    Console.WriteLine("Press any key to continue.");
}

void GenerateQuestion(char operation)
{
    Random random = new Random();
    int firstNum;
    int secondNum;

    if (operation == '/')
    {
        secondNum = random.Next(1, 11);
        firstNum = random.Next(1, 11) * secondNum;
    }
    else
    {
        firstNum = random.Next(1, 101);
        secondNum = random.Next(1, 101);
    }

    Console.Write($"{firstNum} {operation} {secondNum} = ");
    CheckIfAnswerCorrect(firstNum, secondNum, operation);
}

void CheckIfAnswerCorrect(int firstNum, int secondNum, char operation)
{
    int numberInput = CheckInputNumberValidity(firstNum, secondNum, operation);

    switch (operation)
    {
        case '+':
            if (numberInput == firstNum + secondNum)
            {
                correctAnswers++;
                Console.WriteLine("Correct answer press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Wrong answer press any key to continue.");
                Console.ReadKey();
            }
            break;
        case '-':
            if (numberInput == firstNum - secondNum)
            {
                correctAnswers++;
                Console.WriteLine("Correct answer press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Wrong answer press any key to continue.");
                Console.ReadKey();
            }
            break;
        case '*':
            if (numberInput == firstNum * secondNum)
            {
                correctAnswers++;
                Console.WriteLine("Correct answer press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Wrong answer press any key to continue.");
                Console.ReadKey();
            }
            break;
        case '/':
            if (numberInput == firstNum / secondNum)
            {
                correctAnswers++;
                Console.WriteLine("Correct answer press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Wrong answer press any key to continue.");
                Console.ReadKey();
            }
            break;
    }
}

int CheckInputNumberValidity(int firstNum = 0, int secondNum = 0, char operation = '.')
{
    int numberInput;

    while (true)
    {
        readValue = Console.ReadLine();
        if (readValue != null && int.TryParse(readValue, out numberInput))
        {
            return numberInput;
        }
        else
        {
            Console.WriteLine("The number you entered is not valid.");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
            Console.Clear();
            if (operation != '.')
            {
                Console.Write($"{firstNum} {operation} {secondNum} = ");
            }
        }
    }
}

char CheckOperationValidity()
{
    while (true)
    {
        char operation = Console.ReadKey().KeyChar;
        Console.WriteLine();

        if (operation == '+' || operation == '-' || operation == '/' || operation == '*')
        {
            return operation;
        }
        else
        {
            Console.WriteLine("Invalid operation try again.");
        }
    }
}

int CheckQuestionNumberValidity()
{
    int questionNumber;

    while (true)
    {
        readValue = Console.ReadLine();
        if (readValue != null && int.TryParse(readValue, out questionNumber))
        {
            if (questionNumber >= 5 && questionNumber <= 10)
            {
                return questionNumber;
            }
            else
            {
                Console.WriteLine($"The number {questionNumber} is out of index.");
                Console.WriteLine("Enter number of questions between 1 and 10.");
            }
        }
        else
        {
            Console.WriteLine("The number you entered is not valid.");
            Console.WriteLine("Enter number of questions between 1 and 10.");
        }
    }
}