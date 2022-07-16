using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System;
using System.IO;
using System.Media;
using System.Timers;
namespace Graphics
{   
    public partial class GraphicsForm : Form
    {
         
        public float i = 0;
        public float j = 0;
        System.Media.SoundPlayer pl;
        Renderer renderer = new Renderer();
        Thread MainLoopThread;

        float deltaTime;
        public GraphicsForm()
        {
            
            

            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            
                

           // MoveCursor();

            //renderer.SendLightData(1,1,1,100,0);
            initialize();
            deltaTime = 0.05f;
            MainLoopThread = new Thread(MainLoop);
            MainLoopThread.Start();
            
            
        }

     

        void initialize()
        {
            renderer.Initialize();
        }
        void MainLoop()
        {
            while (true)
            {
                renderer.Draw();
                for (; j < 1;)
                {

                    
                    renderer.SendLightData(j, j, j, j, j);
                    j += 0.001f;
                    Thread.SpinWait(1000);

                    renderer.Draw();
                    renderer.Update(deltaTime);
                    simpleOpenGlControl1.Refresh();
                }
                for (; j >1;) 
                {
                    renderer.SendLightData(j, j, j, j, j);
                    j -= 0.001f;
                    Thread.SpinWait(100);

                    renderer.Draw();
                    renderer.Update(deltaTime);
                    simpleOpenGlControl1.Refresh();
                //texrenderer.m.animSt.curr_frame + "";

                }


            }
        }
        private void GraphicsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderer.CleanUp();
            MainLoopThread.Abort();
        }
       

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw();
            renderer.Update(deltaTime);
        }
       

        private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //opengl_control_key(sender,e);

            float speed = 4f;
            
            if (e.KeyChar == 'w')
                renderer.cam.Walk(speed);
            if (e.KeyChar == 'a')
                renderer.cam.Strafe(-speed);
            if (e.KeyChar == 'd')
                renderer.cam.Strafe(speed);
            if (e.KeyChar == 's')
                renderer.cam.Walk(-speed);
            if (e.KeyChar == 0x11)
                renderer.cam.Walk(speed);
            if (e.KeyChar == 'z')
                renderer.cam.Fly(-speed);
            if (e.KeyChar == 'x')
                renderer.cam.Fly(speed);

            if (e.KeyChar == 'b')
                renderer.x += 0.1f;
            if (e.KeyChar == 'v')
                renderer.x -= 0.1f;
            if (e.KeyChar == 'q')
            {
                for (float i = 0; i < 10; i += .1f)
                {
                    renderer.cam.Walk(speed);
                    Thread.Sleep(20);
                    renderer.Draw();
                    renderer.Update(deltaTime);
                    simpleOpenGlControl1.Refresh();

                }
                for (float i = 0; i < 10; i += .1f)
                {
                    renderer.cam.Strafe(speed);
                    Thread.Sleep(20);
                    renderer.Draw();
                    renderer.Update(deltaTime);
                    simpleOpenGlControl1.Refresh();

                }
                for (float i = 0; i < 10; i += .1f)
                {
                    renderer.cam.Walk(-speed);
                    Thread.Sleep(20);
                    renderer.Draw();
                    renderer.Update(deltaTime);
                    simpleOpenGlControl1.Refresh();

                }
                for (float i = 0; i < 10; i += .1f)
                {
                    renderer.cam.Strafe(-speed);
                    Thread.Sleep(20);
                    renderer.Draw();
                    renderer.Update(deltaTime);
                    simpleOpenGlControl1.Refresh();

                }
                //for (int i = 0; i < 100; i++)
                //{
                //    renderer.cam.Walk(-i);

                //}
                //for (int i = 0; i < 100; i++)
                //{
                //    renderer.cam.Strafe(-i);

                //}
            }
       
            if (e.KeyChar == 'm')
            {
                if (i < 1)
                {
                    for (i = 0; i < 1; i += 0.01f)
                    {

                        renderer.SendLightData(i, i, i, i, i);
                        Thread.SpinWait(500);
                        renderer.Draw();
                        renderer.Update(deltaTime);
                        simpleOpenGlControl1.Refresh();


                    }
                }
                
                {
                    for (i = 1; i >0; i -= 0.01f)
                    {

                        renderer.SendLightData(i, i, i, i, i);
                        Thread.SpinWait(500);
                        renderer.Draw();
                        renderer.Update(deltaTime);
                        simpleOpenGlControl1.Refresh();


                    }
                }
            }
            if (e.KeyChar == 'n')
            {
                renderer.SendLightData(.8f, .8f, 1, 100, 0);
            }
            if (e.KeyChar == 'c')
                renderer.cam.Pitch(0.1f*speed);
            if (e.KeyChar == 'v')
                renderer.cam.Pitch(-0.1f * speed);

        }

        float prevX, prevY;
        private void simpleOpenGlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float speed = 0.05f;

            float delta = e.X - prevX;
            if (delta < 0)
                renderer.cam.Yaw(speed);
            if (delta > 0)
                renderer.cam.Yaw(-speed);


            delta = e.Y - prevY;
            if (delta > 2)
                renderer.cam.Pitch(-speed);
            else if (delta < -2)
                renderer.cam.Pitch(speed);

            MoveCursor();
        }






        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void simpleOpenGlControl1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void MoveCursor()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Point p = PointToScreen(simpleOpenGlControl1.Location);
            Cursor.Position = new Point(simpleOpenGlControl1.Size.Width / 2 + p.X, simpleOpenGlControl1.Size.Height / 2 + p.Y);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
            prevX = simpleOpenGlControl1.Location.X + simpleOpenGlControl1.Size.Width / 2;
            prevY = simpleOpenGlControl1.Location.Y + simpleOpenGlControl1.Size.Height / 2;
        }
    }
}
  
