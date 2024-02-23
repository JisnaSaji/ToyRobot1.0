using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml.Schema;

public interface Robot
{
    public void Place(int NewX, int NewY, string NewF);
    public void Move();
    public void Left();
    public void Right();
    public void Report();
    //public bool ValidateCoordinate(int X, int Y);

}

public class ToyRobot : Robot
{
    private int X { get; set; }                          //represent the x coodinate.
    private int Y { get; set; }                          //repesent the y coordinate.
    private string F { get; set; }                     //repesent the facing direction.

    private static readonly int XLimit = 5;

    private static readonly int YLimit = 5;


    //constructor used to set the default location and the grid size of the ToyRobot.
    public ToyRobot()                                 
    {
        X = 0;
        Y = 0;
        F = "NORTH";
        //this.XCord = XCord;
        //this.YCord = YCord;
    }
    public void Place(int NewX, int NewY, string NewF)
    {
        if (ValidateCoordinate(NewX, NewY) && ValidateDirection(NewF))
        {
            this.X = NewX;
            this.Y = NewY;
            this.F = NewF;
            Console.WriteLine("Placed Toy Robot at the new location");
        }
        else
        {
            Console.WriteLine("Invalid PLACE command. Toy Robot not placed.");
        }
    }

    public void Move()
    {
        var NewX = this.X;
        var NewY = this.Y;
        switch (this.F)
        {
            case "NORTH":
                NewY++;
                break;
            case "EAST":
                NewX++;
                break;
            case "SOUTH":
                NewY--;
                break;
            case "WEST":
                NewX--;
                break;
        }
        if (ValidateCoordinate(NewX, NewY))
        {
           this.X = NewX;
           this.Y = NewY;
        }
        else
        {
            Console.WriteLine("Invalid MOVE command. Toy Robot did not move.");
        }
        
    }

    public void Left()
    {
        switch (this.F)
        {
            case "NORTH":
                this.F = "WEST";
                break;
            case "EAST":
                this.F = "NORTH";
                break;
            case "SOUTH":
                this.F = "EAST";
                break;
            case "WEST":
                this.F = "SOUTH";
                break;
        }
    }

    public void Right()
    {
        switch (this.F)
        {
            case "NORTH":
                this.F = "EAST";
                break;
            case "EAST":
                this.F = "SOUTH";
                break;
            case "SOUTH":
                this.F = "WEST";
                break;
            case "WEST":
                this.F = "NORTH";
                break;
        }
    }

    public void Report()
    {
        Console.WriteLine(this.X + "," + this.Y + "," + this.F);
    }

    static bool ValidateCoordinate(int XCord, int YCord)
    {
        return XCord >= -5 && XCord <= XLimit && YCord >= -5 && YCord <= YLimit;
    }

    static bool ValidateDirection(string Direction)
    {
        string[] validDirections = { "NORTH", "EAST", "SOUTH", "WEST" };
        return validDirections.Contains(Direction, StringComparer.OrdinalIgnoreCase);
    }
}

class Program
{
    static void Main(string[] args)
    {
        ToyRobot Kay = new ToyRobot();

        Console.WriteLine("Hello, it's me, Kay, your ToyRobot1.0. I'm all set and ready to begin moving!\nMy coordinates are currently set to the default value (0,0) NORTH. \nPlease use PLACE X,Y,F to change my coordinates.\n");
        Console.Write("Enter your commands:\n");

        // Keep prompting the user for input continuously until a STOP command is entered.
        string command;
        do
        {
            command = Console.ReadLine();   
            ProcessCommand(command);


        } while (!string.Equals(command, "STOP", StringComparison.OrdinalIgnoreCase));


        //call the method corresponding to the command.
        void ProcessCommand(string command)
        {
            //regex used to validate the place command
            string pattern = @"^PLACE\s+(-?[1-9]\d*),(-?[1-9]\d*),[A-Za-z]+$";
            string[] parts = new string[0];

            try
            {

                if (!string.IsNullOrEmpty(command.Trim()))
                {
                    // validate input and add methods
                    if (command.StartsWith("PLACE", StringComparison.OrdinalIgnoreCase) && Regex.IsMatch(command.ToUpper(), pattern))
                    {
                        parts = command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        command = parts[0];
                    } else if (command.StartsWith("PLACE", StringComparison.OrdinalIgnoreCase) && !Regex.IsMatch(command.ToUpper(), pattern))
                    {
                        Console.WriteLine("Please enter a valid PLACE command");
                        return;
                    }

                    switch (command.ToUpper().Trim())
                    {
                        case "PLACE":
                            if (parts.Length != 4)
                                Console.WriteLine("Please enter a valid PLACE command");
                            else
                                Kay.Place(int.Parse(parts[1]), int.Parse(parts[2]), parts[3].ToUpper());
                            break;
                        case "MOVE":
                            Kay.Move();
                            break;
                        case "LEFT":
                            Kay.Left();
                            break;
                        case "RIGHT":
                            Kay.Right();
                            break;
                        case "REPORT":
                            Kay.Report();
                            break;
                        case "STOP":
                            Console.WriteLine("Ending operation.");
                            break;
                        default:
                            Console.WriteLine("Invalid command ");
                            break;
                    }
                } else { Console.WriteLine("Invalid command"); }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Index is outside the bounds of the array: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                parts = null;
                pattern = null;
            }
        }
    } 
}

