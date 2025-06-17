# .NET 9.0 Upgrade Plan

## Execution Steps

1. Validate that an .NET 9.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 9.0 upgrade.
3. Upgrade MVCTagCloudAppCore\MVCTagCloudAppCore.csproj
4. Fix compilation errors

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Project upgrade details

#### MVCTagCloudAppCore\MVCTagCloudAppCore.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`
