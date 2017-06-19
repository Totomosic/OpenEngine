﻿using OpenEngine.Components;
using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class UpdateSystem
    {

        public static void Update(GameTime time)
        {
            GameObject[] objects = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(RigidBody) });
            foreach (GameObject obj in objects)
            {
                Transform transform = obj.Transform;
                RigidBody rb = obj.GetComponent<RigidBody>();

                rb.Velocity += rb.Acceleration * time.ElapsedSeconds;
                transform.Position += rb.Velocity * time.ElapsedSeconds;
            }
        }

    }
}