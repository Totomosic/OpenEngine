using System;
using OpenEngine.Components;

namespace OpenEngine
{
    public static class LightSystem
    {

        public static void Update(GameTime time)
        {
            GameObject[] objects = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(LightComponent) });
            for (int i = 0; i < objects.Length; i++)
            {
                Transform transform = objects[i].Transform;
                LightComponent light = objects[i].GetComponent<LightComponent>();
                foreach (ShaderProgram shader in ShaderManager.GetAllShaders())
                {
                    shader.AddRequest(new Request<int>("UsedLights", objects.Length));
                    shader.AddRequest(new Request<int>("Lights[" + i.ToString() + "].Type", (int)light.Type));
                    shader.AddRequest(new Request<Vector3>("Lights[" + i.ToString() + "].Position", transform.Position));
                    shader.AddRequest(new Request<Vector3>("Lights[" + i.ToString() + "].Attenuation", light.Attenuation));
                    shader.AddRequest(new Request<float>("Lights[" + i.ToString() + "].Ambient", light.Ambient));
                    shader.AddRequest(new Request<Vector3>("Lights[" + i.ToString() + "].Direction", transform.Forward));
                    shader.AddRequest(new Request<float>("Lights[" + i.ToString() + "].Angle", light.Angle));
                    shader.AddRequest(new Request<Color>("Lights[" + i.ToString() + "].Color", light.Color));
                }
            }
        }

    }
}
