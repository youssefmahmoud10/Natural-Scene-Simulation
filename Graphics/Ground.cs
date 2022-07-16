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
    class Ground
    {
        public mat4 scalematrix;
        public mat4 transmatrix;
        public mat4 rotmatrix;
        Model ground1;
        public Ground(float width, float length, float hight, int stride)
        {
            scalematrix = new mat4(1);
            transmatrix = new mat4(1);
            rotmatrix = new mat4(1);
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            //t1 = new Bitmap(projectPath + "\\Textures\\heightmap.jpg");
            ground1 = new Model();
            float[,] hights = new float[(int)(width), (int)(length)];
            Random r = new Random();
            for (int i = 0; i < width; i += stride)
            {
                for (int j = 0; j < length; j += stride)
                {
                    hights[i, j] = (float)(r.NextDouble() * hight);
                    //Color c = t1.GetPixel(i,j);
                    //hights[i,j]= (float)(t1.Height * .5);
                    //hights[i, j] = (float)(c);

                }
            }

            for (int i = 0; i < width - stride; i += stride)
            {
                for (int j = 0; j < length - stride; j += stride)
                {
                    vec3 v1 = new vec3(i, hights[i, j + stride], j + stride);
                    vec3 v2 = new vec3(i + stride, hights[i + stride, j + stride], j + stride);
                    vec3 v3 = new vec3(i, hights[i, j], j);
                    vec3 v4 = new vec3(i + stride, hights[i + stride, j], j);
                    vec3 v5 = v2;
                    vec3 v6 = v3;


                    ground1.vertices.Add(v1);
                    ground1.vertices.Add(v2);
                    ground1.vertices.Add(v3);
                    ground1.vertices.Add(v4);
                    ground1.vertices.Add(v5);
                    ground1.vertices.Add(v6);



                    vec2 uv1 = new vec2(0, 0);
                    vec2 uv2 = new vec2(1, 0);
                    vec2 uv3 = new vec2(0, 1);
                    vec2 uv4 = new vec2(1, 1);
                    vec2 uv5 = uv2;
                    vec2 uv6 = uv3;


                    ground1.uvCoordinates.Add(uv1);
                    ground1.uvCoordinates.Add(uv2);
                    ground1.uvCoordinates.Add(uv3);
                    ground1.uvCoordinates.Add(uv4);
                    ground1.uvCoordinates.Add(uv5);
                    ground1.uvCoordinates.Add(uv6);


                    vec3 e1 = v2 - v1;
                    vec3 e2 = v3 - v1;
                    e1 = glm.normalize(e1);
                    e2 = glm.normalize(e2);
                    vec3 n1 = glm.cross(e1, e2);



                    ground1.normals.Add(n1);
                    ground1.normals.Add(n1);
                    ground1.normals.Add(n1);


                    e1 = v6 - v4;
                    e2 = v5 - v4;
                    e1 = glm.normalize(e1);
                    e2 = glm.normalize(e2);
                    n1 = glm.cross(e1, e2);



                    ground1.normals.Add(n1);
                    ground1.normals.Add(n1);
                    ground1.normals.Add(n1);


                }
            }

            ground1.Initialize();
        }



        public void draw(int matID)
        {
            ground1.Draw(matID);
        }


        public void draw_with_trans(int matID)
        {
            ground1.Draw(matID, scalematrix, rotmatrix, transmatrix);

        }
    }


}
