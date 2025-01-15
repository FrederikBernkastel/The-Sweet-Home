using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public enum TypeVirtualCamera
    {
        CameraMenu,
        CameraMainCharacter,
        CameraFreeCamera,
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
    }
}
