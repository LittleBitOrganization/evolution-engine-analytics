# evolution-engine-analytics

## Installation

1. ##### Dlownload all those archives from [Google API](https://developers.google.com/unity/archive#external_dependency_manager_for_unity) in .tgz extention.

[com.google.firebase.app](https://developers.google.com/unity/archive#firebase_app_core)
[com.google.firebase.auth](https://developers.google.com/unity/archive#firebase_authentication)
[com.google.firebase.storage](https://developers.google.com/unity/archive#cloud_storage_for_firebase)
[com.google.firebase.analytics](https://developers.google.com/unity/archive#google_analytics_for_firebase)

2. #### Create folder named *GooglePackages* in your project root folder, it should be next to the *Assets* folder


3. #### Add those lines to your manifest file (it locates somwhere in root package folder).

```json 
 "dependencies": {
  "com.gameanalytics.sdk": "7.3.20",
  "com.google.external-dependency-manager": "https://github.com/LittleBitOrganization/evolution-engine-google-version-handler.git#1.2.171",
  "com.google.firebase.app": "file:../GooglePackages/com.google.firebase.app-9.0.0.tgz",
  "com.google.firebase.auth": "file:../GooglePackages/com.google.firebase.auth-9.0.0.tgz",
  "com.google.firebase.storage": "file:../GooglePackages/com.google.firebase.storage-9.0.0.tgz",
  "com.google.firebase.analytics": "file:../GooglePackages/com.google.firebase.analytics-9.0.0.tgz",
  "com.google.firebase.crashlytics": "file:../GooglePackages/com.google.firebase.crashlytics-9.0.0.tgz",
  "com.littlebitgames.analytics": "https://github.com/LittleBitOrganization/evolution-engine-analytics.git#1.0.0",
  "com.dbrizov.naughtyattributes": "https://github.com/dbrizov/NaughtyAttributes.git#upm"
}
```

4. #### Add scope for gameanalitycs 
```json
"scopedRegistries": [
    {
      "name": "Game Package Registry by Google", 
      "url": "https://unityregistry-pa.googleapis.com/", 
      "scopes": [ 
        "com.google" 
      ]
    },
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.gameanalytics"
      ]
    }
  ]
```
5. #### Open Unity project and wait for assets importing :raised_hands:














