using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using GlmNet;
using System.IO;
using Graphics._3D_Models;
using System.Drawing;
namespace Graphics
{
    class Renderer
    {
        Boolean reverse;
        float tt;
        uint seqid;
        //main shader 
        Shader sh;
        // water shader 
        Shader sh_W;
        
        int modelID_W,viewID_w,projID_W;
        int modelID;
        int viewID;
        int projID;
        
        Bitmap terrian;
        int EyePositionID;
        int AmbientLightID;
        int DataID;



        mat4 ProjectionMatrix;
        mat4 ViewMatrix;
        mat4 left, right, front, back, up, down;
        public float Speed = 1f;
           
        terrian ter;
        terrian ter1;
        terrian ter2;
        grass my_g;

        Model3D grass;
        Model3D grass1;
   

        md2 blade;

        public Camera cam;
       

        Texture p, d, f, b, r, l,t,w,m,i,gra,gra1,pa;
        
        Ground g;

        float red=0;
        float green=0;
        float blue=0;
        float attenuation=0;
        float specularExponent=0;

        public void Initialize()
        {

            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            sh_W = new Shader(projectPath + "\\Shaders\\water.vertexshader", projectPath + "\\Shaders\\water.fragmentshader");
            p = new Texture(projectPath + "\\Textures\\hills_up.png", 0, true);
            d = new Texture(projectPath + "\\Textures\\Ground.jpg", 1, true);
            // sky box texture 
            l = new Texture(projectPath + "\\Textures\\hills_lf.png", 2, true);
            r = new Texture(projectPath + "\\Textures\\hills_rt.png", 3, true);
            f = new Texture(projectPath + "\\Textures\\hills_ft.png", 4, true);
            b = new Texture(projectPath + "\\Textures\\hills_bk.png", 5, true);
            t = new Texture(projectPath + "\\Textures\\ground_grass_3264_4062_Small.jpg", 6, true);
            // water texture 
            w = new Texture(projectPath + "\\Textures\\download.jpg", 7, true);
            m = new Texture(projectPath + "\\Textures\\frontend-large.jpg", 8, true);
            i = new Texture(projectPath + "\\Textures\\wall-murals-ice-texture.jpg.jpg", 9, true);
            gra = new Texture(projectPath + "\\Textures\\download (2).jpg ", 10, true);
            gra1 = new Texture(projectPath + "\\Textures\\580b585b2edbce24c47b26b8 (1).png ", 11, true);
            pa = new Texture(projectPath + "\\Textures\\800px_COLOURBOX5788947.jpg", 12, true);




            // loading tree 2 modele 
            
            grass = new Model3D();
            grass.LoadFile(projectPath + "\\\\ModelFiles\\static\\grass", "Grass06.3DS", 6);
            grass.scalematrix = glm.scale(new mat4(1), new vec3(.5f, 2f, .5f));
            grass.transmatrix = glm.translate(new mat4(1), new vec3(-800, 300,700));
            grass.rotmatrix = glm.rotate(-70 / 180.0f * 3.1412f, new vec3(1, 0, 0));
            
            grass1 = new Model3D();
            grass1.LoadFile(projectPath + "\\\\ModelFiles\\static\\grass", "Grass06.3DS", 6);
            grass1.scalematrix = glm.scale(new mat4(1), new vec3(.2f, 5f, .2f));
            grass1.transmatrix = glm.translate(new mat4(1), new vec3(-600, 1800, -600));
            grass1.rotmatrix = glm.rotate(-90  / 180.0f * 3.1412f, new vec3(1, 0, 0));


            //palm.rotmatrix = glm.rotate(-80 / 180.0f * 3.1412f, new vec3(1, 0, 0));
            // animated character 
            blade = new md2(projectPath + "\\ModelFiles\\animated\\md2\\samourai\\Samourai.md2");
            blade.StartAnimation(animType.FLIP);
            blade.StartAnimation(animType.FLIP);
            blade.AnimationSpeed = 0.01f;
            blade.rotationMatrix = glm.rotate(-90.0f / 180.0f * 3.1412f, new vec3(1, 0, 0));
            blade.TranslationMatrix = glm.translate(new mat4(1), new vec3(-900, 500, 600));
            blade.scaleMatrix = glm.scale(new mat4(1), new vec3(7f, 10f, 7f));
            
            // terrian code 
            terrian = new Bitmap(projectPath + "\\Textures\\heightmap.jpg");
            ter = new terrian();
            ter1 = new terrian();
            ter2 = new terrian();

            my_g = new grass();
            my_g.scalematrix = glm.scale(new mat4(1), new vec3(100, 10, 100));
            my_g.transmatrix = glm.translate(new mat4(1), new vec3(500, 5, 50));
            //ter.scalematrix =(float) (glm.rotate((float)(new mat4(1)), new vec3(5, 5, 5));
            ter.scalematrix = glm.scale(new mat4(1), new vec3(10, 2f, 10));
            ter.transmatrix = glm.translate(new mat4(1), new vec3(-700, 10,-700));
            
            //g.scalematrix = glm.scale(new mat4(1), new vec3(.2f, 0, .2f));
            ter1.scalematrix = glm.scale(new mat4(1), new vec3(16, 4f, 15));
            ter1.rotmatrix = glm.rotate(180.0f / 180.0f * (22.0f / 7.0f), new vec3(0, 1, 0));
            ter1.transmatrix = glm.translate(new mat4(1), new vec3(900, -50,900));
            ter2.scalematrix = glm.scale(new mat4(1), new vec3(10,5f, 10));
            ter2.transmatrix = glm.translate(new mat4(1), new vec3(0, -10, 0));
            g = new Ground(10000, 10000,5, 50);
           
            g.transmatrix = glm.translate(new mat4(1), new vec3(-900, 300, -900));
            
            float[] seq = {
                -1,0,1,     1,0,0, 0,0, 0,1,0,
                1,0,1,      1,0,0, 1,0, 0,1,0,
                -1,0,-1,    1,0,0, 0,1, 0,1,0,
                1,0,1,      1,0,0, 1,0, 0,1,0,
                -1,0,-1,    1,0,0, 0,1, 0,1,0,
                1,0,-1,     1,0,0, 1,1, 0,1,0


            };
            seqid = GPU.GenerateBuffer(seq);
            down = MathHelper.MultiplyMatrices(new List<mat4>(){
                //glm.rotate(180.0f / 180.0f * 22.0f / 7.0f, new vec3(1, 0,0)),
                //glm.translate(new mat4 (1),new vec3(-130,-10,-120)),
                glm.scale(new mat4(1),new vec3(1000,1000,1000))

            });
            
            up = MathHelper.MultiplyMatrices(new List<mat4>(){
                glm.rotate(180 / 180.0f * 22.0f / 7.0f, new vec3(0, 0,1)),
                glm.rotate(90f / 180.0f * 22.0f / 7.0f, new vec3(0, 1,0)),
                glm.translate(new mat4 (1),new vec3(0,2,0)),
                glm.scale(new mat4(1),new vec3(1000,1000,1000))

            });
            left = MathHelper.MultiplyMatrices(new List<mat4>(){
                glm.rotate(90.0f / 180.0f * 22.0f / 7.0f, new vec3(0, 0,1)),
                glm.rotate(90.0f / 180.0f * 22.0f / 7.0f, new vec3(1, 0,0)),
                glm.rotate(180 / 180.0f * 22.0f / 7.0f, new vec3(0,1,0)),

                glm.translate(new mat4 (1),new vec3(-1,1,0)),
                glm.scale(new mat4(1),new vec3(1000,1000,1000))

            });
            right = MathHelper.MultiplyMatrices(new List<mat4>(){
                glm.rotate(90f / 180.0f * 22.0f / 7.0f, new vec3(0, 0,1)),
                glm.rotate(90.0f / 180.0f * 22.0f / 7.0f, new vec3(1, 0,0)),
                //glm.rotate(180 / 180.0f * 22.0f / 7.0f, new vec3(0,1,0)),

                glm.translate(new mat4 (1),new vec3(1,1,0)),
                glm.scale(new mat4(1),new vec3(1000,1000,1000))

            });
            front = MathHelper.MultiplyMatrices(new List<mat4>(){
                glm.rotate(90.0f / 180.0f * 22.0f / 7.0f, new vec3(1, 0,0)),
                glm.rotate(180 / 180.0f * 22.0f / 7.0f, new vec3(0,1,0)),
                glm.translate(new mat4 (1),new vec3(0,1,1)),
                glm.scale(new mat4(1),new vec3(1000,1000,1000))
            });
            back = MathHelper.MultiplyMatrices(new List<mat4>(){
                glm.rotate(90.0f / 180.0f * 22.0f / 7.0f, new vec3(1, 0,0)),
                //glm.rotate(180 / 180.0f * 22.0f / 7.0f, new vec3(0,1,0)),

                glm.translate(new mat4 (1),new vec3(0,1,-1)),
                glm.scale(new mat4(1),new vec3(1000,1000,1000))

            });



            Gl.glClearColor(0f, 0.2f, .4f, 1);


           

           // id = GPU.GenerateBuffer(ver);

            sh.UseShader();

            modelID_W = Gl.glGetUniformLocation(sh_W.ID, "model");

             
            cam = new Camera();
            //cam.Reset(0, 15, 20, 0, 0, 0, 0, 1, 0);
            cam.Reset(800, 1000, 700, 0, 0, 0, 0, 1, 0);
           
            //cam.Reset()
            projID_W = Gl.glGetUniformLocation(sh_W.ID, "projection");
            viewID_w = Gl.glGetUniformLocation(sh_W.ID, "view");
            //AmbientLightID = Gl.glGetUniformLocation(sh.ID, "ambientlight");

            modelID = Gl.glGetUniformLocation(sh.ID, "model");
            ProjectionMatrix = cam.GetProjectionMatrix();
            ViewMatrix = cam.GetViewMatrix();
            projID = Gl.glGetUniformLocation(sh.ID, "projection");
            viewID = Gl.glGetUniformLocation(sh.ID, "view");
            DataID = Gl.glGetUniformLocation(sh.ID, "data");

            AmbientLightID = Gl.glGetUniformLocation(sh.ID, "ambientLight");
            int LightPositionID = Gl.glGetUniformLocation(sh.ID, "LightPosition_worldspace");
            vec3 lightPosition = new vec3(-500.0f, 900f, -500);
            Gl.glUniform3fv(LightPositionID, 1, lightPosition.to_array());

            vec2 data = new vec2(100, 50);
            Gl.glUniform2fv(DataID, 1, data.to_array());

            //setup the ambient light component.
          //  vec3 ambientLight = new vec3(1f, 0, 0);
          //  Gl.glUniform3fv(AmbientLightID, 1, ambientLight.to_array());
            //setup the eye position.
            EyePositionID = Gl.glGetUniformLocation(sh.ID, "EyePosition_worldspace");
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthFunc(Gl.GL_LESS);
        }
        public void Draw()
        {
            sh.UseShader();

           Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            vec3 ambientLight = new vec3(red,green, blue);
            Gl.glUniform3fv(AmbientLightID, 1, ambientLight.to_array());
            //water motion

            //sh.UseShader();

            Gl.glUniformMatrix4fv(projID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());
            Gl.glUniformMatrix4fv(viewID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, new mat4(1).to_array());
            Gl.glUniform3fv(EyePositionID, 1, cam.GetCameraPosition().to_array());

            pa.Bind();

            // For each of your model, 
            // 1- call the draw method
            // 2- Pass to it the ID of the model in the vertex shader so that it can apply the correct transformations on its vertices

            // code goes here



            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, seqid);
            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glEnableVertexAttribArray(2);
            Gl.glEnableVertexAttribArray(3);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 11 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 11 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 11 * sizeof(float), (IntPtr)(6 * sizeof(float)));
            Gl.glVertexAttribPointer(3, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 11 * sizeof(float), (IntPtr)(8 * sizeof(float)));

