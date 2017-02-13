HockeySDK for Windows (WPF)
=========

The unofficial Windows SDK for the http://www.hockeyapp.com, specific to WPF platform

## Feature Support

| Platform Name | Crash Reporting  | Beta Distribution | Feedback | Authentication | Custom Events | Usage Metrics
| --- | --- | --- | --- | --- | --- | --- |
| HockeySDK.WPF 4.5 | &#x2713; | &#x2713; | | |  | 
| PCL |  |  |  |  | &#x2713; | |

## Onboarding Instructions 
1. Add nuget package: 

| Platform | Nuget Package Manager Console Command |
| --- | --- |
| WPF 4.5 | Install-Package Install-Package Unofficial.HockeySDK.WPF |

2. In App.xaml.cs file add the following line in usage declaration section: <pre>using Microsoft.HockeyApp;</pre>
3. In App.xaml.cs file add the following line in App class constructor: <pre>Microsoft.HockeyApp.HockeyClient.Current.Configure(“Your_App_ID”);</pre>

## Documentation
https://support.hockeyapp.net/kb/client-integration-windows-and-windows-phone

## Support
If you have any questions, problems or suggestions, create an issue.

## Original sources
This repository is a fork of the WPF original project of HockeyApp team.
I've keeped only necessary sources for WPF, and integrate some code of this repository : https://github.com/edoust/HockeySDK-Windows
