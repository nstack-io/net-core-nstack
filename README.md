# NStack .NET Standard SDK
Use this SDK to interact with NStack. This SDK contains injectable services to get translations etc. from NStack.

## Configuration
The configuration is stored in the class `NStackConfiguration` which have three properties:
- ApiKey
- ApplicationId
- BaseUrl

All information can be found in the NStack portal

## Interfaces
Every service is implemented using the interface approach to make dependency injection as smooth as possible.

### INStackRepository
This interface handles the REST communication with the NStack server. The built in implementation of the interface is `NStackRepository` which takes `NStackConfiguration` as a constructor parameter. The exposed functions are for use by the service layer.

### INStackLocalizeService
This interface handles all communication regarding translations.

#### GetLanguages
This function returns all available languages for the platform provided as parameter. The format returned is `DataWrapper&lt;List&lt;ResourceData>>`.

##### Parameters:

| Name | Type | Description |
| ---- | ---- | ----------- |
| platform | NStackPlatform (enum) | Which platform to get the languages for |

##### ResourceData
| Property name | Type |
| ------------- | ---- |
| Id | int |
| Url | string |
| LastUpdatedAt | DateTime |
| ShouldUpdate | bool |
| Language | Language |

##### Language
| Property name | Type |
| ------------- | ---- |
| Id | int |
| Name | string |
| Locale | string |
| LanguageDirection | LanguageDirection (enum) |
| IsDefault | bool |
| IsBestFit | bool |

#### GetResource
This function returns translations for a language. The function by default returns a `ResourceItem`, but takes a type if you have a class which inherits `ResourceItem` to get your language file strongly typed.

This function has two overloads: one for a specific ID and one for locale.

##### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| id | int | The ID of the translation to get |

| Property name | Type | Description |
| ------------- | ---- | ----------- |
| locale | string | The ISO code for the language to get e.g. `en-GB` |
| platform | NStackPlatform (enum) | Which platform to get the translation for |

##### ResourceItem
ResourceItem is an implementation of `ConcurrentDictionary<string, ResourceInnerItem>` which can be extended to get strongly typed sections for your translation. The `Translation generator` tool can be used to generate these files automatically.

This class holds the translation sections and can be used by either extending the class or just use the class as is as a normal dictionary e.g. `item["mySection"]`.

##### ResourceInnerItem
ResourceInnerItem is an implemenation of `ConcurrentDictionary<string, string>` which can be extended in the same manner as `ResourceItem`. This class holds the actual translations and can be used by either extending the class or just use the class as is as a normal dictionary e.g. `innerItem["myTranslation"]`.

### GetDefaultResource
This functions returns the default language. The function by default returns a `ResourceItem`, but takes a type if you have a class which inherits `ResourceItem` to get your language file strongly typed.

#### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| platform | NStackPlatform (enum) | Which platform to get the translation for |

## DI setup
The SDK is built with DI support in mind and can be quickly set up in your `startup.cs` file in `ConfigureServices`:

```C#
services.AddSingleton<NStackConfiguration>(r => new NStackConfiguration
{
    ApiKey = "MyApiKey",
    ApplicationId = "MyApplicationId",
    BaseUrl = "MyBaseUrl"
});
services.AddTransient<INStackRepository, NStackRepository>();
services.AddTransient<INStackLocalizeService, NStackLocalizeService>();
```

Best practice is to not hard code the configuration values but to fetch them from your application settings.

## Translation generator
The translation generator is a tool which can access your NStack translation and generate C# classes based on the JSON response from a specified resource.

Currently the tool is only available for use in Windows. The tool is downloaded along with the NuGet package in `packages/NStack.SDK/{version}/NStackTranslationGenerator/NStackTranslationGenerator.exe`.

### Parameters
The parameters are listed below and can also be viewed by using the `--help` for the tool

| Short name | Long name | Required | Default value | Description |
| ---------- | --------- | :------: | ------------- | ----------- |
| -c | --className | | Translation | The name of the class holding the translation sections |
| -i | --applicationId | :heavy_check_mark: |  | The ID for you NStack application |
| -k | --apiKey | :heavy_check_mark: |  | The API key for your NStack integration |
| -n | --namespace | :heavy_check_mark: |  | The namespace for the created classes |
| -o | --output | | ./output | The output folder for the generated classes |
| -s | --showJson | | false | Show the fetched JSON from NStack in the console? |
| -t | --translationId | :heavy_check_mark: |  | The ID of the translation to generate the classes from |
| -u | --url | | https://nstack.io | The base url of the NStack service |