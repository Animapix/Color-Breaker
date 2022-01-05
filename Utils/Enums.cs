using System;

[Flags]
public enum Sides
{
    None = 0,
    Left = 1,
    Top = 2,
    Right = 4,
    Bottom = 8,
    Vertical = 16,
    Horizontal = 32
}

public enum Scenes
{
    Menu,
    Game,
    LevelSelection
}

public enum Layers
{
    None,
    Background,
    Shadows,
    Props,
    Particles,
    GUI
}

[Flags]
public enum Alignment { 
    Center = 0, 
    Left = 1, 
    Right = 2, 
    Top = 4, 
    Bottom = 8 
}