# CS5700 - Decoupling With Observers And Extending With Decorators

## Grader Notes

I apologize, I was unable to get the GUI portion of the project into a fully functional state.

The Program is still runnable and will process the incoming messages however the only active observer is the CheatingScreen and the results of my cheat detection are logged to the Debug Console.

To run the program swap the `Project Startups` tab in Visual Studio to `UI` and run.

## Implementation Notes
### Observer 'Interface'

[Observer.cs](./src/Program/Common/Observer.cs)

### Observer's (StatusScreen, CheatingScreen)

- [StatusScreen.cs](./src/Program/Entities/StatusScreen.cs)

- [CheatingScreen.cs](./src/Program/Entities/CheatingScreen.cs)

### Subject 'Interface'

[Subject.cs](./src/Program/Common/Subject.cs)

### Subject's (Racer)

- [Racer.cs](./src/Program/Entities/Racer.cs)

### Notifying Observers of Subject Modifications

[Controller.cs:102](./src/Program/Controller.cs#L102) - Lines 102:125

### Observer Creation

[Controller.cs:141](./src/Program/Controller.cs#L141) - Lines 141:160

### Desctruction of observers

[Controller.cs:162](./src/Program/Controller.cs#L162) - Lines 162:166

## Unit Tests

Unit Tests are minimal but visible in [UnitTests.cs](./tests/Program.Tests/UnitTests.cs)

Theses are executable by changing the `Project Startups` tab in Visual Studio to `Program.Tests` and run.