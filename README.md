# NStack .NET Standard SDK
Use this SDK to interact with NStack. This SDK contains injectable services to get translations etc. from NStack.

## Installation
The SDK is available from the NuGet gallery. The package is called `NStack.SDK`, you can install it by searching for it or by running the following command in the Package Manager Console

```PowerShell
Install-Package NStack.SDK
```

Or if you're using the dotnet CLI, run the following command:

```PowerShell
dotnet add package NStack.SDK
```

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
This function returns all available languages for the platform provided as parameter. The format returned is `DataWrapper<List<ResourceData>>`.

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

### INStackTermsService
This interface handles all communication regarding terms.

#### GetAllTerms
Gets all terms available for a language. The format returned is `DataWrapper<IEnumerable<TermsEntry>>`.

##### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| language | string | The ISO language code to get the list of terms for e.g. en-GB. |

##### TermsEntry
| Property name | Type |
| ------------- | ---- |
| Id | int |
| Type | string |
| Name | string |
| Slug | string |

#### GetTermsVersions
Gets the available versions of a specified terms. The format returned is `DataWrapper<IEnumerable<Terms>>`.

##### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| termsName | string | The name of the terms to fetch. |
| userId | string | The unique ID of the user who is going to read the terms.|
| language | string | The ISO language code to get the terms in e.g. en-GB. |

##### Terms
| Property name | Type |
| ------------- | ---- |
| Id | int |
| Version | string |
| VersionName | string |
| PublishedAt | DateTime |
| HasViewed | bool |

#### GetNewestTerms
Gets the newest version of the specified terms. The format returned is `DataWrapper<TermsWithContent>`.

##### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| termsName | string | The name of the terms to fetch. |
| userId | string | The unique ID of the user who is going to read the terms. |
| language | string | The ISO language code to get the terms in e.g. en-GB. |

##### TermsWithContent
| Property name | Type |
| ------------- | ---- |
| Id | int |
| Version | string |
| VersionName | string |
| PublishedAt | DateTime |
| HasViewed | bool |
| Content | TermsContent |

##### TermsContent
| Property name | Type |
| ------------- | ---- |
| Data | string |
| Locale | string |

#### GetTerms
Gets a specified version of a terms. The format returned is `DataWrapper<TermsWithContent>`.

##### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| termsId | int | The ID of the terms to fetch. |
| userId | string | The unique ID of the user who is going to read the terms. |
| language | string | The ISO language code to get the terms in e.g. en-GB. |

#### MarkRead
Mark a terms as read for the user. The format returned is `bool`.

##### Parameters
| Property name | Type | Description |
| ------------- | ---- | ----------- |
| termsId | int | The ID of the terms to fetch. |
| userId | string | The unique ID of the user who is going to read the terms. |
| language | string | The ISO language code to get the terms in e.g. en-GB. |

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
services.AddTransient<INStackTermsService, NStackTermsService>();
```

Best practice is to not hard code the configuration values but to fetch them from your application settings. If `BaseUrl` isn't set, it will default to `https://nstack.io`. The rest of the properties does not have any default values.

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


## Demo site
The demo site is available on [https://nstack-dotnetsdkdemo.azurewebsites.net/](https://nstack-dotnetsdkdemo.azurewebsites.net/). The infrastructure has been defined in Terraform if you want to set up your own version quickly.