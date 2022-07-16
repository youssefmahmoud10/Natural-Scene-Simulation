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
    class grass
    {
        Model mygrass;
        public mat4 scalematrix;
        public mat4 transmatrix;
        public mat4 rotmatrix;
        public grass ()
        {
            mygrass = new Model();
            scalematrix = new mat4(1);
            transmatrix = new mat4(1);
            rotmatrix = new mat4(1);
            vec3 v1 = new vec3(-1, 0, 0);
            vec3 v2 = new vec3(1, 0, 0);
            vec3 v3 = new vec3(1, 1, 0);
            vec3 v4 = new vec3(1, 1, 0);
            vec3 v5 = new vec3(-1, 0,0);
            vec3 v6 = new vec3(-1, 1, 0);

            mygrass.vertices.Add(v1);
            mygrass.vertices.Add(v2);
            mygrass.vertices.Add(v3);
            mygrass.vertices.Add(v4);
            mygrass.vertices.Add(v5);
            mygrass.vertices.Add(v6);
            vec2 uv1 = new vec2(0, 0);
            vec2 uv2 = new vec2(1, 0);
            vec2 uv3 = new vec2(1, 1);
            vec2 uv4 = new vec2(1, 1);
            vec2 uv5 = new vec2(0, 0);
            vec2 uv6 = new vec2(0, 1);
            mygrass.uvCoordinates.Add(uv1);
            mygrass.uvCoordinates.Add(uv2);
            mygrass.uvCoordinates.Add(uv3);
            mygrass.uvCoordinates.Add(uv4);
            mygrass.uvCoordinates.Add(uv5);
            mygrass.uvCoordinates.Add(uv6);

            vec3 e1 = v2 - v1;
            vec3 e2 = v3 - v1;
            e1 = glm.normalize(e1);
            e2 = glm.normalize(e2);
            vec3 n1 = glm.cross(e1, e2);



            mygrass.normals.Add(n1);
            mygrass.normals.Add(n1);
            mygrass.normals.Add(n1);


            e1 = v6 - v4;
            e2 = v5 - v4;
            e1 = glm.normalize(e1);
            e2 = glm.normalize(e2);
            n1 = glm.cross(e1, e2);



            mygrass.normals.Add(n1);
            mygrass.normals.Add(n1);
            mygrass.normals.Add(n1);

            mygrass.Initialize();

        }
        public void draW(int modelID)
        {
            mygrass.Draw(modelID, scalematrix, rotmatrix, transmatrix);

        }

    }
}
