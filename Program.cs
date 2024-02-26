using System;
using System.Text.RegularExpressions;

// Interface class to declare all the main methods to be implemented the concrete classes
public interface Robot
{
    public bool Place(int NewX, int NewY, string NewF);
    public bool Move();
    public bool Left();
    public bool Right();
    public void Report();
}
// Class ToyRobot implementing the interface Robot
public class ToyRobot : Robot
{
    private int X { get; set; }                          // Represent the x coodinate.
    private int Y { get; set; }                          // Repesent the y coordinate.
    private string F { get; set; }                     // Repesent the facing direction.

    private readonly int XLimit;                      // Represent the maximum limit of the X-coordinate

    private readonly int YLimit;                     // Represent the maximum limit of the Y-coordinate

    // Constructor to set the grid size of the ToyRobot.
    public ToyRobot(int XCord, int YCord)
    {
        this.XLimit = XCord;
        this.YLimit = YCord;
    }

    // Implementation of the Place method
    public bool Place(int NewX, int NewY, string NewF)
    {
        if (ValidateCoordinate(NewX, NewY) && ValidateDirection(NewF))
        {
            this.X = NewX;
            this.Y = NewY;
            this.F = NewF;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Implementation of the Move method
    public bool Move()
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
           return true;
        }
        else
        {
            return false;
        }
        
    }

    // Implementation of the Left method
    public bool Left()
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
        return true;
    }

    // Implementation of the Right method
    public bool Right()
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
        return true;
    }

    // Implementation of the Report method
    public void Report()
    {
        Console.WriteLine(this.X + "," + this.Y + "," + this.F);
    }

    // Method to validate the coordinates of the grid
    bool ValidateCoordinate(int XCord, int YCord)
    {
        return XCord >= (XLimit * -1) && XCord <= XLimit && YCord >= (XLimit * -1) && YCord <= YLimit;
    }

    // Method to validate the direction given in the command
    bool ValidateDirection(string Direction)
    {
        string[] validDirections = { "NORTH", "EAST", "SOUTH", "WEST" };
        return validDirections.Contains(Direction, StringComparer.OrdinalIgnoreCase);
    }

    // Method to validate the PLACE command using the given regex
    public bool IsValidPlaceCommand(string input)
    {
        string pattern = @"^(?i)PLACE\s+(?!-0)(-?[0-" + XLimit + "]+|0),(?!-0)(-?[0-" + YLimit + "]+|0),(EAST|WEST|NORTH|SOUTH)$";//@"^(?i)PLACE\s+(-?[0-9]|-?[1-9][0-9]?),(-?[0-9]|-?[1-9][0-9]?),(EAST|WEST|NORTH|SOUTH)$";
        return Regex.IsMatch(input.ToUpper(), pattern);
    }
}

// Main class of the program.
class Program
{
    static void Main(string[] args)
    {
        ToyRobot Kay = new ToyRobot(5,5);                // Initialize the ToyRobot with the grid size as X,Y Coordinates

        Console.WriteLine("Hello, it's me, Kay, your ToyRobot 1.0. I'm all set and ready to begin moving!\nPlease use 'PLACE X,Y,F' to set my coordinates.\n");

        string command;
        bool isFirstExecution = true;

        // Keep prompting the user for input until a valid "PLACE" command is entered
        Console.Write("Enter your commands:\n");
        do
        {
            if (!isFirstExecution)
            {
                Console.WriteLine("Please enter a valid PLACE Command");
            }
            command = Console.ReadLine();
            isFirstExecution = false;
        } while (!Kay.IsValidPlaceCommand(command));

        // Keep prompting the user for input continuously until a STOP command is entered.
        while (!string.Equals(command, "STOP", StringComparison.OrdinalIgnoreCase))
        {
            ProcessCommand(command);
            command = Console.ReadLine();
        }

        // This method calls the method corresponding to the given command.
        void ProcessCommand(string command)
        {
            string[] parts = new string[0];
            try
            {
                if (!string.IsNullOrEmpty(command.Trim()))
                {
                    // validate input and add methods
                    if (command.StartsWith("PLACE", StringComparison.OrdinalIgnoreCase) && Kay.IsValidPlaceCommand(command))
                    {
                        parts = command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        command = parts[0];
                    } else if(command.StartsWith("PLACE", StringComparison.OrdinalIgnoreCase) && !Kay.IsValidPlaceCommand(command))
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
                                if (Kay.Place(int.Parse(parts[1]), int.Parse(parts[2]), parts[3].ToUpper()))
                                    Console.WriteLine("Toy Robot has been set  to this location");
                                else
                                Console.WriteLine("Toy Robot has not been placed. Invalid PLACE Command.");
                            break;
                        case "MOVE":
                            if (Kay.Move())
                                Console.WriteLine("Toy Robot has been moved.");
                            else
                                Console.WriteLine("Toy Robot has reached its boundary X or Y Coordinates.");
                            break;
                        case "LEFT":
                            if (Kay.Left())
                                Console.WriteLine("Successfully rotated the facing direction.");
                            break;
                        case "RIGHT":
                            if (Kay.Right())
                                Console.WriteLine("Successfully rotated the facing direction.");
                            break;
                        case "REPORT":
                            Kay.Report();
                            break;
                        case "STOP":
                            Console.WriteLine("Ending operation.");
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
                } else { Console.WriteLine("Command not found"); }
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
            }
        }
    } 
}
