using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Tao.OpenGl;
using GlmNet;
using System.IO;
using Graphics._3D_Models;
namespace Graphics
{
   

    class terrian
    {
        public mat4 scalematrix;
        public mat4 transmatrix;
        public mat4 rotmatrix;
        Bitmap bt1;
        Model terrain_m;
        public terrian ()
        {
            scalematrix = new mat4(1);
            transmatrix = new mat4(1);
            rotmatrix = new mat4(1);
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            bt1 = new Bitmap(projectPath + "\\Textures\\heightmap.jpg");
            terrain_m = new Model();
            int length = bt1.Height;
            int width = bt1.Width;
            for (int x = 0; x < width / 2 - 1; x++)
            {
                for (int z = 0; z < length / 2 - 1; z++)
                {
                    Color c1 = bt1.GetPixel(x, z);
                    Color c2 = bt1.GetPixel(x + 1, z);
                    Color c3 = bt1.GetPixel(x + 1, z + 1);
                    Color c4 = bt1.GetPixel(x, z + 1);
                    vec3 v1 = new vec3(x, c4.B, z + 1);
                    vec3 v2 = new vec3(x + 1, c3.B, z + 1);
                    vec3 v3 = new vec3(x, c1.B, z);
                    vec3 v4 = new vec3(x + 1, c2.B, z);
                    vec3 v5 = v2;
                    vec3 v6 = v3;
                    terrain_m.vertices.Add(v1);
                    terrain_m.vertices.Add(v2);
                    terrain_m.vertices.Add(v3);
                    terrain_m.vertices.Add(v4);
                    terrain_m.vertices.Add(v5);
                    terrain_m.vertices.Add(v6);

                    vec2 uv1 = new vec2(0, 0);
                    vec2 uv2 = new vec2(1, 0);
                    vec2 uv3 = new vec2(0, 1);
                    vec2 uv4 = new vec2(1, 1);

                    terrain_m.uvCoordinates.Add(uv1);
                    terrain_m.uvCoordinates.Add(uv2);
                    terrain_m.uvCoordinates.Add(uv3);
                    terrain_m.uvCoordinates.Add(uv4);

                    vec3 e1 = v2 - v1;
                    vec3 e2 = v3 - v1;
                    e1 = glm.normalize(e1);
                    e2 = glm.normalize(e2);
                    vec3 n1 = glm.cross(e1, e2);



                    terrain_m.normals.Add(n1);
                    terrain_m.normals.Add(n1);
                    terrain_m.normals.Add(n1);


                    e1 = v6 - v4;
                    e2 = v5 - v4;
                    e1 = glm.normalize(e1);
                    e2 = glm.normalize(e2);
                    n1 = glm.cross(e1, e2);



                    terrain_m.normals.Add(n1);
                    terrain_m.normals.Add(n1);
                    terrain_m.normals.Add(n1);
                }
            }
            terrain_m.Initialize();

         }
        public void draw (int matID)
        {
            terrain_m.Draw(matID);
        }
        public void draw_with_trans(int matID)
        {
            terrain_m.Draw(matID, scalematrix, rotmatrix, transmatrix);
        }
     }
    
}
