# Kraftful Analytics for .Net

## Setup

Add the package as a dependency to your solution;

1. `git clone https://github.com/kraftful/kraftful-analytics-dot-net.git`
2. Import the `KraftfulAnalytics` project into your .Net solution and add it as a reference to your project.

## Usage

The KraftfulAnalytics API exposes the following methods:

### `Initialize(string apiKey)`

Add the initialize call to your App's `init()` method.

```csharp
using KraftfulAnalytics;

KraftfulAnalytics.Initialize("YOUR_API_KEY");
```

### `TrackSignInStart()` and `TrackSignInSuccess(string userId)`

Add the `TrackSignIn(...)` calls to your login and registration flows. Typically the start call happens when your login/register screen loads and the success call happens when the user successfully logs in/registers.

```csharp
// Call trackSignInStart when your sign in screen appears
KraftfulAnalytics.TrackSignInStart();
```

```csharp
// Call trackSignInSuccess when the user is authenticated
KraftfulAnalytics.TrackSignInSuccess(authState.loggedInUserId);
```

### `TrackConnectionStart()` and `TrackConnectionSuccess()`

Add the `TrackConnection(...)` calls to your device connection flows. Similar to the sign in tracking, these are typically added when the first screen in your device connection flow loads and then is successfully connected.

```csharp
// Call trackConnectionStart when your connection page appears
KraftfulAnalytics.TrackConnectionStart();
```

```csharp
// Call trackConnectionStart when your connection page appears
KraftfulAnalytics.TrackConnectionSuccess();
```

### `TrackFeatureUse(string feature)`

Add `TrackFeatureUse(...)` calls to the features you want to track. Here we're adding the calls to some Button actions.

```csharp
// Track usage of a feature
KraftfulAnalytics.TrackFeatureUse("Decrease Setpoint");
```

### `TrackAppReturn(string userId)`

Add `TrackAppReturn(...)` calls when your app is foregrounded. This should be done where you rehydrate your user information so you can pass the logged in userId if they are already logged in.

```csharp
// Track return to the app with the logged in userId
KraftfulAnalytics.TrackAppReturn(authState.loggedInUserId);
```

## License

```
MIT License

Copyright (c) 2022 Kraftful

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
