# Portland Weather

A simple C# app to display current conditions in Portland, OR as provided by the
[OpenWeatherMap](https://openweathermap.org/api) API.  This program was written
for .Net 8.0, although newer versions may work as well.

## Sample output

```
** Conditions in Portland as of Friday, December 13, 2024 1:13 PM **

Current Temperature : 42.3° (feels like 34.5°)
Temperature Range   : 40.0° (Low) to 44.7° (High)

Conditions are rain with moderate rain, a humidity of 94%,
 wind speed of 16.11 mph, and gusts up to 25.32 mph.

The sun will rise at 7:43 AM and will set at 4:27 PM

Data by OpenWeatherMap
```

## Usage

### Compiling the code

```
# This will build a binary, PortlandWeather.exe
cd PortlandWeather
dotnet build

```

### Running the program

This program makes use of [OpenWeatherMap](https://openweathermap.org/api) to
retrieve weather information about Portland, Oregon.  As such, you will need to
go to their web site, create an account, and generate a new API key.  Copy the
key and then do the following:

[comment]: # (Does anyone still use csh or derivative?)
```
# Set API Key in bourne-shell compatible shells (bash, zsh, etc.,)
export WEATHER_KEY=your-key-here

# Set API Key in Powershell
Set-Item -Path Env:\WEATHER_KEY -Value "your-key-here"

# Run the program
dotnet run

# Build and run the program
dotnet build
./bin/Debug/net8.0/PortlandWeather.exe
```

### Publishing the program

```
# From the root project folder ...
dotnet publish -o /path/to/output-folder
```

For further options, consult the [dotnet
publish](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish) documentation.

## Caveats

[comment]: # (Just in case people get confused ...)
This program displays the current weather for Portland, *Oregon* not Portland, *Maine*.

## Copyright

See file, LICENSE, for copyright information
