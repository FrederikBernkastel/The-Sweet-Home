using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public enum LogType
    {
        Enter,
        Exit,
        Awake,
        Start,
        Update,
        FixedUpdate,
        Init,
        GUI,
        DrawGizmos,
        Dispose,
        Reset,
    }
    public enum TypeApplication
    {
        Cossacks,
        TheSweetHome,
        Lesson7,
        Foxhole,
    }
    public enum TypeVirtualCamera
    {
        None = 0,
        CameraMenu,
        CameraMainCharacter,
        CameraFreeCamera,
        Ignore,
    }
    public enum TypeView
    {
        ViewLoadingDefault,
        ViewPrologeDefault,
        ViewMenuDefault,
        ViewMainMenuDefault,
        ViewSettingsDefault,
        ViewMainCamera,
        ViewUICamera,
        ViewHelp,
        ViewTV,
        ViewNPC,
        ViewMainCharacter,
        ViewFlowchart,
        ViewGhost,
        ViewAdditionalUICanvas,
        ViewMessageBox,
        Ignore,
    }
    public enum TypeViewUI
    {
        ViewLoadingDefault,
        ViewPrologeDefault,
        ViewMenuDefault,
        ViewMainMenuDefault,
        ViewSettingsDefault,
        ViewMainCamera,
        ViewUICamera,
        ViewHelp,
        ViewTV,
        ViewNPC,
        ViewMainCharacter,
        ViewFlowchart,
        ViewGhost,
        ViewAdditionalUICanvas,
        ViewMessageBox,
        Ignore,
    }
    public enum TypeStatArmor
    {
        Attack,
        Defense,
        Speed,
        Life,
    }
    public enum TypeItem
    {
        Helmet,
        Armor,
        Legs,
        Gloves,
        Shoes,
        Belt,
    }
    public enum TypeResources
    {
        CameraResources,
        LoadingResources,
        PrologeResources,
        GameResources,
        MainActorResources,
        GameUIResources,
        StoryUIResources,
        AdditionalUIResources,
        DebugTest,
    }
    public enum TypeCamera
    {
        MainCamera,
        UICamera,
        Ignore,
    }
    public enum TypeChannel
    {
        Channel1,
        Channel2,
        Channel3,
        Channel4,
    }
    [Flags]
    public enum TypeUpdate : byte
    {
        None = 0,
        Update = 2,
        FixedUpdate = 4,
        GUI = 8,
        DrawGizmos = 16,
    }
}
