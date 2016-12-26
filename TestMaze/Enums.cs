using System.Collections;
using System.ComponentModel;

namespace TestMaze
{
    public enum GameMode
    {
        DrawingPath = 0,
        Previous,
        Paste,
        Preview,
        Shoping,
        FirstPerson
    }

    public enum MazeState
    {
        Starting,
        Drawing,
        Decorating
    }

    public enum ItemDirection
    {
        Left,
        Right
    }

    public enum EntryType
    {
        Entrance = 0,
        Exit
    }

    public enum ShopItemType
    {
        Decorate,
        Irritant
    }

    public enum ShopItemPositionType
    {
        Floor,
        Middle,
        Wall
    }

    public enum ObjectInventoryState
    {
        Increase,
        Reduce
    }

    public enum MazeDirection
    {
        North,
        East,
        South,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest,
        None
    }

    public enum MazeObjectLocation
    {
        Corridor,
        Nook,
        Intersections
    }

    public enum MazeObjectPositionCell
    {
        Wall,
        Ground,
        WallLayer,
        GroundLayer,
        Ceiling,
        CellMidst

    }

    public enum MazeObjectFunction
    {
        Decorate,
        Deceive,
        Frighten,
        Irritant,
        Complexify,
        Booster,
        AntiBooster

    }

    public enum ButtonState
    {
        Enable,
        Highlight,
        Disable

    }

    public enum CellType
    {
        none,
        Straight,
        Corner,
        DeadEnd,
        T_Junction,
        X_Road,
    }

    public enum ZoomType
    {
        ZoomIn,
        ZoomOut
    }

    public enum MoveType
    {
        Left,
        Right,
        Down,
        Up
    }

    public class RuleMaze
    {
        public static string NONE = "";
        public static string LENGTH = "LENGTH";
        public static string LOOPS = "LOOPS";
        public static string BLOCKED_SESSION = "BLOCKED SESSION";
    }

}