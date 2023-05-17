# AddressBook migration from Xamarin.Forms to .Net MAUI

## Overview
This repository contains **Address Book** application for Microsoft.Maui (.Net 7.0) which is migration of Xamarin.Forms and that shows design & coding practices followed by **[Differenz System](http://www.differenzsystem.com/)**. 

The app does the following:
1. **Login:** 
    - User can login via email/password. 
2. **Home:** 
    - It will list all the saved contacts. 
    - It has the option to add a new contact on the top right.
    - Contact can be deleted by swiping card to left and clicking on trash icon.
    - User can edit contact by tapping on contact.
3. **Create new contact:** 
    - User can add a new contact to his address book by filling details here.
4. **Dark/Light Mode:** 
    - App supports Light & Dark mode, user can change mode by going into device dark mode settings.
5. **Localization:**
    - App is designed to extend support for multiple languages. Currently we have added support for english language.

## Pre-requisites
- iOS device or emulator running iOS 11 or above
- Android device or emulator running API 21 (5.0 - Lollipop) or above
- Mac Catalyst device or emulator running 10.15 or above
- [Android SDK 33](https://developer.android.com/about/versions/13/migration)
- [Xcode 14.2](https://developer.apple.com/documentation/xcode-release-notes/xcode-14_2-release-notes)
- [Visual Studio](https://www.visualstudio.com/vs/features/mobile-app-development/#downloadvs)

## Getting Started
1. [Install Visual Studio](https://www.visualstudio.com/vs/features/mobile-app-development/#downloadvs)
2. Clone this sample repository
3. Open the sample project into Visual Studio
    - File -> Open
    - Browse to <path_to_project>/AddressBook.MAUI.sln
    - Click "Open"
4. Select project to run (android or ios) from top left after toolbar. Click on :arrow_forward: button

## Key Tools & Technologies
- **Database:** [sqlite-net-pcl](https://www.nuget.org/packages/sqlite-net-pcl/1.8.116) (1.8.116)
- **API/Service calls:** HttpClient
- **IDE:** [Visual Studio Community for MAC](https://www.visualstudio.com/vs/visual-studio-mac/) (17.4.2)

- **Others:** [Microsoft.Maui](7.0.52), [.NET 7.0](https://www.microsoft.com/net/learn/get-started/macos) (7.0), [Microsoft.Android] (33.0.4), Microsoft.iOS (16.1.1481)

## Xamarin.Forms Screenshots
### Android
<img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.Xamarin/ScreenShots/Android/login.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.Xamarin/ScreenShots/Android/list.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.Xamarin/ScreenShots/Android/detail.png" width="280">  

### iOS
<img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.Xamarin/ScreenShots/iOS/login.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.Xamarin/ScreenShots/iOS/list.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.Xamarin/ScreenShots/iOS/detail.png" width="280">


## .Net MAUI Screenshots
### Android
<img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/Android/login.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/Android/list.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/Android/detail.png" width="280">  

### iOS
<img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/iOS/login.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/iOS/list.png" width="280"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/iOS/detail.png" width="280">

### Mac Catalyst
<img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/MacCatalyst/login.png" width="300"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/MacCatalyst/list.png" width="300"> <img src="https://github.com/differenz-system/AddressBook.Xamarin--MAUI/blob/main/AddressBook.MAUI/ScreenShots/MacCatalyst/detail.png" width="300">

## Support
If you've found an error in this sample, please [report an issue](https://github.com/differenz-system/AddressBook.MAUI/issues/new). You can also send your feedback and suggestions at info@differenzsystem.com

Happy coding!
