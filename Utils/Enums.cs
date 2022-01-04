using System;

public enum Sides
{
    None,
    Left,
    Top,
    Right,
    Bottom,
    Vertical,
    Horizontal
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