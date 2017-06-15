using System;
using System.Collections;
using System.Collections.Generic;
using Pencil.Gaming;

namespace OpenEngine
{
    public class EventQueue : IEnumerable<Event>
    {

        #region FIELDS

        private List<Event> events;

        #endregion

        #region CONSTRUCTORS

        public EventQueue()
        {
            events = new List<Event>();
        }

        #endregion

        #region PROPERTIES

        public int Length
        {
            get { return events.Count; }
        }

        public List<Event> Events
        {
            get { return events; }
        }

        #endregion

        #region PUBLIC METHODS

        public void AddEvent(Event @event)
        {
            events.Add(@event);
        }

        public void Clear()
        {
            events.Clear();
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return ((IEnumerable<Event>)events).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Event>)events).GetEnumerator();
        }

        public void Setup(GlfwWindowPtr window)
        {
            Glfw.SetCharCallback(window, CharCallback);
            Glfw.SetCursorEnterCallback(window, MouseEnterCallback);
            Glfw.SetCursorPosCallback(window, MousePosCallback);
            Glfw.SetKeyCallback(window, KeyCallback);
            Glfw.SetMouseButtonCallback(window, MouseButtonCallback);
            Glfw.SetScrollCallback(window, ScrollCallback);
        }

        #endregion

        #region PRIVATE METHODS

        #region CALLBACK FUNCTIONS

        private void CharCallback(GlfwWindowPtr window, char chr)
        {
            events.Add(new Event(EventType.Char, chr: chr));
        }

        private void MouseEnterCallback(GlfwWindowPtr window, bool entered)
        {
            events.Add(new Event(EventType.MouseEntered, mouseentered: entered));
        }

        private void MousePosCallback(GlfwWindowPtr window, double x, double y)
        {
            events.Add(new Event(EventType.MousePosition, xPos: x, yPos: y));
        }

        private void KeyCallback(GlfwWindowPtr window, Key key, int scancode, KeyAction keyaction, KeyModifiers mods)
        {
            events.Add(new Event(EventType.Key, keyboardKey: key, scancode: scancode, keyaction: keyaction, keymodifiers: mods));
        }

        private void MouseButtonCallback(GlfwWindowPtr window, MouseButton button, KeyAction action)
        {
            events.Add(new Event(EventType.MouseButton, mButton: button, mouseaction: action));
        }

        private void ScrollCallback(GlfwWindowPtr window, double xScroll, double yScroll)
        {
            events.Add(new Event(EventType.MouseScroll, xscroll: xScroll, yscroll: yScroll));
        }

        #endregion

        #endregion

    }
}
