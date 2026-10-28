# GitHub Copilot Instructions for Boombugs (Continued)

## Mod Overview and Purpose

**Boombugs (Continued)** is an update to the Poshspiders mod for RimWorld. It introduces new explosive insect species such as Boomspiders, Boomscarabs, and Boompedes. The mod enhances the existing Poshspiders mod by making the boom-insects inherit base insect types rather than copying them. Key features include a boom-hive that spawns boom-insects, chemfuel, and fuel-puddles, and a new, more challenging infestation event. The mod aims to provide an engaging experience with explosive insects in a RimWorld game.

## Key Features and Systems

- **Inheritance of Base Insects**: Boom-insects now inherit from base insect types, reducing code duplication.
- **Boom-Hive**: A new structure that spawns boom-insects, chemfuel, and fuel-puddles.
- **Enhanced Infestation Event**: A more challenging and rarer infestation event that introduces boom-insects.
- **Explosions on Death**: Only Boomspiders have a large explosion upon death; other boom-insects have smaller explosions.
- **Fire Resistance**: All boom-insects and their hive are almost immune to fire.
- **Milkable Boom-Insects**: Tame boom-insects can be milked for chemfuel.
- **Compatibility**: Works well with BoomMod Expanded.

## Coding Patterns and Conventions

- **Class Naming**: Use descriptive and consistent class names. For example, `BoomHive`, `BoomPawnKindDefOf`, `BoomThingDefOf`, `BoomTunnelHiveSpawner`, and `IncidentWorker_BoomInfestation`.
- **Method Naming**: Methods should be clear and convey their functionality. Example methods include `ResetStaticData`, `createSustainer`, `ExposeData`, `SpawnSetup`, `Tick`, `spawnTunnels`, and `TryExecuteWorker`.
- **File Structure**: Organize C# files into logical sections, such as `BoomHive.cs`, `BoomPawnKindDefOf.cs`, `BoomThingDefOf.cs`, `BoomTunnelHiveSpawner.cs`, and `IncidentWorker_BoomInfestation.cs`.
- **XML Files**: Ensure XML files are organized into categories like `Storyteller`, `ThingsDefs_Buildings`, `ThingsDefs_Races`, etc.
- **Error Handling**: Implement proper error handling and logging to assist with debugging.

## XML Integration

- **Defs**: Use XML definitions to define new game elements such as `IncidentDef`, `letterDef`, `ThingDef`, `filthDef`, `milkDef`, `PawnKindDef`, and other relevant elements.
- **Attributes and Tags**: Ensure all XML tags and attributes are correctly formatted and named to match RimWorld's schema.
- **Inheritance and References**: Use XML inheritance and references to link new definitions to existing game elements.

## Harmony Patching

- **Harmony Patches**: Use Harmony for patching existing game methods to integrate new features seamlessly.
- **Patch Naming**: Name patches clearly to indicate their purpose. Example patch names include `ResetStaticData`, `createSustainer`, `ExposeData`, `SpawnSetup`, and `Tick`.
- **Patch Location**: Place Harmony patches in appropriate classes and methods to ensure they are executed at the correct time.

## Suggestions for Copilot

1. **XML Definitions**: Provide examples of XML definitions for new game elements such as `IncidentDef`, `ThingDef`, and `PawnKindDef`.
2. **Harmony Patches**: Offer examples of Harmony patches for existing game methods to integrate new features.
3. **C# Class and Method Generation**: Generate C# class and method templates based on the coding patterns and conventions outlined above.
4. **Error Handling and Logging**: Suggest best practices for error handling and logging within C# code.

By following these guidelines and utilizing Copilot for code generation and assistance, you can efficiently develop and enhance the Boombugs (Continued) mod for RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

