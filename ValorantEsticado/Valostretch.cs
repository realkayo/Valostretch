using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Drawing.Drawing2D;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace ValorantEsticado
{
    public partial class Valostretch : Form
    {
        private const int GWL_STYLE = -16;
        private const int WS_BORDER = 0x00800000;
        private const int WS_POPUP = unchecked((int)0x80000000);
        private const int WS_VISIBLE = 0x10000000;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const string ProcessName = "VALORANT-Win64-Shipping";

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public Valostretch()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 10, 10));


            barra3.Size = new Size(532, 3);

        }

        private void SetValorantFullscreen()
        {
            IntPtr windowHandle = GetWindowHandleByProcessName(ProcessName);

            if (windowHandle != IntPtr.Zero)
            {
                SetWindowLong(windowHandle, GWL_STYLE, WS_POPUP | WS_VISIBLE);
                SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, SWP_FRAMECHANGED);
            }
            else
            {
                Console.WriteLine("Processo não encontrado.");
            }
        }

        private void RemoveBorder()
        {
            IntPtr windowHandle = GetWindowHandleByProcessName(ProcessName);

            if (windowHandle != IntPtr.Zero)
            {
                RECT rect;
                GetWindowRect(windowHandle, out rect);

                int style = GetWindowLong(windowHandle, GWL_STYLE);
                style &= ~WS_BORDER;
                SetWindowLong(windowHandle, GWL_STYLE, style);
                SetWindowPos(windowHandle, IntPtr.Zero, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, SWP_FRAMECHANGED);
            }
            else
            {
                Console.WriteLine("Processo não encontrado.");
            }
        }

        private IntPtr GetWindowHandleByProcessName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                return processes[0].MainWindowHandle;
            }
            return IntPtr.Zero;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int alpha = 75;
            int red = (int)(Math.Sin(Environment.TickCount * 0.002) * 127 + 128);
            int green = (int)(Math.Sin(Environment.TickCount * 0.002 + 2) * 127 + 128);
            int blue = (int)(Math.Sin(Environment.TickCount * 0.002 + 4) * 127 + 128);

            button1.ForeColor = Color.FromArgb(alpha, red, green, blue); 
            label3.ForeColor = Color.FromArgb(alpha, red, green, blue);
            barra3.BackColor = Color.FromArgb(alpha, red, green, blue);
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RemoveBorder();
            Thread.Sleep(1000);
            SetValorantFullscreen();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