            t.Bind();
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, down.to_array());
           Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);
            l.Bind();
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, left.to_array());
           Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);
            r.Bind();
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, right.to_array());
           Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);
            f.Bind();
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, front.to_array());
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);
            b.Bind();
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, back.to_array());
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);
            p.Bind();
            Gl.glUniformMatrix4fv(modelID, 1, Gl.GL_FALSE, up.to_array());
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 6);
            gra.Bind();


            m.Bind();
            ter.draw_with_trans(modelID);
            gra.Bind();
            ter1.draw_with_trans(modelID);
            i.Bind();
            ter2.draw_with_trans(modelID);
            //sh_W.UseShader();
            //==============================================
            sh_W.UseShader();
            Gl.glUniformMatrix4fv(projID_W, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());
            Gl.glUniformMatrix4fv(viewID_w, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(modelID_W, 1, Gl.GL_FALSE, new mat4(1).to_array());
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            w.Bind();
            g.draw_with_trans(modelID_W);
            Gl.glDisable(Gl.GL_BLEND);

            //===============================================
            if (reverse)
                tt -= 0.00001f;
            else
                tt += 0.00001f;
            if (tt > 0.2f)
                reverse = true;
            if (tt < 0)
                reverse = false;
            int timeID = Gl.glGetUniformLocation(sh_W.ID, "tt");
            Gl.glUniform1f(timeID, tt);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            Gl.glDisableVertexAttribArray(2);
            Gl.glDisableVertexAttribArray(3);

            //sh_W.UseShader();

        }
        public float x;
        public void Update(float deltaTime)
        {
            cam.UpdateViewMatrix();
            ProjectionMatrix = cam.GetProjectionMatrix();
            ViewMatrix = cam.GetViewMatrix();
            SendLightData(red,green,blue,attenuation,specularExponent);
            blade.UpdateAnimation();
        }
        public void SendLightData(float red , float green, float blue, float attenuation, float specularExponent)
        {
            this.red=red;
            this.green=green;
            this.blue=blue;
            this.attenuation=attenuation;
            this.specularExponent = specularExponent;

            vec3 ambientLight = new vec3(red, green, blue);
            Gl.glUniform3fv(AmbientLightID, 1, ambientLight.to_array());
            vec2 data = new vec2(attenuation, specularExponent);
            Gl.glUniform2fv(DataID, 1, data.to_array());
        }
        public void CleanUp()
        {
            sh.DestroyShader();
            sh_W.DestroyShader();
        }
    }
}
