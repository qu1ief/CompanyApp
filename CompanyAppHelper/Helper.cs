namespace CompanyAppHelper;

public static  class Helper
{

    public static void ChangeTextColor(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}