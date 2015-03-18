using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace RoFF.Controls
{
    internal class SharedDictionaryManager
    {
        internal static ResourceDictionary SharedDictionary
        {
            get
            {
                if (sharedDictionary == null)
                {
                    var resourceLocator = new System.Uri("/RoFF.Controls;component/RoFF.Controls.xaml", System.UriKind.Relative);

                    sharedDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocator);
                }

                return sharedDictionary;
            }
        }

        private static ResourceDictionary sharedDictionary;
    }
}
