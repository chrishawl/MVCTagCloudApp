# .NET 9.0 Upgrade Report

## Project modifications

| Project name                                   | Old Target Framework    | New Target Framework         | Commits                   |
|:-----------------------------------------------|:-----------------------:|:----------------------------:|---------------------------|
| MVCTagCloudAppCore\MVCTagCloudAppCore.csproj   |   net10.0               | net9.0                       | 482dcd1f                  |

## Code Fixes

| File                                            | Description                                                        | Commit Id  |
|:------------------------------------------------|:-------------------------------------------------------------------|------------|
| MVCTagCloudAppCore\Controllers\HomeController.cs| Corrected constructor syntax for DI of IHttpClientFactory          | e95d9a4c   |

## All commits

| Commit ID   | Description                                                        |
|:------------|:-------------------------------------------------------------------|
| 482dcd1f    | Update target framework in MVCTagCloudAppCore.csproj               |
| e95d9a4c    | Corrected the syntax error in the constructor declaration          |

## Next steps

- Review your application for any runtime issues.
- Run and test your application to ensure all features work as expected.
- Address any package vulnerabilities if needed.
