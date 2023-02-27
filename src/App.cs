using StereoKit;
using StereoKit.Framework;

namespace StereoKitApp
{
    public class App
    {
        public SKSettings Settings => new SKSettings
        {
            appName = "StereoKit Template",
            assetsFolder = "Assets",
            displayPreference = DisplayMode.MixedReality
        };

        public static PassthroughFBExt Passthrough;

        Pose cubePose = new Pose(-0.1f, 0, -0.5f, Quat.Identity);
        Model cube;

        Matrix floorTransform = Matrix.TS(new Vec3(0, -1.5f, 0), new Vec3(30, 0.1f, 30));
        Material floorMaterial;

        public void Init()
        {
            // Create assets used by the app
            cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                Default.MaterialUI);

            floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            floorMaterial.Transparency = Transparency.Blend;
        }

        Pose windowPose = new Pose(0.1f, 0, -0.5f, Quat.LookDir(new Vec3(0,0,1)));

        public void Step()
        {
            UI.WindowBegin("Passthrough Demo", ref windowPose);

            if (UI.Button("Toggle Passthrough"))
            {
                if (Passthrough.EnabledPassthrough)
                {
                    Passthrough.Shutdown();
                }
                else
                {
                    Passthrough.Initialize();
                }
            }

            UI.WindowEnd();

            if (!Passthrough.EnabledPassthrough)
            {
                if (SK.System.displayType == Display.Opaque)
                    Default.MeshCube.Draw(floorMaterial, floorTransform);
            }

            UI.Handle("Cube", ref cubePose, cube.Bounds);
            cube.Draw(cubePose.ToMatrix());
        }
    }
}