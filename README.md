# V Rising Mod Template Extended

## Installation
`dotnet new --install VRising.ModTemplatePlus`

## Example usage
`dotnet new vrisingmodplus -n NameOfYourMod -how -desc "Description of your mod"`

Replace `NameOfYourMod` with the name of your mod and update the description, and optionally add/remove the --use flags as appropriate. Then cd into the directory and run `dotnet build` to build the mod.

## Flags
- `-desc` - Initialize the mod with a description
- `-how` - Starts the project with a how-to guide example
- `-bloodstone` - Use the Bloodstone Framework
- `-bloodycore` - Use the Bloody.Core Framework

## Resources

- Wiki: https://wiki.vrisingmods.com
- Discord: https://vrisingmods.com/discord

---

## Contributing to Template
You can use any directory, but there's a workflow using `QTemplateTest` included in a script and the gitignore. This script will clean up that direcotry, build, and reinstall the template locally. The workflow looks like:

### Reinstall locally:
`.Reinstall-Template.ps1`

### Test and Develop ex:
```ps1
dotnet new vrisingmodplus -n "QTemplateTest" -vcf -how
dotnet build QTemplateTest
```