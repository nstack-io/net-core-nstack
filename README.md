# NStack .NET Standard SDK

## How to use

### Configuration

### DI setup

## Functions

## Translation generator
The translation generator is a tool which can access your NStack translation and generate C# classes based on the JSON response from a specified resource.

Currently the tool is only available for use in Windows. The tool is downloaded along with the NuGet package in `packages/NStack.SDK/{version}/NStackTranslationGenerator/NStackTranslationGenerator.exe`.

### Parameters
The parameters are listed below and can also be viewed by using the `--help` for the tool

| Short name | Long name | Required | Default value | Description |
| ---------- | --------- | ---------| ------------- | ----------- |
| -c | --className | | Translation | The name of the class holding the translation sections |
| -i | --applicationId | :heavy_check_mark: | N/A | The ID for you NStack application |
| -k | --apiKey | [x] | N/A | The API key for your NStack integration |
| -n | --namespace | [x] | N/A | The namespace for the created classes |
| -o | --output | | ./output | The output folder for the generated classes |
| -s | --showJson | | false | Show the fetched JSON from NStack in the console? |
| -t | --translationId | [x] | N/A | The ID of the translation to generate the classes from |
| -u | --url | | https://nstack.io | The base url of the NStack service |