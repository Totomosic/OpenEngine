using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{

    public enum EventType
    {
        Char, MouseEntered, Key, MouseScroll, MousePosition, MouseButton
    }

    public enum BindState
    {
        Bound, Unbound
    }

    public enum BufferLayout
    {
        Vertices, Normals, TextureCoordinates, Color
    }

    public enum BufferType
    {
        Color0, Color1, Color2, Color3, Color4, Color5, Color6, Color7, Color8, Color9, Depth0, Depth1, Depth2, Depth3, Depth4, Depth5, Depth6, Depth7, Depth8, Depth9
    }

    public enum RenderMode
    {
        Arrays, Elements
    }

    public enum ShaderStatus
    {
        Idle, Running
    }

    public enum Space
    {
        World, Local, Origin
    }

    public enum ProjectionType
    {
        Perspective, Orthographic
    }

    public enum GLType
    {
        Framebuffer, Texture, VAO, VBO, Buffer, PixelBuffer
    }

    public enum PixelOperation
    {
        Download = (int)BufferTarget.PixelPackBuffer, Upload = (int)BufferTarget.PixelUnpackBuffer
    }

    public enum OutOfRange
    {
        ReturnDefault, ThrowException
    }

    public enum LightType
    {
        Point, Directional, Spotlight
    }

    public enum RepeatType
    {
        Repeat, None, Count
    }

    public enum AngleType
    {
        Degrees, Radians
    }

    public enum CoordinateType
    {
        NDC, Orthographic, TopLeft, BottomLeft
    }

    public enum NodeIndex
    {
        LeftBottomFront, RightBottomFront, LeftBottomBack, RightBottomBack, LeftTopFront, RightTopFront, LeftTopBack, RightTopBack
    }

    public enum BatchType
    {
        Static, Dynamic
    }

    public enum ClearMode
    {
        All, DynamicOnly, StaticOnly, StaticDynamic
    }

    public enum CameraMode
    {
        FirstPerson, ThirdPerson
    }

    public enum BroadcastSetting
    {
        None, RequireReceive
    }

}
