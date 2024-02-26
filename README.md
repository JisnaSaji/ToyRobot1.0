#ToyRobot1.0

ToyRobot1.0 is a program that simulates a Toy Robot named Kay moving on a tabletop. The tabletop is set to a grid of 5 units x 5 units, assuming there are no obstructions on the table surface. The program provides a basic Command-Line Interface (CLI) implemented in C#. Note that a graphical user interface (UI) has not been added.

## Functionality

* The program accepts commands to control the Toy Robot's movements on the tabletop.
* Each command will either result in a successful action or a failure message.
* The first valid command must be PLACE in the format PLACE X,Y,F. After placing the robot, it will not accept any other sequence of valid commands until the PLACE command is given.
* Commands are not case-sensitive.

## Tabletop Specifications

* The tabletop boundary coordinates are as follows:
* x-coordinates range from -5 to 5.
* y-coordinates range from -5 to 5.
* The centre of the tabletop is at coordinate (0,0).
* The corners of the tabletop are at coordinates (5,5), (5,-5), (-5,-5), and (-5,5).

## Installation

To install and run the ToyRobot1.0 project, follow these steps:

1. Clone the repository to your local machine:
	git clone https://github.com/JisnaSaji/ToyRobot1.0.git
2. Open the ToyRobot1.0.sln solution file in Microsoft Visual Studio.
3. Set the ToyRobot project as the startup project.
4. Build and run the project using Visual Studio.
5. Press F5 or select "Start Debugging" from the Debug menu.
6. The program will start running in the Visual Studio debugger, and you can interact with it using the Command-Line Interface (CLI).

## Commands

* PLACE 0,0,NORTH: Place the robot at the centre of the tabletop, facing North.
* MOVE: Moves the robot one unit forward in the direction it is facing.
* LEFT: Rotates the robot 90 degrees counterclockwise.
* RIGHT: Rotates the robot 90 degrees clockwise.
* REPORT: Prints the current position and orientation of the robot.
* STOP: Stops the execution of CLI

## Example Input and Output

Input:

PLACE 0,0,NORTH
MOVE
REPORT

Output:

0,1,NORTH
