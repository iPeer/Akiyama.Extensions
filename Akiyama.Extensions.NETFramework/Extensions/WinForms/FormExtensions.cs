using System.Collections.Generic;
using System.Windows.Forms;

namespace Akiyama.Extensions.WinForms
{
    public static partial class FormExtensions
    {

        /// <summary>
        /// A list of all the controls in the <see cref="Form"/>
        /// <br /><b>Note</b>: This method will not return items within <see cref="ContextMenuStrip"/> or <see cref="MenuStrip"/>.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>A list of all <see cref="Control"/>s within a <see cref="Form"/>.</returns>
        public static List<Control> GetAllControls(this Form form)
        {
            List<Control> controls = new List<Control>();
            foreach (Control control in form.Controls)
            {
                controls.Add(control);
                controls.AddRange(GetControlsRecursive(control));
            }
            return controls;
        }

        /// <summary>
        /// A list of all the controls in the <see cref="Form"/> which match the type of <see cref="{T}"/>
        /// <br /><b>Note</b>: This method will not return items within <see cref="ContextMenuStrip"/> or <see cref="MenuStrip"/>.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>A <see cref="List"/> of all <see cref="Control"/>s within a <see cref="Form"/> that match the type of <see cref="{T}"/></returns>
        public static List<Control> GetAllControls<T>(this Form form)
        {
            List<Control> controls = new List<Control>();
            foreach (Control control in form.Controls)
            {
                if (control is T) { controls.Add(control); }
                controls.AddRange(GetControlsRecursive<T>(control));
            }
            return controls;
        }

        private static List<Control> GetControlsRecursive<T>(Control c)
        {
            List<Control> list = new List<Control>();
            foreach (Control control in c.Controls)
            {
                if (control is T)
                {
                    list.Add(control);
                }
                list.AddRange(GetControlsRecursive<T>(control));
            }
            return list;
        }

        private static List<Control> GetControlsRecursive(Control c)
        {
            List<Control> list = new List<Control>();
            foreach (Control control in c.Controls)
            {
                list.Add(control);
                list.AddRange(GetControlsRecursive(control));
            }
            return list;
        }

    }
}
