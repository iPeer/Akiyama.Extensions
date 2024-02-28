using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Akiyama.Extensions.WinForms
{

    public enum ProgressBarState
    {
        NORMAL = 1,
        GREEN = 1,
        BLOCKED = 2,
        RED = 2,
        PAUSED = 3,
        YELLOW = 3
    }

    public static partial class ProgressBarExtensions
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

        /// <summary>
        /// Sets the state of the progress bar (whether the bar is green, yellow, or red)
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="state">The <see cref="ProgressBarState"/> to set the progress bar to.</param>
        public static void SetState(this ProgressBar bar, ProgressBarState state)
        {
            SendMessage(bar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }

    }

}
