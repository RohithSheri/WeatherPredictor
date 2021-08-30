# WeatherPredictor

## Project Overview
The project is aimed to predict weather for a given day. If the day is not given the app will return the current day weather prediction.

## Code Structure
The code is divided into four parts
1. Console App (WeatherPredictor)
2. Business Services (PredictionService)
3. Data Services (DSWeatherPrediction)
4. Unit Tests (BusinessServiceTests)

## Architecture
1. Tried to follow SOLID principles, DRY, YAGNI.
2. Everything is open for extension.
3. Implementations are easy to be replacable.

## Steps to run the program
1. Clone the repo.
2. Make sure to install .Net Core (3.1). Link to follow https://docs.microsoft.com/en-us/dotnet/core/install/
3. Once the installation is complete. Open the terminal and check 'dotnet' command is recognized.
4. Then navigate to the solution/repo folder.
5. Run the following commands by then navigating to /WeatherPredictor folder. Note:- The solution has dependencies on Nuget Packages.
  a. dotnet build
  b. dotnet run
6. You should be prompted with the following instructions
  a. Enter the month in number.
  b. Enter the day in number.
7. The program should return the JSON response for the weather.

## Assumptions:
1. Not included the option to prompt for Julian day but that is easily plugable into the console application.
2. Unit tests are only for business logic. 
3. For now assumed to use only simple file retrieval instead of creating a database source for prediction data.
4. Didn't want to overarchitecture anything but kept everything open for extension.
5. Assumed we don't require to generate document either from doxygen or other alternatives.

## Improvements:
1. More unit tests and code coverage.
2. Improving the data access layer for extension.
3. A utility class to validate user input.
4. A support service that can validate input and call various prediction implementation based on the requirements and return json response.
