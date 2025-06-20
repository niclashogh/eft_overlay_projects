using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efto_window.Views.Components
{
    public class CListItem : ContentControl
    {
        public bool IsSelected { get; set; }

        public CListItem()
        {
            this.DefaultStyleKey = typeof(CListItem);
        }
    }
}
