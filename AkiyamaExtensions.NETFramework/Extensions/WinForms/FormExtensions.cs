using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AkiyamaExtensions.Extensions.WinForms
{
    public static partial class FormExtensions
    {

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

        // UNTESTED
        public static List<T> GetAllControls<T>(this Form form)
        {
            List<T> controls = new List<T>();
            foreach (Control control in form.Controls)
            {
                if (control is T) { controls.Add((T)Convert.ChangeType(control, typeof(T))); }
                controls.AddRange(GetControlsRecursive<T>(control));
            }
            return controls;
        }

        // UNTESTED
        private static List<T> GetControlsRecursive<T>(Control c)
        {
            List<T> list = new List<T>();
            foreach (Control control in c.Controls)
            {
                if (control is T)
                {
                    T cn = (T)Convert.ChangeType(control, typeof(T));
                    list.Add(cn);
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
