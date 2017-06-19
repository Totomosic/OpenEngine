using System;
using System.Collections.Generic;
using Pencil.Gaming;

namespace OpenEngine
{
    /// <summary>
    /// Class that represents a user event
    /// </summary>
    public class Event
    {

        #region FIELDS

        private EventType type;

        private Key key;
        private int scanCode;
        private KeyAction keyAction;
        private KeyModifiers keyModifiers;

        private char character;

        private bool mouseEntered;

        private MouseButton mouseButton;
        private KeyAction mouseAction;

        private float xScroll;
        private float yScroll;

        private int mouseXPos;
        private int mouseYPos;

        #endregion

        #region CONSTRUCTORS

        public Event(EventType eType, Key keyboardKey = default(Key), int scancode = 0, KeyAction keyaction = default(KeyAction), KeyModifiers keymodifiers = default(KeyModifiers), char chr = default(char), bool mouseentered = false, MouseButton mButton = MouseButton.Button1, KeyAction mouseaction = KeyAction.Press, double xscroll = 0, double yscroll = 0, double xPos = 0, double yPos = 0)
        {
            type = eType;
            key = keyboardKey;
            scanCode = scancode;
            keyAction = keyaction;
            keyModifiers = keymodifiers;

            character = chr;
            mouseEntered = mouseentered;

            mouseButton = mButton;
            mouseAction = mouseaction;

            xScroll = (float)xscroll;
            yScroll = (float)yscroll;

            mouseXPos = (int)xPos;
            mouseYPos = (int)yPos;
        }

        #endregion

        #region PROPERTIES

        public EventType Type
        {
            get { return type; }
        }

        public Key Key
        {
            get { return key; }
        }

        public int Scancode
        {
            get { return scanCode; }
        }

        public KeyAction KeyAction
        {
            get { return keyAction; }
        }

        public KeyModifiers KeyModifiers
        {
            get { return keyModifiers; }
        }

        public char Character
        {
            get { return character; }
        }

        public bool MouseEntered
        {
            get { return mouseEntered; }
        }

        public MouseButton MouseButton
        {
            get { return mouseButton; }
        }

        public KeyAction MouseAction
        {
            get { return mouseAction; }
        }

        public float XScroll
        {
            get { return xScroll; }
        }

        public float YScroll
        {
            get { return yScroll; }
        }

        public int MouseX
        {
            get { return mouseXPos; }
        }

        public int MouseY
        {
            get { return mouseYPos; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
